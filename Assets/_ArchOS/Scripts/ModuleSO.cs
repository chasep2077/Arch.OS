using UnityEngine;

namespace ArchOS
{
    public abstract class ModuleSO : ScriptableObject
    {
        public abstract ISlotable CreateInstance(ICaster owner = null);
    }
}
