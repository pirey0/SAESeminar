using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalLessons.GPR4100
{
    class Calculator
    {

        public static void Main()
        {
            RunCalculator();
        }

        private static void RunCalculator()
        {
            bool running = true;
            while (running)
            {
                char key1 = Console.ReadKey().KeyChar;
                char key2 = Console.ReadKey().KeyChar;
                char key3 = Console.ReadKey().KeyChar;

                Console.WriteLine("");
                Console.WriteLine("Werte sind: " + key1 + key2 + key3);

                float wert1 = 0;
                float wert2 = 0;

                try
                {
                    wert1 = float.Parse(key1.ToString());
                    wert2 = float.Parse(key3.ToString());
                }
                catch
                {
                    Console.WriteLine("Error");
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                }

                CalculateResult(key2, wert1, wert2);
            }
        }

        private static void CalculateResult(char key2, float wert1, float wert2)
        {
            if (key2 == '*')
            {
                Console.WriteLine("Multiplikation: " + wert1 * wert2);
            }
            else if (key2 == '+')
            {
                Console.WriteLine("Addition: " + (wert1 + wert2));
            }
            else if (key2 == '/')
            {
                if (wert2 == 0)
                {
                    Console.WriteLine("Error: Division By Zero");
                }
                else
                {
                    Console.WriteLine("Division: " + (wert1 / wert2));
                }
            }
            else if (key2 == '-')
            {
                Console.WriteLine("Subtration: " + (wert1 - wert2));
            }
            else
            {
                Console.WriteLine("Error");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
