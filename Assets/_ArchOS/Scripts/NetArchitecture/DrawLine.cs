using UnityEngine;

namespace ArchOS
{
    public class DrawLine : MonoBehaviour
    {
        private LineRenderer line;

        public Transform pointA;
        public Transform pointB;
        public Material material;

        void Start()
        {
            line = gameObject.AddComponent<LineRenderer>();
            line.material = material;
        }

        void Update()
        {
            line.SetPosition(0, pointA.position);
            line.SetPosition(1, pointB.position);
        }
    }
}
