using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterBattleSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Monster m1 =  GetMonsterFromUser();

            Console.WriteLine("Monster 1 done! Now defining monster 2..");
            Monster m2 = GetMonsterFromUser(); 
            Console.ReadLine();

        }

        private static Monster GetMonsterFromUser()
        {
            Console.WriteLine("Creating Monster:");
            Race race1 = GetRaceFromUser();

            Console.WriteLine("HP:");
            float hp = GetNumberFromUser();

            Console.WriteLine("Attack Power:");
            float ap = GetNumberFromUser();

            Console.WriteLine("Defensive Power:");
            float dp = GetNumberFromUser();

            Console.WriteLine("Speed:");
            float sp = GetNumberFromUser();


            Monster monster1 = new Monster();
            monster1.Race = race1;
            monster1.Health = hp;
            monster1.AttackPower = ap;
            monster1.DefensePower = dp;
            monster1.Speed = sp;

            return monster1;
        }

        private static float GetNumberFromUser()
        {
            int result;
            do
            {
                Console.WriteLine("Please pick a Number between 0 and 9");
                ConsoleKey key = Console.ReadKey().Key;
                result = (int)key - (int)ConsoleKey.D0;

            }
            while (result < 0 || result > 9);

            Console.WriteLine();
            Console.WriteLine("Selected: " + result);
            return result;
        }

        private static Race GetRaceFromUser()
        {
            int keyRace;
            do
            {
                Console.WriteLine("Please pick a Race: (1=Orc, 2=Troll, 3=Goblin)");
                ConsoleKey key = Console.ReadKey().Key;
                keyRace = (int)key - (int)ConsoleKey.D0;

            }
            while (keyRace < 0 || keyRace > 3);

            Race raceInput;
            raceInput = (Race)keyRace;
            
            Console.WriteLine();
            Console.WriteLine("You selected a " + raceInput);
            return raceInput;
        }

        private static void TestAttack()
        {
            Monster monster1 = new Monster();
            Monster monster2 = new Monster();

            monster1.AttackPower = 5;
            monster2.DefensePower = 2;
            monster2.Health = 10;

            monster1.Attack(monster2);

            Console.WriteLine("TestAttack Result: " + (monster2.Health == 7));
        }
    }
}
