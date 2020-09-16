using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PracticalLessons.GPR4100
{
    public class Lesson1
    {
        //Funktion F(x) = x
        // y = x

        //Eine Funktion namens "F" die einen Float zurück gibt und einen float parameter "x" nimmt 
        static float F(float x)
        {
            return x * x;
        }

        //Funktion oder Methode
        //Eine Funktion namens "Main" die nichts zurück gibt und keine parameter hat
        public static void Main()
        {
            float xKoordinate = 0;
            float yKoordinate = 0;

            while (true)
            {
                if (IsAlive(xKoordinate, yKoordinate))
                {
                    InputHandling(ref xKoordinate, ref yKoordinate);
                    Rendering(xKoordinate, yKoordinate);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Game Over");
                    Console.ReadKey();
                }      
            }

        }

        private static void Rendering(float xKoordinate, float yKoordinate)
        {
            Console.Clear();
            for (int y = 0; y < 20; y++)
            {
                for (int x = 0; x < 20; x++)
                {
                    if (x == xKoordinate && y == yKoordinate)
                    {
                        Console.Write("P");
                    }
                    else
                    {
                        Console.Write("_");
                    }

                }
                Console.WriteLine();
            }
        }

        static private void InputHandling(ref float x, ref float y)
        {
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.D)
            {
                x = MoveRight(x);
            }
            else if (key.Key == ConsoleKey.A)
            {
                x = MoveLeft(x);
            }
            else if (key.Key == ConsoleKey.W)
            {
                y = MoveUp(y);
            }
            else if (key.Key == ConsoleKey.S)
            {
                y = MoveDown(y);
            }
        }


        static private float MoveRight(float position)
        {
            return position + 1;
        }

        static private float MoveLeft(float position)
        {
            return position - 1;
        }

        static private float MoveUp(float vertical)
        {
            return vertical - 1;
        }

        static private float MoveDown(float vertical)
        {
            return vertical + 1;
        }

        static bool IsAlive(float x, float y)
        {
            return x < 20 && y < 20;
        }

        private static void Task1( int x)
        {
            Console.WriteLine("Task 1:");
            Console.WriteLine(x + 3); 
        }

        //Task 1:
        // -2 Integer

        private static void Task2()
        {
            Console.WriteLine("Task 2:");
            Console.WriteLine(-5f + 3f);
        }

        //Task 2
        // -2 Float

        private static void Task3()
        {
            Console.WriteLine("Task 3:");
            Console.WriteLine(3 / 4);
            Console.WriteLine(3f / 4f);
            // Integer rechnung: 3 / 4 = 0 mit rest 3
            // Float rechnung: 3 / 4 = 0.75 
        }

        //Task 3:
        // 0 Integer

        private static void Task4()
        {
            Console.WriteLine("Task 4:");
            Console.WriteLine(0.33333f * 3 == 1);
            // 0.33333f *3 = 0.999999f ungleich 1
        }

        //Task 4:
        // False Bool

        private static void Task5()
        {
            Console.WriteLine("Task 5:");
            Console.WriteLine(2 / 4 == 3 / 4);
            // 2/4 == 3/4 -> 0 == 0 -> True
            // True Bool
        }


        private static void Task6()
        {
            Console.WriteLine("Task 6:");
            Console.WriteLine("Ergebnis: " + true);
            // "35" + " -> "35" + "2" -> "352"
            // 352 String
        }


        private static void Task7()
        {
            Console.WriteLine("Task 7:");
            bool links = 20 % 2 == 0;
            //20 % 2 == -> 0 == 0 
            bool rechts = !links;
            Console.WriteLine(links || rechts);
            // ! -> logisches nicht (not)
            // && -> logisches und
            // || -> logisches oder

            // 20 % 2 == 0 || 20 % 2 != 0 
            // 0 == 0 || 0 != 0
            // true || false
            // true

            //  True Bool
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

            // (!(12 - 15 > 0))
            // (!(-3 >0))
            // (!(False))
            // (True)

            // Task 8:
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
        // Was ist Task9(4)?
        //
        // F(x) -> 

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
     */
}