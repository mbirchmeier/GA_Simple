using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_Simple
{
    class EightByteGA : IGeneticAlgorithm
    {
        private byte[] _bytes = new byte[8];
        private Gene[] _genes;
        private Random rand = new Random((int)DateTime.Now.Ticks);

        public EightByteGA()
        {
            rand.NextBytes(_bytes);
            _genes = _bytes.SelectMany(b => ByteToGenes(b)).ToArray();
        }

        public EightByteGA(byte[] bytes)
        {
            if (bytes.Length != 8)
            {
                throw new Exception("Algorithm must be 8 bits long");
            }
            _bytes = bytes;
            _genes = _bytes.SelectMany(b => ByteToGenes(b)).ToArray();
        }

        public static Gene[] ByteToGenes(byte b)
        {
            Gene top = new Gene(b / 16);
            Gene bottom = new Gene(b % 16);
            return new Gene[]{top, bottom};
        }

        public double GetValue()
        {
            double toReturn = 0;
            StringBuilder cleanEquation = new StringBuilder();
            Operation nextOper = Operation.Add;

            bool seekingNumber = true;
            foreach (Gene g in _genes)
            {
                if (seekingNumber && g.IsNumber())
                {
                    cleanEquation.Append(g.ToString());

                    Apply(ref toReturn, nextOper, g.Number);

                    seekingNumber = !seekingNumber;
                }
                else if (!seekingNumber && g.IsOperation())
                {
                    cleanEquation.Append(g.ToString());
                    nextOper = g.Operation;
                    seekingNumber = !seekingNumber;
                }
            }

            Console.WriteLine("{0} => {1} => {2}", this, cleanEquation.ToString(), toReturn);
            return 0.0;
        }

        public void Apply(ref double current, Operation operation, double next)
        {
            switch (operation)
            {
                case Operation.Add:
                    current = current + next;
                    break;

                case Operation.Subtract:
                    current = current - next;
                    break;

                case Operation.Multiply:
                    current = current * next;
                    break;

                case Operation.Divide:
                    //dividing by 0 doesn't work so throw out this section of the equation
                    if (next != 0)
                    {
                        current = current / next;    
                    }
                    break;
            }

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Gene g in _genes)
            {
                sb.Append(g.ToString());
            }
            return sb.ToString();
        }
    }
}
