namespace ArchOS
{
    public abstract class Hardware : IDeckModule
    {
        public ModuleSO Data { get; private set; }

        public string Name => Data != null ? Data.Name : "NewProgram";
        public string Description => Data != null ? Data.Description : "";
        public int CostEB => Data != null ? Data.CostEB : 0;
        public int SlotCount => Data != null ? Data.SlotCount : 1;

        public Hardware(ModuleSO data)
        {
            Data = data;
        }

        public virtual void Install() { }
        public virtual void Uninstall() { }
    }
}
