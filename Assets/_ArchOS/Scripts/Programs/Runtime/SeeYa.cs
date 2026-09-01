using System.Collections;
using UnityEngine;

namespace ArchOS.Assets._ArchOS.Scripts.Programs.Runtime
{
    public class SeeYa : Program
    {
        public SeeYa(ProgramSO definition, ICaster owner = null, ITargetable target = null) : base(definition, owner, target)
        {
        }

        protected override void OnActivate()
        {
            if (Owner is not Player player) return;
            Debug.Log($"Activated {Definition.Name} Program.");

            player.Stats.Modify(StatModifier.Pathfinder, 2);
        }

        protected override void OnDeactivate()
        {
            if (Owner is not Player player) return;
            Debug.Log($"Deactivated {Definition.Name} Program.");

            player.Stats.Modify(StatModifier.Pathfinder, -2);
        }
    }
}