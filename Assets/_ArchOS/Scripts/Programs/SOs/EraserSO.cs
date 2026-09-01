using UnityEngine;

namespace ArchOS
{
    [CreateAssetMenu(fileName = "EraserSO", menuName = "Scriptable Objects/EraserSO")]
    public class EraserSO : ProgramSO
    {
        public override ISlotable CreateInstance(ICaster owner = null)
        {
            return new Eraser(this, owner);
        }
    }
}
