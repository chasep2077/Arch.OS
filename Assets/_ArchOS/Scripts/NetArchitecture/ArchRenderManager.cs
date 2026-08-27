using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArchOS
{
    public class ArchRenderManager : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private NodeView nodePrefab;

        [Header("Layout Settings")]
        [SerializeField] private float verticalSpacing = 30f;
        [SerializeField] private float baseBranchOffset = 20f;
        [SerializeField] private float baseFloorY = 5f;
        [SerializeField] private float moveSpeed = 8f;

        private Architecture _activeArch;
        private readonly Dictionary<Level, NodeView> _activeNodes = new Dictionary<Level, NodeView>();
        private readonly Stack<NodeView> _nodePool = new Stack<NodeView>();

        public void BindArchitecture(Architecture arch)
        {
            if (_activeArch != null)
                _activeArch.OnArchitectureModified -= HandleArchitectureModified;

            _activeArch = arch;

            if (_activeArch != null)
            {
                _activeArch.OnArchitectureModified += HandleArchitectureModified;
                RebuildLayout();
            }
        }

        private void Start()
        {
            Level level1 = new Level();
            Level level2 = new Level();
            Level level3 = new Level();
            Level level4a = new Level();
            Level level5a = new Level();
            Level level6aa = new Level();
            Level level6ab = new Level();
            Level level7ab = new Level();
            Level level4b = new Level();
            Level level5b = new Level();
            Level level6ba = new Level();
            Level level6bb = new Level();

            Architecture arch = new Architecture("Arasaka", "", level1);

            arch.AddLevel(level2, level1);
            arch.AddLevel(level3, level2);
            arch.AddLevel(level4a, level3);
            arch.AddLevel(level5a, level4a);
            arch.AddLevel(level6aa, level5a);
            arch.AddLevel(level6ab, level5a);
            arch.AddLevel(level7ab, level6ab);
            arch.AddLevel(level4b, level3);
            arch.AddLevel(level5b, level4b);
            //arch.AddLevel(level6ba, level5b);
            //arch.AddLevel(level6bb, level5b);


            BindArchitecture(arch);
            RebuildLayout();
        }

        private void OnDestroy()
        {
            if (_activeArch != null)
                _activeArch.OnArchitectureModified -= HandleArchitectureModified;
        }

        private void HandleArchitectureModified()
        {
            RebuildLayout();
        }

        public void RebuildLayout()
        {
            if (_activeArch?.Root == null) return;

            // Track nodes present in current pass to purge removed nodes later
            HashSet<Level> visitedLevels = new HashSet<Level>();

            int maxDepth = _activeArch.GetMaxDepth(_activeArch.Root);
            float rootY = baseFloorY + (maxDepth * verticalSpacing);
            Vector3 rootPos = new Vector3(0, rootY, 0);

            CalculateAndAnimateNodes(_activeArch.Root, rootPos, visitedLevels);

            // Clean up 3D nodes that were removed from the data structure
            List<Level> toRemove = new List<Level>();
            foreach (var kvp in _activeNodes)
            {
                if (!visitedLevels.Contains(kvp.Key))
                {
                    RecycleNode(kvp.Value);
                    toRemove.Add(kvp.Key);
                }
            }

            foreach (Level level in toRemove)
            {
                _activeNodes.Remove(level);
            }
        }

        private void CalculateAndAnimateNodes(Level current, Vector3 targetPos, HashSet<Level> visitedLevels)
        {
            visitedLevels.Add(current);

            // Fetch existing NodeView or fetch from pool
            NodeView view = GetOrCreateNodeView(current);
            StartCoroutine(AnimateToPosition(view.transform, targetPos));

            view.ClearConnections();

            int childCount = current.Children.Count;
            if (childCount == 0) return;

            float childY = targetPos.y - verticalSpacing;

            if (childCount == 1)
            {
                Vector3 childPos = new Vector3(targetPos.x, childY, targetPos.z);
                view.ConnectToChild(childPos);
                CalculateAndAnimateNodes(current.Children[0], childPos, visitedLevels);
                return;
            }

            for (int i = 0; i < childCount; i++)
            {
                Level child = current.Children[i];
                int branchCount = GetSubtreeBranchCount(child);
                float offset = baseBranchOffset * (1 + branchCount);
                float direction = (i == 0) ? -1f : 1f;

                Vector3 childPos = new Vector3(targetPos.x + (direction * offset), childY, targetPos.z);

                // 1. Fetch/Instantiate the child node first
                NodeView childView = GetOrCreateNodeView(child);

                // 2. Connect using the child Transform reference so LateUpdate tracks it
                view.ConnectToChild(childView.transform);

                // 3. Recurse down
                CalculateAndAnimateNodes(child, childPos, visitedLevels);
            }
        }

        private int GetSubtreeBranchCount(Level node)
        {
            if (node == null || node.Children.Count == 0) return 0;

            int branches = (node.Children.Count > 1) ? 1 : 0;
            foreach (Level child in node.Children)
            {
                branches += GetSubtreeBranchCount(child);
            }
            return branches;
        }

        #region Object Pooling & Motion

        private NodeView GetOrCreateNodeView(Level level)
        {
            if (_activeNodes.TryGetValue(level, out NodeView existingView))
                return existingView;

            NodeView view = (_nodePool.Count > 0) ? _nodePool.Pop() : Instantiate(nodePrefab, transform);
            view.gameObject.SetActive(true);
            view.Initialize(level);

            _activeNodes[level] = view;
            return view;
        }

        private void RecycleNode(NodeView view)
        {
            view.ClearConnections();
            view.gameObject.SetActive(false);
            _nodePool.Push(view);
        }

        private IEnumerator AnimateToPosition(Transform targetTransform, Vector3 endPos)
        {
            while (Vector3.Distance(targetTransform.position, endPos) > 0.01f)
            {
                targetTransform.position = Vector3.Lerp(targetTransform.position, endPos, Time.deltaTime * moveSpeed);
                yield return null;
            }
            targetTransform.position = endPos;
        }

        #endregion
    }

    //public class ArchRenderManager : MonoBehaviour
    //{
    //    [Header("Prefabs")]
    //    [SerializeField] private NodeView nodePrefab;

    //    [Header("Layout Settings")]
    //    [SerializeField] private float verticalSpacing = 30f;
    //    [SerializeField] private float baseBranchOffset = 20f;
    //    [SerializeField] private float baseFloorY = 5f;

    //    private readonly List<GameObject> _spawnedObjects = new List<GameObject>();

    //    private void Start()
    //    {
    //        Level level1 = new Level();
    //        Level level2 = new Level();
    //        Level level3 = new Level();
    //        Level level4a = new Level();
    //        Level level5a = new Level();
    //        Level level6aa = new Level();
    //        Level level6ab = new Level();
    //        Level level7ab = new Level();
    //        Level level4b = new Level();
    //        Level level5b = new Level();
    //        Level level6ba = new Level();
    //        Level level6bb = new Level();

    //        Architecture arch = new Architecture("Arasaka", "", level1);

    //        arch.AddLevel(level2, level1);
    //        arch.AddLevel(level3, level2);
    //        arch.AddLevel(level4a, level3);
    //        arch.AddLevel(level5a, level4a);
    //        arch.AddLevel(level6aa, level5a);
    //        arch.AddLevel(level6ab, level5a);
    //        arch.AddLevel(level7ab, level6ab);
    //        arch.AddLevel(level4b, level3);
    //        arch.AddLevel(level5b, level4b);
    //        //arch.AddLevel(level6ba, level5b);
    //        //arch.AddLevel(level6bb, level5b);


    //        RenderArchitecture(arch);
    //    }

    //    public void RenderArchitecture(Architecture arch)
    //    {
    //        ClearScene();

    //        if (arch?.Root == null) return;

    //        int maxDepth = arch.GetMaxDepth(arch.Root);
    //        float rootY = baseFloorY + (maxDepth * verticalSpacing);
    //        Vector3 rootPosition = new Vector3(0, rootY, 0);

    //        GenerateNodeRecursive(arch.Root, rootPosition);
    //    }

    //    private void GenerateNodeRecursive(Level current, Vector3 position)
    //    {
    //        // 1. Spawn current Node
    //        NodeView view = Instantiate(nodePrefab, position, Quaternion.identity, transform);
    //        view.Initialize(current);
    //        _spawnedObjects.Add(view.gameObject);

    //        int childCount = current.Children.Count;
    //        if (childCount == 0) return;

    //        float childY = position.y - verticalSpacing;

    //        // Single child drops straight down along parent's X position
    //        if (childCount == 1)
    //        {
    //            Vector3 childPos = new Vector3(position.x, childY, position.z);
    //            view.ConnectToChild(childPos);
    //            GenerateNodeRecursive(current.Children[0], childPos);
    //            return;
    //        }

    //        // 2. Branching: calculate offset based on internal sub-branching below
    //        for (int i = 0; i < childCount; i++)
    //        {
    //            Level child = current.Children[i];

    //            // Count internal branches below this specific child
    //            int branchCount = GetSubtreeBranchCount(child);

    //            // Calculate total offset (e.g., 20 * (1 + 1) = 40)
    //            float offset = baseBranchOffset * (1 + branchCount);

    //            // Symmetric placement: Left for first child (-), Right for second child (+)
    //            float direction = (i == 0) ? -1f : 1f;
    //            float childX = position.x + (direction * offset);

    //            Vector3 childPos = new Vector3(childX, childY, position.z);

    //            // Connect visually and recurse downward
    //            view.ConnectToChild(childPos);
    //            GenerateNodeRecursive(child, childPos);
    //        }
    //    }

    //    /// <summary>
    //    /// Recursively counts how many internal branch points (nodes with > 1 child) exist anywhere in this node's
    //    /// subtree.
    //    /// </summary>
    //    private int GetSubtreeBranchCount(Level node)
    //    {
    //        if (node == null || node.Children.Count == 0) return 0;

    //        int branches = 0;
    //        if (node.Children.Count > 1)
    //        {
    //            branches += 1;
    //        }

    //        foreach (Level child in node.Children)
    //        {
    //            branches += GetSubtreeBranchCount(child);
    //        }

    //        return branches;
    //    }

    //    public void ClearScene()
    //    {
    //        foreach (GameObject obj in _spawnedObjects)
    //        {
    //            if (obj != null) Destroy(obj);
    //        }
    //        _spawnedObjects.Clear();
    //    }
    //}
}
