using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Collections;

namespace GA_Simple
{
    class BitCrosser
    {
        public const int BITS_PER_BYTE = 8;

        public static BitArray Cross(int position, BitArray parent1, BitArray parent2)
        {
            return Cross(position, parent1, position, parent2);
        }

        //cross the first position1 bits from parent1 with the last bits of parent2 starting at position2
        public static BitArray Cross(int position1, BitArray parent1, int position2, BitArray parent2)
        {
            int finalLength = position1 + (parent2.Length - position2);
            
            BitArray toReturn = new BitArray(finalLength);
            for (int x = 0; x != position1; x++)
            {
                toReturn[x] = parent1[x];
            }
            for (int x = position2; x != parent2.Length; x++)
            {
                toReturn[position1 + x - position2] = parent2[x];
            }

            return toReturn;

        }

        public static void FlipBit(int position, ref BitArray parent)
        {
            parent[position] = !parent[position];
        }
    }
}
