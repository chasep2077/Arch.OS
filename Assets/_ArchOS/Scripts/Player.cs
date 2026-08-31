using ChaseP.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace ArchOS
{
    public class Player : MonoBehaviour, IDamageable, IController
    {
        private Cyberdeck _deck;
        private HealthComponent _healthComponent;

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
        public int MaxHealth => _healthComponent != null ? _healthComponent.MaxHealth : _maxHealth;
        public int Health => _healthComponent != null ? _healthComponent.Health : 0;
        public DeckQuality DeckQuality => _deckQuality;
        public Cyberdeck Deck => _deck;
        public IReadOnlyList<IDeckModule> InstalledModules => _deck?.InstalledModules;

        private void Awake()
        {
            // Initialize components in Awake so other scripts can safely query them in Start
            _deck = new Cyberdeck(_deckQuality);
            _healthComponent = new HealthComponent(_maxHealth);

            _healthComponent.OnDeath += HandlePlayerDeath;
        }

        private void Start()
        {
            // Load starting modules into the deck
            foreach (ModuleSO moduleData in _startingModuleData)
            {
                if (moduleData == null) continue;

                // Turn the ScriptableObject data into a runtime module instance
                IDeckModule runtimeModule = ModuleFactory.CreateModule(moduleData);
                if (runtimeModule != null)
                {
                    _deck.AddModule(runtimeModule);
                }
            }
        }

        private void OnDestroy()
        {
            if (_healthComponent != null)
            {
                _healthComponent.OnDeath -= HandlePlayerDeath;
            }
        }

        private void HandlePlayerDeath(HealthComponent health)
        {
            Debug.Log($"{_name} has flatlined!");
            // Trigger jack out, game over screen, or combat cleanup here
        }

        public void Damage(int amount)
        {
            _healthComponent?.Damage(amount);
        }
    }
}