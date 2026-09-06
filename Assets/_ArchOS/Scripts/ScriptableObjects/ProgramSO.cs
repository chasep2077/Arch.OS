using UnityEngine;

namespace ArchOS
{
    [CreateAssetMenu(fileName = "New Program", menuName = "Items/ProgramSO")]
    public class ProgramSO : ItemSO
    {
        [Header("Program Information")]
        [SerializeField] private ProgramType _programType;
        [SerializeField, Min(0)] private int _atk = 0;
        [SerializeField, Min(0)] private int _def = 0;
        [SerializeField, Min(0)] private int _rez = 0;

        public ProgramType ProgramType => _programType;
        public int Atk => _atk;
        public int Def => _def;
        public int Rez => _rez;

        public override int SlotCount => 1;
    }
}
