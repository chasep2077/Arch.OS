using UnityEngine;

namespace ArchOS
{
    public sealed class PasswordFunction : Function
    {
        public override FunctionType Type => FunctionType.Password;

        public int DV { get; private set; }

        public PasswordFunction(int dv = 0)
        {
            if(dv < 0) dv = 0;

            DV = dv;
        }

        public void SetDV(int dv)
        {
            if (dv < 0) dv = 0;

            DV = dv;
        }
    }
}
