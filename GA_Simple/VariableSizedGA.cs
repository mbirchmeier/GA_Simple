using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace GA_Simple
{
    class VariableSizedGA : IGeneticAlgorithm
    {
        private BitArray _bits;
        private Gene[] _genes;
        private static Random rand = new Random((int)DateTime.Now.Ticks);

        public VariableSizedGA()
        {
            Byte[] bytes = new Byte[8];
            rand.NextBytes(bytes);
            _bits = new BitArray(bytes);
            _genes = bytes.SelectMany(b => ByteToGenes(b)).ToArray();
        }

        public VariableSizedGA(BitArray bytes)
        {
            _bits = new BitArray(bytes);
            _genes = ToGenes(_bits).ToArray();
        }

        private IEnumerable<Gene> ToGenes(BitArray bits)
        {
            int numBytes = bits.Count / 8;
            if (bits.Count % 8 != 0) numBytes++;

            byte bytes = 0;
            int bitIndex = 0;

            for (int i = 0; i < bits.Count; i++) 
            {
                //if the bit has been set add the value
                if (bits[i])
                    bytes |= (byte)(1 << (3 - bitIndex));

                bitIndex++;
                if (bitIndex == 4) 
                {
                    bitIndex = 0;
                    yield return new Gene(bytes);
                    bytes = 0;
                }
            }

            //if we have any trailing bits return them
            if (bitIndex != 0)
            {
                yield return new Gene(bytes);
            }
    
        }

        public static Gene[] ByteToGenes(byte b)
        {
            Gene top = new Gene(b / 16);
            Gene bottom = new Gene(b % 16);
            return new Gene[]{top, bottom};
        }

        private double? _value = null;
        public Double Value 
        { 
            get
            {
                if (_value.HasValue)
                    return _value.Value;
                _value = GetValue();
                return _value.Value;
            }

        }

        private double GetValue()
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

            //Console.WriteLine("{0} => {1} => {2}", this, cleanEquation.ToString(), toReturn);
            return toReturn;
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

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public IGeneticAlgorithm CreateChild(IGeneticAlgorithm mate)
        {
            if (!(mate is VariableSizedGA))
            {
                throw new Exception("Unable to mate with different types currently");
            }
            VariableSizedGA typedMate = (VariableSizedGA)mate;

            BitArray childBytes;

            //99% chance of mating
            if (rand.NextDouble() <= .99)
            {
                int myCrossoverPoint = rand.Next(0, _bits.Length); // select crossover point for mate
                int mateCrossoverPoint = rand.Next(0, typedMate._bits.Length); // select crossover point for mate
                childBytes = BitCrosser.Cross(myCrossoverPoint, _bits, mateCrossoverPoint, typedMate._bits);
            }
            //1% chance of just moving through
            else
            {
                childBytes = this._bits;
            }

            
            //.1% chance of a mutation
            if (rand.NextDouble() <= .001)
            {
                int mutate = rand.Next(0, childBytes.Length);
                BitCrosser.FlipBit(mutate, ref childBytes);
            }

            return new VariableSizedGA(childBytes);

        }

        public override bool Equals(object obj)
        {
            if (obj is VariableSizedGA)
            {
                VariableSizedGA typedObj = (VariableSizedGA)obj;
                if (typedObj._bits.Length == _bits.Length)
                {
                    for (int x= 0; x != _bits.Length; x++)
                    {
                        if (typedObj._bits[x] != _bits[x])
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }
        
        public double Fitness {get; set;}
    }
}
