using UnityEngine;

namespace ArchOS
{
    [CreateAssetMenu(fileName = "New Hardware", menuName = "Items/HardwareSO")]
    public class HardwareSO : ItemSO
    {
        [Header("Hardware Information")]
        [SerializeField, Range(1, 3)] private int _slotCount;

        public override int SlotCount => _slotCount;
    }
}
