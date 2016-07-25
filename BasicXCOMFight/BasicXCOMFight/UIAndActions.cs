using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicXCOMFight
{
    class UIAndActions
    {
        // UI: USER COMMANDS
        public void showCommand(int hit_chance, int crit)
        {
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine();
            Console.WriteLine("|========= COMMAND ========|");
            Console.WriteLine("1. Take a Shot - Hit Chance: {0}% | Crit Chance: {1}%",
                              hit_chance, crit);
            Console.WriteLine("2. Hunker Down");
            Console.WriteLine("3. Move Up - Costs 1 Action");
            Console.WriteLine("|==========================|");
        }
        // ACTION: TAKING SHOT
        public void takeShot(string user_name, string target_name, int damage)
        {
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine();
            Console.WriteLine("{0} took an accurate shot and {1} took {2} damage.", user_name, target_name, damage);
        }
        // ACTION: MISSING SHOT
        public void shotMissed(string user_name, string target_name)
        {
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine();
            Console.WriteLine("{0} took a shot at {1}. It is a miss.", user_name, target_name);
        }
        // ACTION: MOVING UP
        public void moveUp(string user_name)
        {
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine();
            Console.WriteLine("{0} closed in. Distance decreased by 1.", user_name);
        }

    }   // END of Class UIAndActions
}   // END of namespace BasicXCOMFight
