using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_Simple
{
    class FitnessTester
    {
        private double _target;
        public FitnessTester(double target)
        {
            _target = target;
        }

        public double GetFitness(IGeneticAlgorithm toTest)
        {
            double actual = toTest.Value;

            if (actual == _target)
                return double.MaxValue;

            return 1.0 / Math.Abs(_target - actual);
        }


    }
}
