using ChaseP.Utils;
using UnityEngine;

namespace ArchOS
{
    public abstract class Program : ISlotable
    {
        private readonly HealthComponent _health;

        public ProgramSO Definition { get; }

        // Definition
        public string Name => Definition.Name;
        public ProgramType ProgramType => Definition.ProgramType;
        public int Atk => Definition.Atk;
        public int Def => Definition.Def;
        public int MaxRez => Definition.Rez;
        public string Details => Definition.Details;
        public int Cost => Definition.Cost;
        public int SlotCount => 1;

        // Runtime
        public int Rez => _health.Health;

        public ICaster Owner { get; private set; }
        public ITargetable Target { get; private set; }

        public bool IsActive { get; private set; }
        public bool IsDerezzed => _health.IsDead;
        public bool IsDestroyed { get; private set; }
        public bool HasBeenUsed { get; private set;  }

        public Program(ProgramSO definition, ICaster owner = null, ITargetable target = null)
        {
            Definition = definition;
            Owner = owner;
            Target = target;

            _health = new HealthComponent(definition.Rez);
        }

        public bool Activate()
        {
            if (!CanActivate())
            {
                return false;
            }

            Heal(MaxRez, true);

            IsActive = true;

            if(ProgramType is ProgramType.Defender)
            {
                HasBeenUsed = true;
            }

            OnActivate();

            return true;
        }

        public bool Deactivate()
        {
            if (!IsActive) return false;

            IsActive = false;

            OnDeactivate();

            return true;
        }

        public void Damage(int amount, bool canDestroy = false)
        {
            if (IsDestroyed) return;

            _health.Damage(amount);

            if (!IsDerezzed) return;

            Deactivate();

            if (canDestroy)
            {
                Destroy();
            }
        }
        
        public void Heal(int amount, bool overrideDead = false)
        {
            if (IsDestroyed) return;

            _health.Heal(amount, overrideDead);
        }

        private bool CanActivate()
        {
            if (IsActive) return false;
            if (IsDestroyed) return false;
            if (IsDerezzed) return false;
            if (HasBeenUsed) return false;

            return true;
        }

        private void Destroy()
        {
            if (IsDestroyed) return;

            IsDestroyed = true;

            Deactivate();

            OnDestroyed();
        }

        protected virtual void OnActivate()
        {
        }

        protected virtual void OnDeactivate()
        {
        }

        protected virtual void OnDerezzed()
        {
        }

        protected virtual void OnDestroyed()
        {
        }
    }
}
