using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_Simple
{
    class BitCrosser
    {
        public const int BITS_PER_BYTE = 8;

        public static Byte[] Cross(int position, Byte[] parent1, Byte[] parent2)
        {
            List<Byte> toReturn = new List<Byte>();

            int wholeBytesBeforeCross = position / BITS_PER_BYTE;

            for (int x = 0; x != wholeBytesBeforeCross; x++)
            {
                toReturn.Add(parent1[x]);
            }

            int bitsFromFirstByte = position % BITS_PER_BYTE;
            if (bitsFromFirstByte != 0)
            {
                int divisor = (int)Math.Pow(2, bitsFromFirstByte);
                int p1Part = parent1[wholeBytesBeforeCross] / divisor;
                int p2Part = parent2[wholeBytesBeforeCross] % divisor;
                byte newByte = (byte)(p1Part * divisor + p2Part);
                toReturn.Add(newByte);
            }
            for (int x = toReturn.Count(); x != parent2.Length; x++)
            {
                toReturn.Add(parent2[x]);
            }

            return toReturn.ToArray();

        }

        public static void FlipBit(int position, ref Byte[] parent)
        {
            int affectedByte = position / BITS_PER_BYTE;
            int affectedBit = position % BITS_PER_BYTE;

            byte toAlter = parent[affectedByte];
            byte toFlip = (byte)Math.Pow(2, affectedBit);
            toAlter = (byte)(toAlter ^ toFlip); // do a bitwise xor to flip the bit
        }
    }
}
