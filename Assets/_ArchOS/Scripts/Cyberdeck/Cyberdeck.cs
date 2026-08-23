using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ArchOS
{
    public class Cyberdeck
    {
        private readonly List<IDeckModule> _installedModules = new List<IDeckModule>();

        public DeckQuality Quality { get; private set; }
        public IReadOnlyList<IDeckModule> InstalledModules => _installedModules;
        public int Cost => Quality switch
        {
            DeckQuality.Poor => 100,
            DeckQuality.Standard => 500,
            DeckQuality.Excellent => 1000,
            _ => -1
        };
        public int MaxSlots => Quality switch
        {
            DeckQuality.Poor => 5,
            DeckQuality.Standard => 7,
            DeckQuality.Excellent => 9,
            _ => -1
        };
        public int CurrentSlots => _installedModules.Sum(m => m.Slots);
        public int AvailableSlots => MaxSlots - CurrentSlots;

        public Cyberdeck(DeckQuality quality = DeckQuality.Poor)
        {
            Quality = quality;
        }

        public void SetQuality(DeckQuality quality)
        {
            Quality = quality;
        }

        public bool AddModule(IDeckModule module)
        {
            if (module == null) return false;
            if (CurrentSlots + module.Slots > MaxSlots) return false;

            if (module is Hardware hardware)
            {
                if (_installedModules.Contains(hardware)) return false;

                hardware.Install();
            }

            _installedModules.Add(module);
            return true;
        }

        public bool RemoveModule(IDeckModule module)
        {
            if (module == null || !_installedModules.Contains(module)) return false;

            if (module is Hardware hardware)
            {
                hardware.Uninstall();
            }

            _installedModules.Remove(module);
            return true;
        }
    }
}
