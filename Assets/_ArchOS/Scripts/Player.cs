using ChaseP.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace ArchOS
{
    public class Player : MonoBehaviour, IDamageable, IController, ICaster
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
        
        public IReadOnlyList<ISlotable> InstalledModules => _deck?.InstalledModules;

        private void Awake()
        {
            _deck = new Cyberdeck(_deckQuality);
            _healthComponent = new HealthComponent(_maxHealth);

            _healthComponent.OnDeath += HandlePlayerDeath;
        }

        private void Start()
        {
            LoadStartingModules();
        }

        private void LoadStartingModules()
        {
            foreach(ModuleSO moduleData in _startingModuleData)
            {
                if(moduleData == null)
                {
                    Debug.LogWarning("Player has a null starting module.");
                    continue;
                }

                ISlotable module = moduleData.CreateInstance(this);

                if (_deck.AddModule(module))
                {
                    Debug.Log($"Loaded module '{module.Name}' onto {Name}'s cyberdeck.");
                }
                else
                {
                    Debug.LogWarning($"Failed to load module '{moduleData.name}' onto {Name}'s cyberdeck.");
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

        private void HandlePlayerDeath()
        {
            Debug.Log($"{_name} has flatlined!");
        }

        public void Damage(int amount)
        {
            _healthComponent?.Damage(amount);
        }

        public void Heal(int amount, bool overrideDead = false)
        {
            _healthComponent?.Heal(amount, overrideDead);
        }
    }
}