﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_Simple
{
    interface IGeneticAlgorithm
    {
        double Value{get;}

        IGeneticAlgorithm CreateChild(IGeneticAlgorithm mate);

        double Fitness { get; set; }
    }
}
