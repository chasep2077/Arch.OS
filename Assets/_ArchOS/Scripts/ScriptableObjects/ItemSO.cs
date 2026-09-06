using UnityEngine;

namespace ArchOS
{
    public abstract class ItemSO : ScriptableObject
    {
        [Header("Item Information")]
        [SerializeField] private string _name;
        [SerializeField, TextArea(5, 15)] private string _description;
        [SerializeField, Min(0)] private int _cost;

        public string Name => _name;
        public string Description => _description;
        public int Cost => _cost;

        public abstract int SlotCount { get; }
    }
}
