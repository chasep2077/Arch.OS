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
            if (Owner is not Player player) return;
            Debug.Log($"Activated {Definition.Name} Program.");

            player.Stats.Modify(StatModifier.Cloak, 2);
        }

        protected override void OnDeactivate()
        {
            if (Owner is not Player player) return;
            Debug.Log($"Deactivated {Definition.Name} Program.");

            player.Stats.Modify(StatModifier.Cloak, -2);
        }
    }
}
