//using UnityEngine;

//namespace ArchOS
//{
//    public abstract class ProgramSO : ModuleSO
//    {
//        [Header("Program")]
//        [SerializeField] private string _name = "New Program";
//        [SerializeField] private ProgramType _programType;
//        [SerializeField, Min(0)] private int _atk = 0;
//        [SerializeField, Min(0)] private int _def = 0;
//        [SerializeField, Min(0)] private int _rez = 0;
//        [SerializeField, Min(0)] private int _cost = 0;
//        [SerializeField, TextArea(3, 10)] string _details;

//        public string Name => _name;
//        public ProgramType ProgramType => _programType;
//        public int Atk => _atk;
//        public int Def => _def;
//        public int Rez => _rez;
//        public int Cost => _cost;
//        public string Details => _details;
//    }
//}