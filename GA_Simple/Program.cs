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

            //max number of algorithms per generation
            const int GenerationSize = 100;


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

            int generationNumber = 0;

            List<IGeneticAlgorithm> currentGeneration = new List<IGeneticAlgorithm>();

            for (int x = 0; x != GenerationSize; x++)
            {
                IGeneticAlgorithm ga = new VariableSizedGA();
                ga.Fitness = tester.GetFitness(ga);
                currentGeneration.Add(ga);
            }

            bool matchFound = currentGeneration.Any(x => x.Value == target);

            while (!matchFound)
            {
                IGeneticAlgorithm strongest = currentGeneration.OrderByDescending(x => x.Fitness).First();
                Console.WriteLine("Generation {2}\t: Closest value {0} , Fitness: {1}", strongest.Value, strongest.Fitness, generationNumber);
                //Console.WriteLine(strongest.ToString());
                generationNumber++;
                List<IGeneticAlgorithm> lastGeneration = new List<IGeneticAlgorithm>();

                //copy each item over
                foreach (IGeneticAlgorithm ga in currentGeneration)
                {
                    lastGeneration.Add(ga);
                }
                currentGeneration.Clear();

                while (currentGeneration.Count != GenerationSize)
                {
                    IGeneticAlgorithm parent1 = RouletteSelect(lastGeneration);
                    IGeneticAlgorithm parent2 = RouletteSelect(lastGeneration);
                    IGeneticAlgorithm child = parent1.CreateChild(parent2);


                    if (!currentGeneration.Contains(child))
                    {
                        child.Fitness = tester.GetFitness(child);
                        currentGeneration.Add(child);
                    }
 
                }

                matchFound = currentGeneration.Any(x => x.Value == target);
            }
            IGeneticAlgorithm match = currentGeneration.OrderByDescending(x => x.Fitness).First();
            Console.WriteLine("Generation {2}\t: Closest value {0} , Fitness: {1}", match.Value, match.Fitness, generationNumber);
            Console.WriteLine(match.ToString());
            

            Console.WriteLine("Execution complete: Press any key to continue");
            Console.ReadKey();
        }
        public static Random rand = new Random((int)DateTime.Now.Ticks);

        public static IGeneticAlgorithm RouletteSelect(List<IGeneticAlgorithm> pool)
        {
            //sum up the fitnesses
            double total = pool.Sum(x => x.Fitness);

            //Get a random number between 0 and the total
            double target = rand.NextDouble() * total;

            foreach (IGeneticAlgorithm ga in pool)
            {
                if (target < ga.Fitness)
                {
                    return ga;
                }
                target -= ga.Fitness;
            }
            //should never reach this
            return pool.First();
        }
    }
}
