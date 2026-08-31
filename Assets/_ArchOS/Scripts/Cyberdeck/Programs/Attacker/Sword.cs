using UnityEngine;

namespace ArchOS
{
    public class Sword : Program
    {
        public Sword() : base("Sword")
        {

        }

        public override void Activate()
        {
            if (IsActive) return;

            IsActive = true;
        }

        public override void Deactivate()
        {
            if (!IsActive) return;

            IsActive = false;
        }
    }
}
