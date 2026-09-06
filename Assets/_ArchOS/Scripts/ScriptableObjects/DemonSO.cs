using UnityEngine;

namespace ArchOS
{
    [CreateAssetMenu(fileName = "New Demon", menuName = "Items/DemonSO")]
    public class DemonSO : ItemSO
    {
        [Header("Demon Information")]
        [SerializeField, Min(0)] private int _rez;
        [SerializeField, Min(0)] private int _interface;
        [SerializeField, Min(0)] private int _combatNumber;

        public int Rez => _rez;
        public int Interface => _interface;
        public int CombatNumber => _combatNumber;

        public override int SlotCount => -1;
    }
}
