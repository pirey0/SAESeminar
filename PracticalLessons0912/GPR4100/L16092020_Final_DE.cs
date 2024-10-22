﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PracticalLessons.GPR4100
{
    public class Lesson1
    {
        public static void Main()
        {
            string greeting = Greeting("Luca");
            Console.WriteLine(greeting);

            Console.ReadLine();
            TaskExtra();
            Console.ReadLine();

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
                outputString += TaskFinal(i) + " ";
            }
            Console.WriteLine(outputString);

            Console.ReadLine();
            TaskExtra();
        }


        #region GameTest
        //Funktion F(x) = x
        // y = x

        //Eine Funktion namens "F" die einen Float zurück gibt und einen float parameter "x" nimmt 
        static float F(float x)
        {
            return x * x;
        }

        public static void RunGame()
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

        #endregion

        private static void Task1()
        {
            Console.WriteLine("Task 1:");
            Console.WriteLine(-5 + 3);
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
        private static int Factorial(int input)
        {
            int result = 1;
            for (int i = 1; i <= input; i++)
            {
                result *= i;
            }

            return result;
        }


        // Was ist Task9(4)?
        //
        // F(x) -> 

        //Simples Programm mit Verzweigungen, Rechenoperationen. Welche Zahl ist größer? Taschenrechner
        // Welche Zahl ist größer?
        //Taschenrechner + - * / > < >= <=
        //Task 9 
        // Summe aller kleinen
        // Factorial

        //try catch

        //to rename together in class
        private static List<int> Factorial(int[] inputs)
        {
            List<int> outputs = new List<int>();

            foreach (var input in inputs)
            {
                outputs.Add(Factorial(input));
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
            int[] array = new int[20];

            for (int i = 0; i < array.Length; i++)
            {
                int factorial = Factorial(i);

                if (factorial > 0)
                {
                    array[i] = factorial;
                }
                else
                {
                    array[i] = -1;
                }
            }

            Console.WriteLine((Factorial(Factorial(3))).ToString() + ArrayToString(array));
            Console.ReadLine();
        }

        private static string ArrayToString(int[] array)
        {
            string result = "(";

            for (int i = 0; i < array.Length; i++)
            {
                result += array[i].ToString() + ", ";
            }

            result += ")";

            return result;
        }

        private static string Greeting(string name)
        {
            return "Hallo, " + name + " wie geht es dir?";
        }

        private static string RepeatString(string input, int repeatCount)
        {
            string result = "";

            for (int i = 0; i < repeatCount; i++)
            {
                result = result + input;
            }
            return result;
        }

    }

    //Possible task Examples:
    /* Schreibe eine Methode die einen Gruß zurück gibt und einen Namen als parameter nimmt.
     * Beispiel:
     * Greeting("Luca") // "Hallo, Luca wie geht es dir?"
     * 
     * Screibe eine Methode namens "RepeatString" die einen gegebenen string x mal wiederholt.
     * Beispiel:
     * RepeatString("I", 6) //"IIIIII"
     * RepeatString("Hallo", 3) //"HalloHalloHallo"
     * 
     * Your task is to create function IsDivideBy to check if an integer number is divisible by each out of two arguments.
     * IsDevidedBy(10, 2 ,-5) // True (because 10 is both divisible by 2 and by -5;
     * 
     * Get Planet Name By ID
     * GetPlanetName(3); // Earth
     *  
     */
}