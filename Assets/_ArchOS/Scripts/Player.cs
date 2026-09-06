using ChaseP.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace ArchOS
{
    public class Player : MonoBehaviour, IController, ICaster
    {
        [Header("Player Name")]
        [SerializeField] private string _name = "New Player";

        [Header("Player Stats")]
        [SerializeField, Min(1)] private int _interfaceRank = 1;
        [SerializeField, Min(1)] private int _maxHealth = 10;

        [Header("Cyberdeck Config")]
        [SerializeField] private DeckQuality _deckQuality = DeckQuality.Poor;
        [SerializeField] private List<ModuleSO> _startingModuleData = new();

        public string Name => _name;
        public int InterfaceRank => _interfaceRank;

        public HealthComponent Health { get; private set; }

        public DeckQuality DeckQuality => _deckQuality;
        public Cyberdeck Deck { get; private set; }

        public IReadOnlyList<ISlotable> InstalledModules => Deck?.SlottedItems;

        public PlayerStats Stats { get; private set; }

        private void Awake()
        {
            Deck = new Cyberdeck(_deckQuality);

            Health = new HealthComponent(_maxHealth);
            Stats = new PlayerStats(_interfaceRank);

            Health.OnDeath += HandlePlayerDeath;
        }

        private void Start()
        {
            LoadStartingModules();
        }

        private void LoadStartingModules()
        {
            foreach (ModuleSO moduleData in _startingModuleData)
            {
                if (moduleData == null)
                {
                    Debug.LogWarning("Player has a null starting module.");
                    continue;
                }

                ISlotable module = moduleData.CreateInstance(this);

                if (Deck.AddModule(module))
                {
                    Debug.Log($"Loaded module '{module.Definition.Name}' onto {Name}'s cyberdeck.");
                }
                else
                {
                    Debug.LogWarning($"Failed to load module '{moduleData.name}' onto {Name}'s cyberdeck.");
                }
            }
        }

        private void OnDestroy()
        {
            if (Health != null)
            {
                Health.OnDeath -= HandlePlayerDeath;
            }
        }

        private void HandlePlayerDeath()
        {
            Debug.Log($"{_name} has flatlined!");
        }





        [ContextMenu("Test Program")]
        private void TestProgram()
        {
            Deck.TestingAllPrograms();
        }

        [ContextMenu("Print Stats")]
        private void PrintStats()
        {
            Stats.TestingPrintStats();
        }
    }
}