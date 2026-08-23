using System;
using UnityEngine;

namespace ArchOS
{
    public class Netrunner : MonoBehaviour, IController
    {
        [Header("Player Stats")]
        [SerializeField, Min(1)] private int _int = 1;
        [SerializeField, Min(1)] private int _will = 1;
        [SerializeField, Min(1)] private int _cool = 1;
        [SerializeField, Min(1)] private int _emp = 1;
        [SerializeField, Min(1)] private int _tech = 1;
        [SerializeField, Min(1)] private int _ref = 1;
        [SerializeField, Min(1)] private int _luck = 1;
        [SerializeField, Min(1)] private int _body = 1;
        [SerializeField, Min(1)] private int _dex = 1;
        [SerializeField, Min(1)] private int _move = 1;

        public int Int => _int;
        public int Will => _will;
        public int Cool => _cool;
        public int Emp => _emp;
        public int Tech => _tech;
        public int Ref => _ref;
        public int Luck => _luck;
        public int Body => _body;
        public int Dex => _dex;
        public int Move => _move;
        public int MaxHealth
        {
            get
            {
                return 10 + (5 * (int) Mathf.Ceil((_body + _will) / 2f));
            }
        }
        public int MaxHumanity
        {
            get
            {
                return 10 * _emp;
            }
        }

        public Netrunner()
        {

        }

    }
}
