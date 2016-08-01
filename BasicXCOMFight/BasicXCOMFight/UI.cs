using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicXCOMFight
{
    class UI
    {
        public int half_cover = 30;
        public int full_cover = 45;
        public bool loop = true;
        public int input;
        public int close_range = 7;
        public int distance;

        // UI: USER INTERFACE
        public void showUI(int turn, int distance, int half_cover, int full_cover, Unit player, Unit enemy)
        {
            Console.WriteLine();
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("|==========================|");
            Console.WriteLine("        XCOM ACTIVITY       ");
            Console.WriteLine("| Turn: {0} | Distance: {1}", turn, distance);
            Console.WriteLine("|==========================|");
            Console.WriteLine();
            Console.WriteLine("Name: {0}", player.name);
            Console.Write("HP: {0}/{1}", player.hp, player.maxHP);
            Console.Write("   [ ");
            for (int i = 1; i <= player.hp; i++) Console.Write("* ");
            for (int i = 1; i <= player.maxHP - player.hp; i++) Console.Write("= ");
            Console.Write("]\n");
            Console.WriteLine("Aim: {0}", player.aim);
            Console.WriteLine("Defense: {0}", player.def);
            if (player.cover == half_cover) Console.WriteLine("Cover: Half Cover (+{0} Defense)", half_cover);
            else if (player.cover == full_cover) Console.WriteLine("Cover: Full Cover (+{0} Defense)", full_cover);
            else if (player.cover == half_cover * 2) Console.WriteLine("Cover: Half Cover (+{0} Defense) | Hunkered (+{0} Defense)", half_cover);
            else if (player.cover == full_cover * 2) Console.WriteLine("Cover: Full Cover (+{0} Defense) | Hunkered (+{0} Defense)", full_cover);
            Console.WriteLine();
            Console.WriteLine("|=========== VS ===========|");
            Console.WriteLine();
            Console.WriteLine("Name: {0}", enemy.name);
            Console.Write("HP: {0}/{1}", enemy.hp, enemy.maxHP);
            Console.Write("   [ ");
            for (int i = 1; i <= enemy.hp; i++) Console.Write("* ");
            for (int i = 1; i <= enemy.maxHP - enemy.hp; i++) Console.Write("= ");
            Console.Write("]\n");
            Console.WriteLine("Defense: {0}", enemy.def);
            if (enemy.cover == half_cover) Console.WriteLine("Cover: Half Cover (+{0} Defense)", half_cover);
            else if (enemy.cover == full_cover) Console.WriteLine("Cover: Full Cover (+{0} Defense)", full_cover);
            else if (enemy.cover == half_cover * 2) Console.WriteLine("Cover: Half Cover (+{0} Defense) | Hunkered (+{0} Defense)", half_cover);
            else if (enemy.cover == full_cover * 2) Console.WriteLine("Cover: Full Cover (+{0} Defense) | Hunkered (+{0} Defense)", full_cover);
            Console.Write("OVERWATCH: ");
            if (enemy.perks[1] == true) Console.Write("YES\n");
            else Console.Write("NO\n");
        }

        // UI: HELP - PERKS LIST
        public void viewPresentPerks(Unit player, Unit enemy)
        {
            Console.Clear();
            for (int i = 2; i < player.perks.Length; i++)
            {
                if (player.perks[i] == true)
                {
                    Console.WriteLine("|==========================|");
                    Console.WriteLine("|       PLAYER PERKS       |");
                    Console.WriteLine("|==========================|");
                    Console.WriteLine();
                    break;
                }
            }
            if (player.perks[2] == true)
            {
                Console.WriteLine("Opportunist - Eliminates accuracy penalty on Overwatch shots.");
            }
            Console.WriteLine();
            for (int i = 2; i < enemy.perks.Length; i++)
            {
                if (enemy.perks[i] == true)
                {
                    Console.WriteLine("|==========================|");
                    Console.WriteLine("|        ENEMY PERKS       |");
                    Console.WriteLine("|==========================|");
                    Console.WriteLine();
                    break;
                }
            }
            if (enemy.perks[2] == true)
            {
                Console.WriteLine("Opportunist - Eliminates accuracy penalty on Overwatch shots.");
            }
            Console.WriteLine();
            Console.WriteLine("Press any key to go back: ");
            Console.ReadKey();
        }

        // UI: USER COMMANDS
        public void showCommand(int hit_chance, int crit)
        {
            System.Threading.Thread.Sleep(500);
            Console.WriteLine();
            Console.WriteLine("|========= COMMAND ========|");
            Console.WriteLine("1. Take a Shot - Hit Chance: {0}% | Crit Chance: {1}%",
                              hit_chance, crit);
            Console.WriteLine("2. Overwatch");
            Console.WriteLine("3. Move Forward - 1 Action");
            Console.WriteLine("4. Hunker Down");
            Console.WriteLine("0. View Perks");
            Console.WriteLine("|==========================|");
        }
        // UI: INPUT COMMANDS
        public int inputCommand()
        {
            Console.Write("Command: ");
            int input = Convert.ToInt32(Console.ReadLine());
            return input;
        }
        // UI: SLOW PRINT
        public void slowprint(string text, int scroll_speed)
        {            
            System.Threading.Thread.Sleep(500);
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(text[i]);
                System.Threading.Thread.Sleep(scroll_speed);
            }
        }
        // UI: XCOM WINS
        public void xcomWin(string enemy_name)
        {
            Console.WriteLine();
            Console.WriteLine("{0} is down. XCOM WINS!", enemy_name);
            Console.ReadKey();
        }
        // UI: ALIEN WINS
        public void alienWin(string player_name)
        {
            Console.WriteLine();
            Console.WriteLine("{0} is down. ALIENS WIN!", player_name);
            Console.ReadKey();
        }
    }   // END of Class UI
}   // END of namespace BasicXCOMFight
