using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ArchOS
{
    public class Player : MonoBehaviour, IController
    {
        private Cyberdeck _deck;


        [Header("Player ID")]
        [SerializeField] private string _name = "New Player";

        [Header("Player Stats")]
        [SerializeField, Min(1)] private int _int = 1;
        [SerializeField, Min(1)] private int _ref = 1;
        [SerializeField, Min(1)] private int _dex = 1;
        [SerializeField, Min(1)] private int _tech = 1;
        [SerializeField, Min(1)] private int _cool = 1;
        [SerializeField, Min(1)] private int _will = 1;
        [SerializeField, Min(1)] private int _luck = 1;
        [SerializeField, Min(1)] private int _move = 1;
        [SerializeField, Min(1)] private int _body = 1;
        [SerializeField, Min(1)] private int _emp = 1;

        [Header("Cyberdeck Config")]
        [SerializeField] private DeckQuality _deckQuality = DeckQuality.Poor;

        public string Name => _name;
        public int Int => _int;
        public int Ref => _ref;
        public int Dex => _dex;
        public int Tech => _tech;
        public int Cool => _cool;
        public int Will => _will;
        public int Luck => _luck;
        public int Move => _move;
        public int Body => _body;
        public int Emp => _emp;
        public int MaxHealth
        {
            get
            {
                return 10 + (5 * (int)Mathf.Ceil((_body + _will) / 2f));
            }
        }
        public int MaxHumanity
        {
            get
            {
                return 10 * _emp;
            }
        }
        public DeckQuality DeckQuality => _deckQuality;
        public Cyberdeck Deck => _deck;
        public IReadOnlyList<IDeckModule> InstalledModules => _deck.InstalledModules;


        private void Start()
        {
            _deck = new Cyberdeck(_deckQuality);
        }

        // Save and Load System OLD
        public void SaveData(PlayerData data)
        {
            data.Name = Name;
            data.Int = Int;
            data.Ref = Ref;
            data.Dex = Dex;
            data.Tech = Tech;
            data.Cool = Cool;
            data.Will = Will;
            data.Luck = Luck;
            data.Move = Move;
            data.Body = Body;
            data.Emp = Emp;
            data.DeckQuality = DeckQuality;
            data.InstalledModules = InstalledModules;
        }

        public void LoadData(PlayerData data)
        {
            _name = data.Name;
            _int = data.Int;
            _ref = data.Ref;
            _dex = data.Dex;
            _tech = data.Tech;
            _cool = data.Cool;
            _will = data.Will;
            _luck = data.Luck;
            _move = data.Move;
            _body = data.Body;
            _emp = data.Emp;
            _deckQuality = data.DeckQuality;
            foreach (IDeckModule module in InstalledModules)
            {
                _deck.AddModule(module);
            }
        }
    }
}
