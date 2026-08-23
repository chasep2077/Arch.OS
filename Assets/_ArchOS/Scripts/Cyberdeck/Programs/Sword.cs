using UnityEngine;

namespace ArchOS
{
    public class Sword : Program
    {
        private static string _name = "Sword";
        private static string _description = "Does 3d6 REZ to a Black ICE Program, or 2d6 REZ to a Non-Black ICE Program.";
        private static int _slots = 1;
        private static int _cost = 50;

        public Sword() : base(_name, _description, _slots, _cost)
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
