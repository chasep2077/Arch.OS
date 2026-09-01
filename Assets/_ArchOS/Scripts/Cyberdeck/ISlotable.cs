namespace ArchOS
{
    public interface ISlotable
    {
        public string Name { get; }
        public string Details { get; }
        public int Cost { get; }
        public int SlotCount { get; }
    }
}
