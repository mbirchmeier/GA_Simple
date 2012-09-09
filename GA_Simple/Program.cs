using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA_Simple
{
    class Program
    {
        static void Main(string[] args)
        {
            int target = 0;
            bool targetSet = false;
            if (args.Count() == 1)
            {
                if (int.TryParse(args[1], out target))
                {
                    targetSet = true;
                }
            }
            while (!targetSet)
            {
                Console.WriteLine("Enter target:");
                string input = Console.ReadLine();
                targetSet = int.TryParse(input, out target);
            }

            Console.WriteLine("Algorithm will target a value of {0}", target);

            Console.WriteLine("Execution complete: Press any key to continue");
            Console.ReadKey();
        }
    }
}
