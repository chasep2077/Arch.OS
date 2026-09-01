using UnityEngine;

namespace ArchOS
{
    public class Eraser : Program, ISlotable
    {
        public Eraser(EraserSO definition, ICaster owner = null, ITargetable target = null) : base(definition, owner, target)
        {
        }

        protected override void OnActivate()
        {
            Debug.Log("Activated Eraser Program.");
        }

        protected override void OnDeactivate()
        {
            Debug.Log("Deactivated Eraser Program.");
        }
    }
}
