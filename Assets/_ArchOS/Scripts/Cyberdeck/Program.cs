using ChaseP.Utils;
using System;
using UnityEngine;

namespace ArchOS
{
    public abstract class Program : IDamageable, IDeckModule
    {
        public ProgramSO Data { get; private set; }
        public HealthComponent HealthComponent { get; private set; }

        public event Action<Program> OnProgramDestroyed;

        public string Name => Data != null ? Data.Name : "Unknown Program";
        public string Description => Data != null ? Data.Description : "";
        public int CostEB => Data != null ? Data.CostEB : 0;
        public int SlotCount => Data != null ? Data.SlotCount : 1;
        public int Atk => Data != null ? Data.Atk : 0;
        public int Def => Data != null ? Data.Def : 0;
        public int MaxRez => HealthComponent != null ? HealthComponent.MaxHealth : 0;
        public int Rez => HealthComponent != null ? HealthComponent.Health : 0;

        public bool IsActive { get; protected set; }
        public bool IsDestroyed { get; protected set; }

        protected Program(ProgramSO data)
        {
            Data = data;

            int baseRez = data != null ? data.Rez : 10;
            HealthComponent = new HealthComponent(baseRez);

            HealthComponent.OnDeath += HandleDeath;
        }

        private void HandleDeath()
        {
            IsActive = false;
            OnProgramDestroyed?.Invoke(this);
        }

        public abstract void Activate();
        public abstract void Deactivate();

        public void Damage(int amount)
        {
            HealthComponent?.Damage(amount);
        }

        public void Heal(int amount)
        {
            HealthComponent?.Heal(amount);
        }
    }
}