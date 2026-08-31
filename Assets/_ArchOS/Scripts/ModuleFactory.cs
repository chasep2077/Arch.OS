namespace ArchOS
{
    public static class ModuleFactory
    {
        public static IDeckModule CreateModule(ModuleSO data)
        {
            if (data == null) return null;

            // Map data to specific C# program instances
            return data.Name switch
            {
                "Sword" => new Sword(),
                "Eraser" => new Eraser(),
                // Add new program mappings here as you create them
                _ => null
            };
        }
    }
}