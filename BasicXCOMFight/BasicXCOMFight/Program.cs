﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicXCOMFight
{
    class Program : Units
    {
        static void Main(string[] args)
        {
            Units player = new Units();         // Creating player's character
            player.getPlayer();

            Units enemy = new Units();
            enemy.enemy_Sectoid();              // Select enemy

            // OPERATIONAL VARIABLES
            #region
            int input;
            int hit_chance;
            bool p_hunker = false;
            bool e_hunker = false;
            bool alreadyMoved = false;
            bool loop = true;

            // RNG SYSTEM
            Random rnd = new Random();
            int dice;
            int act_chance;
            int damage;
            int distance = rnd.Next(10, 18);
            #endregion
            for (int turn = 1; turn >= 1; turn++)
            {

                // PRE-CONDITIONS
                #region               
                if (player.hp <= 0)     // End Program if Player HP <= 0
                {
                    Console.WriteLine();
                    Console.WriteLine("{0} is down. ALIENS WIN!", player.name);
                    Console.ReadKey();
                    break;
                }
                if (p_hunker == true)   // Check if unit hunkered last turn, if yes then un-hunker
                {
                    player.cover /= 2;
                    p_hunker = false;
                }
                #endregion
                // PRINTING USER INTERFACE
                #region
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
                else Console.WriteLine("Cover: Full Cover (+{0} Defense)", full_cover);
                Console.WriteLine();
                Console.WriteLine("|=========== VS ===========|");
                Console.WriteLine();
                Console.WriteLine("Name: {0}", enemy.name);
                Console.WriteLine("HP: {0}/{1}", enemy.hp, enemy.maxHP);
                Console.WriteLine("Aim: {0}", enemy.aim);
                Console.WriteLine("Defense: {0}", enemy.def);
                if (enemy.cover == half_cover) Console.WriteLine("Cover: Half Cover (+{0} Defense)", half_cover);
                else Console.WriteLine("Cover: Full Cover (+{0} Defense)", full_cover);
                #endregion

                while (loop == true)
                {
                    // PRECONDITIONS
                    #region
                    if (distance >= 7) hit_chance = player.aim - enemy.def - enemy.cover + ((18 - distance) * 2);
                    else hit_chance = player.aim - enemy.def - enemy.cover + 22 + ((7 - distance) * 4);
                    #endregion

                    UIAndActions ui = new UIAndActions();
                    ui.showCommand(hit_chance, player.crit);

                    // INPUT COMMANDS
                    Console.Write("Command: ");
                    input = Convert.ToInt32(Console.ReadLine());

                    // XCOM TURN
                    if (input == 1)         // IF: Take a Shot
                    {
                        dice = rnd.Next(1, 100);
                        if (dice <= hit_chance)     // IF: Shot hits
                        {
                            damage = rnd.Next(1, 3);
                            System.Threading.Thread.Sleep(1000);
                            Console.WriteLine();
                            Console.WriteLine("{0} took an accurate shot and {1} took {2} damage.",
                                                                  player.name, enemy.name, damage);
                            enemy.hp -= damage;
                            break;
                        }
                        else                        // IF: Shot misses
                        {
                            ui.shotMissed(player.name, enemy.name);
                            break;
                        }
                    }
                    else if (input == 2)    // IF: Hunker Down
                    {
                        System.Threading.Thread.Sleep(1000);
                        Console.WriteLine();
                        Console.WriteLine("{0} hunkered down, doubling cover bonus.", player.name);
                        player.cover *= 2;
                        p_hunker = true;
                        break;
                    }
                    else if (input == 3)    // IF: Move Up
                    {
                        if (alreadyMoved == false)
                        {
                            distance--;
                            ui.moveUp(player.name);
                            alreadyMoved = true;
                        }
                        else
                        {
                            distance--;
                            ui.moveUp(player.name);
                            break;
                        }
                    }
                }
                // ALIEN TURN

                // PRE-CONDITIONS
                if (enemy.hp <= 0)      // Check if Enemy's HP <= 0. If yes then end battle
                {
                    Console.WriteLine();
                    Console.WriteLine("{0} is down. XCOM WINS!", enemy.name);
                    Console.ReadKey();
                    break;
                }
                if (e_hunker == true)   // Check if unit hunkered last turn. If yes then un-hunker
                {
                    enemy.cover /= 2;
                    e_hunker = false;
                }
                alreadyMoved = false;
                while (loop == true)
                {
                    // Calculating Hit Chance influenced by Distance
                    if (distance >= 7) hit_chance = enemy.aim - player.def - player.cover + ((18 - distance) * 2);
                    else hit_chance = enemy.aim - player.def - player.cover + 22 + ((7 - distance) * 4);

                    if (hit_chance >= 40)   // AI: If hit chance is higher than X% (1)
                    {
                        act_chance = 70;
                        dice = rnd.Next(1, 100);
                        if (dice <= act_chance)     // AI: (1) Roll a dice to determine decision. If yes, take a shot (2)
                        {
                            dice = rnd.Next(1, 100);
                            if (dice <= hit_chance)     //IF: Shot hits
                            {
                                damage = rnd.Next(1, 3);
                                System.Threading.Thread.Sleep(1000);
                                Console.WriteLine();
                                Console.WriteLine("ALIEN ACTIVITY! {1} took an accurate shot and {0} took {2} damage.",
                                                                      player.name, enemy.name, damage);
                                player.hp -= damage;
                                break;
                            }
                            else                        //IF: Shot misses
                            {
                                System.Threading.Thread.Sleep(1000);
                                Console.WriteLine();
                                Console.WriteLine("ALIEN ACTIVITY! {1} took a shot at {0}. It is a miss.", player.name, enemy.name);
                                break;
                            }
                        }
                        else                            // IF (2) is false, then ... (3)
                        {
                            act_chance = 50;
                            dice = rnd.Next(1, 100);
                            if (dice <= act_chance)     // (3) Hunker Down
                            {
                                System.Threading.Thread.Sleep(1000);    
                                Console.WriteLine();
                                Console.WriteLine("ALIEN ACTIVITY! {0} hunkered down, doubling cover bonus.", enemy.name);
                                enemy.cover *= 2;
                                e_hunker = true;
                                break;
                            }
                            else
                            {
                                distance--;
                                System.Threading.Thread.Sleep(1000);
                                Console.WriteLine();
                                Console.WriteLine("{0} closed in. Distance decreased by 1.", player.name);
                                alreadyMoved = true;
                            }
                        }
                    }
                    else                                // If (1) is false, then Hunker down
                    {
                        System.Threading.Thread.Sleep(1000);
                        Console.WriteLine();
                        Console.WriteLine("ALIEN ACTIVITY! {0} hunkered down, doubling cover bonus.", enemy.name);
                        enemy.cover *= 2;
                        e_hunker = true;
                        break;
                    }
                }   // End of Loop: Alien Activity
            }   // End of Loop: Turn
        }   // End of Method: Main
    }    // End of Class: Program
}    // End of Namespace  

