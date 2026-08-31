using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ArchOS
{
    public class DataPersistenceManager : MonoBehaviour
    {
        [Header("File Storage Config")]
        [SerializeField] private string fileName;

        private PlayerData playerData;
        private List<IDataPersistence> dataPersistenceObjects;
        private FileDataHandler dataHandler;

        public static DataPersistenceManager instance { get; private set; }

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("Found more than one Data Persistence Manager in the scene.");
            }

            instance = this;
        }

        [System.Obsolete]
        private void Start()
        {
            this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
            this.dataPersistenceObjects = FindAllDataPersistenceObjects();
            LoadGame();
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

        public void NewGame()
        {
            this.playerData = new PlayerData();
        }

        public void SaveGame()
        {
            foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
            {
                dataPersistenceObj.SaveData(playerData);
            }

            dataHandler.Save(playerData);
        }

        public void LoadGame()
        {
            this.playerData = dataHandler.Load();

            if (this.playerData == null)
            {
                Debug.Log("No data was found. Initializing data to defaults.");
                NewGame();
            }

            foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
            {
                dataPersistenceObj.LoadData(playerData);
            }
        }

        [System.Obsolete]
        private List<IDataPersistence> FindAllDataPersistenceObjects()
        {
            IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

            return new List<IDataPersistence>(dataPersistenceObjects);
        }
    }
}
