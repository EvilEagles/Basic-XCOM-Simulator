using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicXCOMFight
{
    class UIAndActions
    {
        // INSTANCING RANDOM CLASS
        Random rnd = new Random();

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


        // ACTION: TAKING SHOT
        public void takeShot(Unit user, Unit target, int hitChance)
        {
            int dice = rnd.Next(1, 100);
            if (dice <= hitChance)     // IF: Shot hits
            {
                int damage = rnd.Next(1, 3);
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine();
                Console.WriteLine("{0} took an accurate shot and {1} took {2} damage.", user.name, target.name, damage);
                target.hp -= damage;
            }
            else                        // IF: Shot misses
            {
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine();
                Console.WriteLine("{0} took a shot at {1}. It is a miss.", user.name, target.name);
            }
        }
        // ACTION: GO INTO OVERWATCH
        public void overwatch(Unit user)
        {
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine();
            Console.WriteLine("{0} went into Overwatch.", user.name);
            user.overwatch = true;
        }
        // ACTION: HUNKER DOWN
        public int hunkerDown(Unit user)
        {
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine();
            Console.WriteLine("{0} hunkered down, doubling cover bonus. (+{1} Defense)", user.name, user.cover);
            int hunker = user.cover * 2;
            return hunker;
        }
        // ACTION: MOVING UP
        public bool moveUp(Unit user, int distance, int half_cover, int full_cover)
        {
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine();
            Random rnd = new Random();
            if (user.alreadyMoved == false)
            {
                int dice = rnd.Next(1, 3);
                if (dice == 1)
                {
                    Console.WriteLine("{0} moves forward to Full Cover.", user.name);                    
                    user.cover = full_cover;
                }
                else
                {
                    Console.WriteLine("{0} moves forward to Half Cover.", user.name);                    
                    user.cover = half_cover;
                }
                Console.WriteLine("Distance decreased by 1. Current distance: {0}", distance);
                user.alreadyMoved = true;
                return true;
            }
            else
            {
                int dice = rnd.Next(1, 3);
                if (dice == 1)
                {
                    Console.WriteLine("{0} moves forward to Full Cover.", user.name);
                    user.cover = full_cover;
                }
                else
                {
                    Console.WriteLine("{0} moves forward to Half Cover.", user.name);
                    user.cover = half_cover;
                }
                Console.WriteLine("Distance decreased by 1. Current distance: {0}", distance);
                user.alreadyMoved = false;
                return false;
            }
        }

        // CALCULATION: CALCULATING HIT CHANCE
        public int calculateHitChance(int distance, int close_range, Unit user, Unit target)
        {
            int hit_chance;
                if (distance >= close_range) hit_chance = user.aim - target.def - target.cover + ((18 - distance) * 2);
                else hit_chance = user.aim - target.def - target.cover + 22 + ((7 - distance) * 4);
                return hit_chance;
        }
        // CALCULATION: CALCULATING HIT CHANCE INFLUENCE
        public double calculateTakeShot_influence(int hitChance, Unit user, Unit target)
        {
            double influence = 0;
            if (hitChance <= user.hitChanceCheck)
                influence = 0.5;
            else if (hitChance > user.hitChanceCheck && hitChance <= user.hitChanceCheck + 20)
                influence = 0.6;
            else if (hitChance > user.hitChanceCheck + 20 && hitChance <= user.hitChanceCheck + 40)
                influence = 0.7;
            else if (hitChance > user.hitChanceCheck + 40 && hitChance <= user.hitChanceCheck + 60)
                influence = 0.8;
            else
                influence = 0.9;
            if (user.alreadyMoved == true) influence += 0.05;
            if (user.hp <= user.maxHP / 3) influence -= 0.3;
            if (target.hp <= target.maxHP / 3) influence += 0.2;
            return influence;
        }
        // CALCULATION: CALCULATING HUNKERING INFLUENCE
        public double calculateHunkering_influence(int takeShotPercent, Unit user, int half_cover)
        {
            double influence = 0;
            if (takeShotPercent <= 50) influence = 0.5;
            if (user.hp <= user.maxHP / 3) influence += 0.4;
            if (user.cover == half_cover) influence -= 0.2;
            return influence;
        }
        // CALCULATION: CALCULATING MOVING INFLUENCE
        public double calculateMoving_influence(int takeShotPercent, Unit user, int full_cover)
        {
            double influence = 1;
            if (user.cover == full_cover) influence -= 0.8;
            if (takeShotPercent >= 50) influence -= 0.5;
            else influence += 0.3;
            if (user.alreadyMoved == true) influence -= 0.2;
            return influence;
        }
        // CALCULATION: CHECK IF UNIT IS HUNKERED, IF YES THEN UNHUNKER
        public void unhunker(Unit user)
        {
            if (user.hunker == true)
            {
                user.cover /= 2;
                user.hunker = false;
            }
        }

    }   // END of Class UIAndActions
}   // END of namespace BasicXCOMFight
