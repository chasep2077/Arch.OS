using ChaseP.Utils;
using UnityEngine;

namespace ArchOS
{
    public abstract class Program : ISlotable
    {
        [SerializeField] private ProgramSO _definition;

        private readonly HealthComponent _health;

        public ItemSO Definition => _definition;

        // Definition
        public string Name => _definition.Name;
        public string Description => _definition.Description;
        public int Cost => _definition.Cost;
        public ProgramType ProgramType => _definition.ProgramType;
        public int Atk => _definition.Atk;
        public int Def => _definition.Def;
        public int MaxRez => _definition.Rez;
        public int Rez => _health.Health;
        public int SlotCount => 1;

        public ICaster Owner { get; private set; }
        public ITargetable Target { get; private set; }

        public bool IsActive { get; private set; }
        public bool IsDerezzed => _health.IsDead;
        public bool IsDestroyed { get; private set; }
        public bool HasBeenUsed { get; private set; }

        public ItemSO definition => throw new System.NotImplementedException();

        public Program(ProgramSO definition, ICaster owner = null, ITargetable target = null)
        {
            _definition = definition;
            Owner = owner;
            Target = target;

            _health = new HealthComponent(definition.Rez);
        }

        public bool Activate()
        {
            if (!CanActivate()) return false;

            Heal(MaxRez, true);
            IsActive = true;

            if (ProgramType is ProgramType.Defender)
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

        public void Heal(int amount, bool allowDead = false)
        {
            if (IsDestroyed) return;

            _health.Heal(amount, allowDead);
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
