using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MonsterBattleSimulator
{
    class Monster
    {
        public float Health;
        public float AttackPower;
        public float DefensePower;
        public float Speed;

        public Race Race;

        public void Attack(Monster target)
        {
            float damage = this.AttackPower - target.DefensePower;

            if (damage < 0)
                damage = 0;

            target.Health -= damage;
        }

        public bool IsAlive()
        {
            return Health > 0;
        }


    }

    //integer with a name
    public enum Race
    {
        Orc = 1,
        Troll,
        Goblin 
    }
}
