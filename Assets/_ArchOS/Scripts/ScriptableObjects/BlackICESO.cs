using UnityEngine;

namespace ArchOS
{
    [CreateAssetMenu(fileName = "New Black ICE", menuName = "Items/BlackICESO")]
    public class BlackIceSO : ProgramSO
    {
        [Header("Black ICE Information")]
        [SerializeField, Min(0)] private int _per;
        [SerializeField, Min(0)] private int _spd;

        public int Per => _per;
        public int Spd => _spd;

        public override int SlotCount => 2;
    }
}
