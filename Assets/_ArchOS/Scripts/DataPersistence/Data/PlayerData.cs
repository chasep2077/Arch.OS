using System.Collections.Generic;
using System.Xml.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;

namespace ArchOS
{
    public class PlayerData
    {
        public string Name;
        public int Int;
        public int Ref;
        public int Dex;
        public int Tech;
        public int Cool;
        public int Will;
        public int Luck;
        public int Move;
        public int Body;
        public int Emp;
        public DeckQuality DeckQuality;
        public IReadOnlyList<IDeckModule> InstalledModules;

        public PlayerData()
        {
            Name = "New Player";
            Int = 1;
            Ref = 1;
            Dex = 1;
            Tech = 1;
            Cool = 1;
            Will = 1;
            Luck = 1;
            Move = 1;
            Body = 1;
            Emp = 1;
            DeckQuality = DeckQuality.Poor;
            InstalledModules = new List<IDeckModule>();
        }
    }
}
