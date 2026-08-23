using UnityEngine;

namespace ArchOS
{
    public class BackupDrive : Hardware
    {
        private static string _name = "Backup Drive";
        private static string _description = "While installed on a Cyberdekc, a Backup Drive \"saves\" Non-Black ICE Attacker, Defender, or Booster Programs that are destroyed by pulling them into the Backup Drive the instant before they meet their end. As a Meat Action, a Netrunner can re-install all Programs \"saved\" by the Backup Drive onto their deck, if they have the Slots for them. If removed from a Cyberdeck, the Backup Drive erases its contents automatically. Restored programs with once-per-Netrun restrictions and the like are restored in the exact state they were saved in, so you can't kill your own Armor to refresh it. Yeah, that means you. Takes 2 Hardware Option Slots.";
        private static int _slots = 2;
        private static int _cost = 100;

        public BackupDrive() : base(_name, _description, _slots, _cost)
        {
        }
    }
}
