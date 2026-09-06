using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ArchOS
{
    public class Cyberdeck
    {
        private readonly List<ISlotable> _slottedItems = new();

        public DeckQuality Quality { get; private set; }
        public IReadOnlyList<ISlotable> SlottedItems => _slottedItems;
        public int Cost => Quality switch
        {
            DeckQuality.Poor => 100,
            DeckQuality.Standard => 500,
            DeckQuality.Excellent => 1000,
            _ => 0
        };
        public int MaxSlots => Quality switch
        {
            DeckQuality.Poor => 5,
            DeckQuality.Standard => 7,
            DeckQuality.Excellent => 9,
            _ => 0
        };
        public int CurrentSlots => _slottedItems.Sum(module => module.Definition.SlotCount);
        public int AvailableSlots => MaxSlots - CurrentSlots;

        public Cyberdeck(DeckQuality quality = DeckQuality.Poor)
        {
            Quality = quality;
        }

        public void SetQuality(DeckQuality quality)
        {
            Quality = quality;
        }

        public bool CanInstall(ISlotable module)
        {
            if (module == null && ContainsModule(module)) return false;

            return CurrentSlots + module.Definition.SlotCount <= MaxSlots;
        }

        public bool AddModule(ISlotable module)
        {
            if (!CanInstall(module)) return false;

            _slottedItems.Add(module);

            Debug.Log($"Module '{module.Definition.Name}' installed");

            return true;
        }

        public bool RemoveModule(ISlotable module)
        {
            if (module == null) return false;

            if (!_slottedItems.Remove(module)) return false;

            Debug.Log($"Module '{module.Definition.Name}' removed.");

            return true;
        }

        public bool ContainsModule(ISlotable module)
        {
            return module != null && _slottedItems.Contains(module);
        }

        private bool running;
        public void TestingAllPrograms()
        {
            if (!running)
            {
                running = true;
                foreach (var module in _slottedItems)
                {
                    if (module is not Program program) continue;

                    program.Activate();
                }
            }
            else
            {
                running = false;
                foreach (var module in _slottedItems)
                {
                    if (module is not Program program) continue;

                    program.Deactivate();
                }
            }
        }
    }
}
