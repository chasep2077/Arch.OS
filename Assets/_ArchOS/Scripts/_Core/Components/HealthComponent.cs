using System;
using UnityEngine;

namespace ChaseP.Utils
{
    public class HealthComponent
    {
        public event Action OnDeath;
        public event Action<int, int> OnHealthChanged;

        public int MaxHealth { get; private set; }
        public int Health { get; private set; }

        public bool IsDead => Health <= 0;

        public HealthComponent(int maxHealth)
        {
            MaxHealth = maxHealth;
            Health = maxHealth;
        }

        public void Damage(int amount)
        {
            if (amount <= 0 || IsDead) return;

            ModifyHealth(-amount);
        }

        public void Heal(int amount, bool allowDead = false)
        {
            if (amount <= 0 || (IsDead && allowDead == false)) return;

            ModifyHealth(amount);
        }

        private void ModifyHealth(int amount)
        {
            Health = Mathf.Clamp(Health + amount, 0, MaxHealth);

            OnHealthChanged?.Invoke(Health, MaxHealth);

            if (Health <= 0)
            {
                OnDeath?.Invoke();
            }
        }
    }
}
