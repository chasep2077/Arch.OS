using UnityEngine;

namespace ArchOS
{
    public class RenderArchitecture : MonoBehaviour
    {
        public GameObject NodePrefab;
        public Architecture Architecture { get; private set; }

        private void Start()
        {
            Architecture arch = new Architecture();
            arch.AddLevel(new Level(), arch.Root);

            Architecture = arch;

            LoadArchitecture();
        }

        public void SetArchitecture(Architecture architecture)
        {
            Architecture = architecture;
        }
    
        private void LoadArchitecture()
        {
            if (Architecture == null) return;

            int depth = Architecture.GetMaxDepth();

            float rootY = ((depth - 1) * 6f) + 1.5f;

            Level current = Architecture.Root;
            Vector3 position = new Vector3(0, rootY, 0);
            Quaternion quaternion = new Quaternion(0, 0, 0, 0);
            

            while (current != null)
            {
                GameObject currentObj = Instantiate(NodePrefab, position, quaternion);

                position -= new Vector3(0, -6, 0);

                if(current.Children.Count > 0)
                {
                    current = current.Children[0];
                }
                else
                {
                    current = null;
                }
            }

            
        }
    }
}
