using UnityEngine;

namespace ArchOS
{
    public abstract class Hardware : IDeckModule
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public int Slots { get; protected set; }
        public int Cost { get; protected set; }

        public Hardware(string name, string description, int slots, int cost)
        {
            Name = name;
            Description = description;
            Slots = slots;
            Cost = cost;
        }

        public virtual void Install() { }
        public virtual void Uninstall() { }
    }
}
