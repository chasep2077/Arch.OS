using JetBrains.Annotations;
using UnityEngine;

namespace ArchOS
{
    public class ControlNodeFunction : Function
    {
        public override FunctionType Type => FunctionType.ControlNode;

        public int DV { get; private set; }
        public string Description { get; private set; }
        public IController CurrentController { get; private set; }

        public ControlNodeFunction(int dv = 0, string description = "")
        {
            if(dv < 0) dv = 0;

            DV = dv;
            Description = description ?? "";
        }

        public void SetDV(int dv)
        {
            if( dv < 0 ) dv = 0;

            DV = dv;
        }

        public void SetDescription(string description)
        {
            Description = description ?? "";
        }

        public void SetController(IController controller)
        {
            CurrentController = controller;
        }

        public void ReleaseController()
        {
            CurrentController = null;
        }
    }
}
