using System;
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
            // CREATE CHARACTER
            Units player = new Units();         
            player.getPlayer();

            // SELECT ENEMY
            Units enemy = new Units();
            enemy.enemy_Sectoid();              

            // UI AND ACTIONS INSTANCE
            UIAndActions ui = new UIAndActions();

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
                ui.showUI(turn,
                          distance,
                          half_cover,
                          full_cover,
                          player.name, player.hp, player.maxHP, player.aim, player.def, player.cover,
                          enemy.name,   enemy.hp,  enemy.maxHP,  enemy.aim,  enemy.def,  enemy.cover);


                while (loop == true)
                {
                    // PRE-CONDITIONS
                    if (distance >= 7) hit_chance = player.aim - enemy.def - enemy.cover + ((18 - distance) * 2);
                    else hit_chance = player.aim - enemy.def - enemy.cover + 22 + ((7 - distance) * 4);

                    // SHOW COMMANDS                    
                    ui.showCommand(hit_chance, player.crit);

                    // INPUT COMMANDS
                    input = ui.inputCommand();

                    // XCOM TURN
                    if (input == 1)         // IF: Take a Shot
                    {
                        dice = rnd.Next(1, 100);
                        if (dice <= hit_chance)     // IF: Shot hits
                        {
                            damage = rnd.Next(1, 3);
                            ui.takeShot(player.name, enemy.name, damage);
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
                        ui.hunkerDown(player.name);
                        player.cover *= 2;
                        p_hunker = true;
                        break;
                    }
                    else if (input == 3)    // IF: Move Up
                    {
                        if (alreadyMoved == false)
                        {
                            distance--;
                            ui.moveUp(player.name, distance);
                            alreadyMoved = true;
                        }
                        else
                        {
                            distance--;
                            ui.moveUp(player.name, distance);
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
                                Console.Write("ALIEN ACTIVITY! ");
                                ui.takeShot(enemy.name, player.name, damage);
                                player.hp -= damage;
                                break;
                            }
                            else                        //IF: Shot misses
                            {
                                Console.Write("ALIEN ACTIVITY! ");
                                ui.shotMissed(enemy.name, player.name);
                                break;
                            }
                        }
                        else                            // IF (2) is false, then ... (3)
                        {
                            act_chance = 50;
                            dice = rnd.Next(1, 100);
                            if (dice <= act_chance)     // (3) Hunker Down
                            {
                                Console.Write("ALIEN ACTIVITY! ");
                                ui.hunkerDown(enemy.name);
                                enemy.cover *= 2;
                                e_hunker = true;
                                break;
                            }
                            else
                            {
                                Console.Write("ALIEN ACTIVITY! ");
                                distance--;
                                ui.moveUp(enemy.name, distance);                                
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

