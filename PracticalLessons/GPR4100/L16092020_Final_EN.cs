using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalLessons.GPR4100
{
    public class Lesson1
    {
        public static void Main()
        {
            int f  = Fibonacci(1000);

            Console.WriteLine(f);

            Console.ReadLine();
            return;

            Console.ReadLine();
            Task1();
            Console.ReadLine();
            Task2();
            Console.ReadLine();
            Task3();
            Console.ReadLine();
            Task4();
            Console.ReadLine();
            Task5();
            Console.ReadLine();
            Task6();
            Console.ReadLine();
            Task7();
            Console.ReadLine();
            Task8();
            Console.ReadLine();

            Console.WriteLine("Task 9:");
            int result = Factorial(4);
            Console.WriteLine(result);
            Console.ReadLine();

            Console.WriteLine("Task 10:");
            int[] primes = { 2, 3, 5 };
            List<int> results = Factorial(primes);
            foreach (var r in results)
            {
                Console.WriteLine(r);
            }

            Console.ReadLine();


            Console.WriteLine("Final Task: ");
            string outputString = "";
            for (int i = 0; i < 10; i++)
            {
                outputString += Fibonacci(i) + " ";
            }
            Console.WriteLine(outputString);

            Console.ReadLine();
            TaskExtra();
        }


        private static void Task1()
        {
            Console.WriteLine("Task 1:");
            Console.WriteLine(-5 + 3);
            // -2  int
        }


        private static void Task2()
        {
            Console.WriteLine("Task 2:");
            Console.WriteLine(-5f + 3f);
            // -2 float
        }

        private static void Task3()
        {
            Console.WriteLine("Task 3:");
            Console.WriteLine(3 / 4);
            // 0 int
        }

        private static void Task4()
        {
            Console.WriteLine("Task 4:");
            Console.WriteLine(0.3333 * 3 == 1);
            // false bool

        }

        private static void Task5()
        {
            Console.WriteLine("Task 5:");
            Console.WriteLine(2 / 4 == 3 / 4);
            // 2/4 == 3/4
            // 0 == 0
            // True
        }


        private static void Task6()
        {
            Console.WriteLine("Task 6:");
            Console.WriteLine("35" + 2);

            // "35" + "2"
            // "352" String
        }

        private static void Task7()
        {
            Console.WriteLine("Task 7:");

            // 3/4 = 0
            // 3%4 = 3 
            // 2%4 = 2
            // 5%4 = 1

            int a = 20 % 2;
            Console.WriteLine(a == 0 || a != 0);
            // True bool
        }

        private static void Task8()
        {
            Console.WriteLine("Task 8:");

            bool condition = !(12 - 15 > 0);
            //!(12 - 15 > 0)
            //!(-3 > 0)
            //!(False)
            //True

            if (condition)
            {
                string s = "Hello!";
            }
            else
            {
                Console.WriteLine("World!");
            }
        }

        private static int Factorial(int input)
        {
            int result = 1;
            for (int i = 1; i <= input; i++)
            {
                result *= i; // same as result = result * i;
            }

            return result;

            //returns factorial
        }
        //3! = 1*2*3
        //4! = 1*2*3*4
        //5! = 1*2*3*4*5


        //parameter int[] = {3}
        // In: {2,5} Out: {2!, 5!}
        private static List<int> Factorial(int[] parameter)
        {
            List<int> outputs = new List<int>();

            foreach (var input in parameter)
            {
                outputs.Add(Factorial(input));
            }

            return outputs;
        }

        //to rename together in class
        private static int Fibonacci(int x)
        {
            if (x <= 1)
                return 1;

            return Fibonacci(x - 1) + Fibonacci(x - 2);
        }



        private static void TaskExtra()
        {
            Console.WriteLine("Extra: ");
            Console.WriteLine((Factorial(Factorial(3))).ToString() + 10);
            Console.ReadLine();
        }

        //Tasks:
        /*
         * Make a function that will return a greeting statement that uses an input; your program should return,
         * Greeting("Luca") // returns "Hello, Luca how are you doing today?".
         */

        private static string Greeting(string name)
        {
            return "Hello, " + name + " how are you doing today?";
            
            //other valid results
            return $"Hello, {name} how are you doing today?";
            return string.Format("Hello, {0} how are you doing today?", name);

            string result = "Hello, " + name + " how are you doing today?";
            return result;
        }

        /* Write a function called RepeatString which repeats the given string src exactly count times.
           RepeatString(6, "I") // "IIIIII"
        */

        private static string RepeatString(int times, string src)
        {
            string result = "";
            for (int i = 0; i < times; i++)
            {
                result += src;
            }
            return result;
        }

        /* Your task is to create function IsDividedBy to check if an integer number is divisible by each out of two arguments.
        * IsDevidedBy(10, 2 ,-5) // True (because 10 is both divisible by 2 and by -5;
        */

        private static bool IsDividedBy(int i1, int divisor1, int divisor2)
        {
            return (i1 % divisor1 == 0 && i1 % divisor2 == 0);
        }

        /*
        * Get Planet Name By ID
        *  GetPlanetName(3); // Earth
        */

        private static string GetPlanetName(int planetID)
        {
            string[] planetNames = { "Mercury", "Venus", "Earth", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune" };

            if(planetID > 0 && planetID < 8)
                return planetNames[planetID];
            else
                return "";
        }
    }
}
