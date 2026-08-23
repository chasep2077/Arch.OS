using UnityEngine;

namespace ArchOS
{
    public class DrawLine : MonoBehaviour
    {
        public LineRenderer line;

        public Transform pointA;
        public Transform pointB;

        void Update()
        {
            line.SetPosition(0, pointA.position);
            line.SetPosition(1, pointB.position);
        }
    }
}
