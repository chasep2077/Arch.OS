using System;
using UnityEngine;

namespace ArchOS
{
    public enum StatModifier
    {
        Int,
        Ref,
        Dex,
        Move,
        Slide,
        Backdoor,
        Speed,
        Pathfinder,
        Cloak,
        Defense,
        Reduction
    }
    public class PlayerStats
    {
        public int InterfaceRank { get; private set; }

        public int IntMod { get; private set; }
        public int RefMod { get; private set; }
        public int DexMod { get; private set; }
        public int MoveMod { get; private set; }

        public int SlideMod { get; private set; }
        public int BackdoorMod { get; private set; }
        public int SpeedMod { get; private set; }
        public int PathfinderMod { get; private set; }
        public int CloakMod { get; private set; }

        public int DefenseMod { get; private set; }
        public int ReductionMod { get; private set; }

        public PlayerStats(int interfaceRank)
        {
            if (interfaceRank < 0) interfaceRank = 0;

            InterfaceRank = interfaceRank;
        }

        public void SetInterfaceRank(int value)
        {
            InterfaceRank = Math.Max(value, 0);
        }

        public void Modify(StatModifier stat, int amount)
        {
            switch (stat)
            {
                case StatModifier.Backdoor:
                    BackdoorMod += amount;
                    break;
                case StatModifier.Cloak:
                    CloakMod += amount;
                    break;
                case StatModifier.Defense:
                    DefenseMod += amount;
                    break;
                case StatModifier.Dex:
                    DexMod += amount;
                    break;
                case StatModifier.Int:
                    IntMod += amount;
                    break;
                case StatModifier.Move:
                    MoveMod += amount;
                    break;
                case StatModifier.Pathfinder:
                    PathfinderMod += amount;
                    break;
                case StatModifier.Reduction:
                    ReductionMod += amount;
                    break;
                case StatModifier.Ref:
                    RefMod += amount;
                    break;
                case StatModifier.Slide:
                    SlideMod += amount;
                    break;
                case StatModifier.Speed:
                    SpeedMod += amount;
                    break;
            }
        }

        public void SetStat(StatModifier stat, int value)
        {
            switch (stat)
            {
                case StatModifier.Backdoor:
                    BackdoorMod = value;
                    break;
                case StatModifier.Cloak:
                    CloakMod = value;
                    break;
                case StatModifier.Defense:
                    DefenseMod = value;
                    break;
                case StatModifier.Dex:
                    DexMod = value;
                    break;
                case StatModifier.Int:
                    IntMod = value;
                    break;
                case StatModifier.Move:
                    MoveMod = value;
                    break;
                case StatModifier.Pathfinder:
                    PathfinderMod = value;
                    break;
                case StatModifier.Reduction:
                    ReductionMod = value;
                    break;
                case StatModifier.Ref:
                    RefMod = value;
                    break;
                case StatModifier.Slide:
                    SlideMod = value;
                    break;
                case StatModifier.Speed:
                    SpeedMod = value;
                    break;
            }
        }

        public void ResetStats(bool includeInterface = false)
        {
            if (includeInterface)
            {
                InterfaceRank = 0;
            }

            IntMod = 0;
            RefMod = 0;
            DexMod = 0;
            MoveMod = 0;

            SlideMod = 0;
            BackdoorMod = 0;
            SpeedMod = 0;
            PathfinderMod = 0;
            CloakMod = 0;

            DefenseMod = 0;
            ReductionMod = 0;
        }

        public void TestingPrintStats()
        {
            Debug.Log($"InterfaceRank: {InterfaceRank}");

            Debug.Log($"IntMod: {IntMod}");
            Debug.Log($"RefMod: {RefMod}");
            Debug.Log($"DexMod: {DexMod}");
            Debug.Log($"MoveMod: {MoveMod}");

            Debug.Log($"SlideMod: {SlideMod}");
            Debug.Log($"BackdoorMod: {BackdoorMod}");
            Debug.Log($"SpeedMod: {SpeedMod}");
            Debug.Log($"PathfinderMod: {PathfinderMod}");
            Debug.Log($"CloakMod: {CloakMod}");

            Debug.Log($"DefenseMod: {DefenseMod}");
            Debug.Log($"ReductionMod: {ReductionMod}");
        }
    }
}
