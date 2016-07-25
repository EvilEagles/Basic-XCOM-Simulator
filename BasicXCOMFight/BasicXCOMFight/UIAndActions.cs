using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicXCOMFight
{
    class UIAndActions
    {
        // UI: USER INTERFACE
        public void showUI(int turn, 
                           int distance, 
                           int half_cover, 
                           int full_cover,
                           string player_name, int player_hp, int player_maxHP, int player_aim, int player_def, int player_cover,
                           string enemy_name, int enemy_hp, int enemy_maxHP, int enemy_aim, int enemy_def, int enemy_cover)
        {
            Console.WriteLine();
            System.Threading.Thread.Sleep(1500);
            Console.WriteLine("|==========================|");
            Console.WriteLine("        XCOM ACTIVITY       ");
            Console.WriteLine("| Turn: {0} | Distance: {1}", turn, distance);
            Console.WriteLine("|==========================|");
            Console.WriteLine();
            Console.WriteLine("Name: {0}", player_name);
            Console.WriteLine("HP: {0}/{1}", player_hp, player_maxHP);
            Console.WriteLine("Aim: {0}", player_aim);
            Console.WriteLine("Defense: {0}", player_def);
            if (player_cover == half_cover) Console.WriteLine("Cover: Half Cover (+{0} Defense)", half_cover);
            else Console.WriteLine("Cover: Full Cover (+{0} Defense)", full_cover);
            Console.WriteLine();
            Console.WriteLine("|=========== VS ===========|");
            Console.WriteLine();
            Console.WriteLine("Name: {0}", enemy_name);
            Console.WriteLine("HP: {0}/{1}", enemy_hp, enemy_maxHP);
            Console.WriteLine("Aim: {0}", enemy_aim);
            Console.WriteLine("Defense: {0}", enemy_def);
            if (enemy_cover == half_cover) Console.WriteLine("Cover: Half Cover (+{0} Defense)", half_cover);
            else Console.WriteLine("Cover: Full Cover (+{0} Defense)", full_cover);
        }
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
        // UI: INPUT COMMANDS
        public int inputCommand()
        {
            Console.Write("Command: ");
            int input = Convert.ToInt32(Console.ReadLine());
            return input;
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
        // ACTION: HUNKER DOWN
        public void hunkerDown(string user_name)
        {
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine();
            Console.WriteLine("{0} hunkered down, doubling cover bonus.", user_name);
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
