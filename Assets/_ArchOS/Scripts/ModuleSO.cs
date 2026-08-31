using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace ArchOS
{
    public abstract class ModuleSO : ScriptableObject
    {
        [Header("Display Info")]
        [SerializeField] private string _name;
        [SerializeField, TextArea(3, 11)] private string _description;
        [SerializeField, Min(0)] private int _costEB;

        public string Name => _name;
        public string Description => _description;
        public int CostEB => _costEB;
        public virtual int SlotCount => 1;
    }
}
