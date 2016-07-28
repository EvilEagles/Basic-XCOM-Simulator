using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicXCOMFight
{
    class UI
    {
        public int half_cover = 20;
        public int full_cover = 35;
        public bool loop = true;
        public int input;
        public int close_range = 7;
        Calculation calc = new Calculation();
        public int distance = calc.diceroll(10, 18);

        // UI: USER INTERFACE
        public void showUI(int turn, int distance, int half_cover, int full_cover, Unit player, Unit enemy)
        {
            Console.WriteLine();
            System.Threading.Thread.Sleep(1500);
            Console.WriteLine("|==========================|");
            Console.WriteLine("        XCOM ACTIVITY       ");
            Console.WriteLine("| Turn: {0} | Distance: {1}", turn, distance);
            Console.WriteLine("|==========================|");
            Console.WriteLine();
            Console.WriteLine("Name: {0}", player.name);
            Console.WriteLine("HP: {0}/{1}", player.hp, player.maxHP);
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
            Console.WriteLine("HP: {0}/{1}", enemy.hp, enemy.maxHP);
            Console.WriteLine("Aim: {0}", enemy.aim);
            Console.WriteLine("Defense: {0}", enemy.def);
            if (enemy.cover == half_cover) Console.WriteLine("Cover: Half Cover (+{0} Defense)", half_cover);
            else if (enemy.cover == full_cover) Console.WriteLine("Cover: Full Cover (+{0} Defense)", full_cover);
            else if (enemy.cover == half_cover * 2) Console.WriteLine("Cover: Half Cover (+{0} Defense) | Hunkered (+{0} Defense)", half_cover);
            else if (enemy.cover == full_cover * 2) Console.WriteLine("Cover: Full Cover (+{0} Defense) | Hunkered (+{0} Defense)", full_cover);
            Console.Write("OVERWATCH: ");
            if (enemy.overwatch == true) Console.Write("YES\n");
            else Console.Write("NO\n");
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
            Console.WriteLine("3. Move Forward - 1 Action");
            Console.WriteLine("|==========================|");
        }
        // UI: INPUT COMMANDS
        public int inputCommand()
        {
            Console.Write("Command: ");
            int input = Convert.ToInt32(Console.ReadLine());
            return input;
        }
        // UI: ALIEN ACTIVITY
        public void alienActivity()
        {
            int scroll_speed = 100;
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine();
            string alienActivity = "ALIEN ACTIVITY!";
            for (int i = 0; i < alienActivity.Length; i++)
            {
                Console.Write(alienActivity[i]);
                System.Threading.Thread.Sleep(scroll_speed);
            }
            Console.WriteLine();
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
            Console.WriteLine("{0} is down. XCOM WINS!", player_name);
            Console.ReadKey();
        }
    }   // END of Class UI
}   // END of namespace BasicXCOMFight
