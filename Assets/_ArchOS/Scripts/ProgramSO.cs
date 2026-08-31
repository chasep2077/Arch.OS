using JetBrains.Annotations;
using UnityEngine;

namespace ArchOS
{
    [CreateAssetMenu(fileName = "ProgramSO", menuName = "Scriptable Objects/ProgramSO")]
    public class ProgramSO : ModuleSO
    {
        [Header("Stats")]
        [SerializeField] private int _atk = 0;
        [SerializeField] private int _def = 0;
        [SerializeField] private int _rez = 0;

        public int Atk => _atk;
        public int Def => _def;
        public int Rez => _rez;
    }
}
