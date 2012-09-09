using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_Simple
{
    /// <summary>
    /// Note this program is designed to fulfil the example listed here: http://www.ai-junkie.com/ga/intro/gat3.html
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            double target = 0;
            bool targetSet = false;
            if (args.Count() == 1)
            {
                if (double.TryParse(args[1], out target))
                {
                    targetSet = true;
                }
            }
            while (!targetSet)
            {
                Console.WriteLine("Enter target:");
                string input = Console.ReadLine();
                targetSet = double.TryParse(input, out target);
            }

            Console.WriteLine("Algorithm will target a value of {0}", target);

            FitnessTester tester = new FitnessTester(target);
            IGeneticAlgorithm ga = new EightByteGA();
            double fitness = tester.GetFitness(ga);

            Console.WriteLine("Execution complete: Press any key to continue");
            Console.ReadKey();
        }
    }
}
