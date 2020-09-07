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
            int result = Task9(4);
            Console.WriteLine(result);
            Console.ReadLine();

            Console.WriteLine("Task 10:");
            int[] primes = { 2,3,5,7,11,13};
            List<int> results = Task10(primes);
            foreach (var r in results)
            {
                Console.WriteLine(r);
            }

            Console.ReadLine();


            Console.WriteLine("Final Task: ");
            string outputString = "";
            for (int i = 0; i < 10; i++)
            {
                outputString += TaskFinal(i) + " ";
            }
            Console.WriteLine(outputString);

            Console.ReadLine();
            TaskExtra();
        }


        private static void Task1()
        {
            Console.WriteLine("Task 1:");
            Console.WriteLine(-5 + 3); 
        }

        private static void Task2()
        {
            Console.WriteLine("Task 2:");
            Console.WriteLine(-5f + 3f);
        }

        private static void Task4()
        {
            Console.WriteLine("Task 3:");
            Console.WriteLine(3 / 4);
        }

        private static void Task3()
        {
            Console.WriteLine("Task 4:");
            Console.WriteLine(0.33333f * 3 == 1);
        }

        private static void Task5()
        {
            Console.WriteLine("Task 5:");
            Console.WriteLine(2 / 4 == 3 / 4);
        }

        private static void Task6()
        {
            Console.WriteLine("Task 6:");
            Console.WriteLine("35" + 2);
        }

        private static void Task7()
        {
            Console.WriteLine("Task 7:");
            Console.WriteLine(20 % 2 == 0 || 20 % 2 != 0);
        }

        private static void Task8()
        {
            Console.WriteLine("Task 8:");

            if ((!(12 - 15 > 0)))
            {
                string s = "Hello!";
            }
            else
            {
                Console.WriteLine("World!");
            }
        }

        //to rename together in class
        private static int Task9(int input)
        {
            int result = 1;
            for(int i = 1; i <=input; i++)
            {
                result *= i;
            }

            return result;
        }

        //to rename together in class
        private static List<int> Task10(int[] inputs)
        {
            List<int> outputs = new List<int>();

            foreach (var input in inputs)
            {
                outputs.Add(Task9(input));
            }

            return outputs;
        }

        //to rename together in class
        private static int TaskFinal(int x)
        {
            if (x <= 1)
                return 1;

            return TaskFinal(x - 1) + TaskFinal(x - 2);
        }


        private static void TaskExtra()
        {
            Console.WriteLine("Extra: ");
            Console.WriteLine((Task9(Task9(3))).ToString() + 10);
            Console.ReadLine();
        }
    }


    //Possible task Examples:
    /*
     * Make a function that will return a greeting statement that uses an input; your program should return,
     *   Greeting("Luca") // "Hello, Luca how are you doing today?".
     * 
     * Write a function called repeat_str which repeats the given string src exactly count times.
        repeatStr(6, "I") // "IIIIII"
     * 
     * Your task is to create function IsDivideBy to check if an integer number is divisible by each out of two arguments.
     *  IsDevidedBy(10, 2 ,-5) // True (because 10 is both divisible by 2 and by -5;
     * 
     * Get Planet Name By ID
     *  getPlanetName(3); // Earth
     *  
     *  
     */
}
