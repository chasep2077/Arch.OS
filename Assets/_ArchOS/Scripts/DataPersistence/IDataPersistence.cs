using UnityEngine;

namespace ArchOS
{
    public interface IDataPersistence
    {
        void LoadData(PlayerData data);
        
        void SaveData(ref PlayerData data);
    }
}
