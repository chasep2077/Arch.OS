using UnityEngine;

namespace ArchOS
{
    public abstract class Program : IDeckModule
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public int Slots { get; protected set; }
        public int Cost { get; protected set; }
        public bool IsActive { get; protected set; }

        public Program(string name, string description, int slots, int cost)
        {
            Name = name;
            Description = description;
            Slots = slots;
            Cost = cost;
        }

        public abstract void Activate();
        public abstract void Deactivate();
    }
}
