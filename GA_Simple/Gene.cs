using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_Simple
{
    //represents a 0-15 value for a gene
    public class Gene
    {
        int _value;
        public Gene(int value)
        {
            if (value < 0 || 15 < value)
                throw new Exception("Gene must be between 0 and 15");
            _value = value;
        }
        public int Number
        {
            get
            {
                if (IsNumber())
                    return _value;
                throw new Exception("Not A Number");
            }
        }

        public Operation Operation
        {
            get
            {
                if (IsOperation())
                {
                    switch (_value)
                    {
                        case 10:
                            return GA_Simple.Operation.Add;
                        case 11:
                            return GA_Simple.Operation.Subtract;
                        case 12:
                            return GA_Simple.Operation.Multiply;
                        case 13:
                            return GA_Simple.Operation.Divide;
                    }
                }
                throw new Exception("Not an operation");
            }
        }
        public bool IsNumber()
        {
            return _value <= 9;
        }
        public bool IsOperation()
        {
            return 10 <= _value && _value <= 13;
        }
        public override string ToString()
        {
            
                if (_value <= 9)
                {
                    return _value.ToString();
                }
                else if (_value == 10)
                {
                    return "+";
                }
                else if (_value == 11)
                {
                    return "-";
                }
                else if (_value == 12)
                {
                    return "*";
                }
                else if (_value == 13)
                {
                    return "/";
                }
                else
                {
                    return "_";
                }
        }
    }
}
