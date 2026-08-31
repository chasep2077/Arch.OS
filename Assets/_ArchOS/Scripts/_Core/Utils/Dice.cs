using UnityEngine;

namespace ChaseP.Utils
{
    public static class Dice
    {
        private static int Roll(int sides, int amount, out int[] rolls)
        {
            amount = Mathf.Max(1, amount);
            rolls = new int[amount];
            int total = 0;

            for (int i = 0; i < amount; i++)
            {
                // Note: Random.Range(int minInclusive, int maxExclusive)
                rolls[i] = Random.Range(1, sides + 1);
                total += rolls[i];
            }

            return total;
        }

        private static int Roll(int sides, int amount)
        {
            return Roll(sides, amount, out _);
        }

        public static int D4(int amount = 1) => Roll(4, amount);
        public static int D4(int amount, out int[] rolls) => Roll(4, amount, out rolls);

        public static int D6(int amount = 1) => Roll(6, amount);
        public static int D6(int amount, out int[] rolls) => Roll(6, amount, out rolls);

        public static int D8(int amount = 1) => Roll(8, amount);
        public static int D8(int amount, out int[] rolls) => Roll(8, amount, out rolls);

        public static int D10(int amount = 1) => Roll(10, amount);
        public static int D10(int amount, out int[] rolls) => Roll(10, amount, out rolls);

        public static int D12(int amount = 1) => Roll(12, amount);
        public static int D12(int amount, out int[] rolls) => Roll(12, amount, out rolls);

        public static int D20(int amount = 1) => Roll(20, amount);
        public static int D20(int amount, out int[] rolls) => Roll(20, amount, out rolls);

        public static int D100(int amount = 1) => Roll(100, amount);
        public static int D100(int amount, out int[] rolls) => Roll(100, amount, out rolls);
    }
}
