using UnityEngine;

namespace ArchOS
{
    public sealed class FileFunction : Function
    {
        public override FunctionType Type => FunctionType.File;

        public int DV { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public FileFunction(int dv = 0, string name = "NewFile", string description = "")
        {
            if (dv < 0) dv = 0;

            DV = dv;
            Name = string.IsNullOrEmpty(name) ? "NewFile" : name;
            Description = description ?? "";
        }

        public void SetDV(int dv)
        {
            if (dv < 0) dv = 0;

            DV = dv;
        }

        public void SetName(string name)
        {
            Name = string.IsNullOrEmpty(name) ? "NewFile" : name;
        }

        public void SetDescription(string description)
        {
            Description = description ?? "";
        }
    }
}
