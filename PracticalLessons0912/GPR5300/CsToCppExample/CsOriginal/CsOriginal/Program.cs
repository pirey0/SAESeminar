using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine engine = new Engine(10);

            while (true)
            {
                Console.Clear();
                string draw = engine.Draw();
                Console.WriteLine(draw);


                var key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.A:
                        engine.Move(-1, 0);
                        break;

                    case ConsoleKey.D:
                        engine.Move(1, 0);
                        break;

                    case ConsoleKey.W:
                        engine.Move(0, -1);
                        break;

                    case ConsoleKey.S:
                        engine.Move(0, 1);
                        break;
                }
            }
        }
    }
}
