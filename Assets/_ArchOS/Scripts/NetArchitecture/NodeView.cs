using System.Collections.Generic;
using UnityEngine;

namespace ArchOS
{
    [ExecuteAlways] // Ensures line updates render in Scene View while dragging in Editor
    public class NodeView : MonoBehaviour
    {
        [SerializeField] private LineRenderer linePrefab;

        [System.Serializable]
        private class Connection
        {
            public Transform TargetTransform;
            public Vector3 TargetPosition;
            public LineRenderer Line;
        }

        [SerializeField] private List<Connection> _connections = new List<Connection>();
        public Level NodeData { get; private set; }

        public void Initialize(Level level)
        {
            NodeData = level;
            gameObject.name = $"Node_{level.Function?.GetType().Name ?? "Empty"}";
        }

        public void ConnectToChild(Transform childTransform)
        {
            if (linePrefab == null || childTransform == null) return;

            LineRenderer line = Instantiate(linePrefab, transform);

            // CRITICAL: Ensure world space is enabled
            line.useWorldSpace = true;
            line.positionCount = 2;

            _connections.Add(new Connection
            {
                TargetTransform = childTransform,
                Line = line
            });

            UpdateLinePositions();
        }

        public void ConnectToChild(Vector3 childPosition)
        {
            if (linePrefab == null) return;

            LineRenderer line = Instantiate(linePrefab, transform);

            // CRITICAL: Ensure world space is enabled
            line.useWorldSpace = true;
            line.positionCount = 2;

            _connections.Add(new Connection
            {
                TargetTransform = null,
                TargetPosition = childPosition,
                Line = line
            });

            UpdateLinePositions();
        }

        private void LateUpdate()
        {
            UpdateLinePositions();
        }

        private void UpdateLinePositions()
        {
            if (_connections == null) return;

            for (int i = _connections.Count - 1; i >= 0; i--)
            {
                Connection conn = _connections[i];

                // Remove missing/destroyed lines safely
                if (conn.Line == null)
                {
                    _connections.RemoveAt(i);
                    continue;
                }

                // Start of line is ALWAYS this node's current world position
                conn.Line.SetPosition(0, transform.position);

                // End of line follows target transform or static position
                if (conn.TargetTransform != null)
                {
                    conn.Line.SetPosition(1, conn.TargetTransform.position);
                }
                else
                {
                    conn.Line.SetPosition(1, conn.TargetPosition);
                }
            }
        }

        public void ClearConnections()
        {
            if (_connections == null) return;

            foreach (Connection conn in _connections)
            {
                if (conn.Line != null)
                {
#if UNITY_EDITOR
                    if (!Application.isPlaying)
                        DestroyImmediate(conn.Line.gameObject);
                    else
                        Destroy(conn.Line.gameObject);
#else
                    Destroy(conn.Line.gameObject);
#endif
                }
            }
            _connections.Clear();
        }
    }
}
