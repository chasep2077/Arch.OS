using UnityEngine;

namespace ArchOS
{
    /// <summary>
    /// Represents a Program or piece of Hardware installed in a Cyberdeck.
    /// All deck modules share the Cyberdeck's limited module slots, regardless
    /// of whether they are software or hardware.
    /// </summary>
    public interface IDeckModule
    {
        public string Name { get; }
        public string Description { get; }
        public int CostEB { get; }
        public int SlotCount { get; }
    }
}
