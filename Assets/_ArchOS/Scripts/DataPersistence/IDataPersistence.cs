using UnityEngine;

namespace ArchOS
{
    public interface IDataPersistence
    {
        void SaveData(PlayerData data);
        void LoadData(PlayerData data);
    }
}
