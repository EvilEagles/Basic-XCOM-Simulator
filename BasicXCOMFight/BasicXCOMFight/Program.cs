using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicXCOMFight
{
    class Program : Unit
    {
        static void Main(string[] args)
        {
            // CREATE CHARACTER
            Unit player = new Unit();         
            player.getPlayer();

            // SELECT ENEMY
            Unit enemy = new Unit();
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
                if (player.hp <= 0)     // End Program if Player HP <= 0
                {
                    ui.alienWin(player.name);
                    break;
                }
                if (p_hunker == true)   // Check if unit hunkered last turn, if yes then un-hunker
                {
                    player.cover /= 2;
                    p_hunker = false;
                }


                // PRINTING USER INTERFACE
                ui.showUI(turn, distance, half_cover, full_cover, player, enemy);

                // BATTLE SCENE
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
                        player.cover = ui.hunkerDown(player.name, player.cover);
                        p_hunker = true;
                        break;
                    }
                    else if (input == 3)    // IF: Move Up
                    {
                        if (alreadyMoved == false)
                        {
                            distance--;
                            player.cover = ui.moveUp(player.name, distance, half_cover, full_cover);
                            alreadyMoved = true;
                        }
                        else
                        {
                            distance--;
                            player.cover = ui.moveUp(player.name, distance, half_cover, full_cover);
                            break;
                        }
                    }
                }   // END of Loop: XCOM Activity

                // ALIEN TURN
                ui.alienActivity();

                // PRE-CONDITIONS
                if (enemy.hp <= 0)      // Check if Enemy's HP <= 0. If yes then end battle
                {
                    ui.xcomWin(enemy.name);
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
                    if (alreadyMoved == true) act_chance = 20;
                    else act_chance = 0;
                    if (hit_chance > 40 + act_chance)   // AI: If hit chance is higher than X% (1)
                    {
                        act_chance = 70;
                        dice = rnd.Next(1, 100);
                        if (dice <= act_chance)     // AI: (1) Roll a dice to determine decision. If yes, take a shot (2)
                        {
                            dice = rnd.Next(1, 100);
                            if (dice <= hit_chance)     //IF: Shot hits
                            {
                                damage = rnd.Next(1, 3);
                                ui.takeShot(enemy.name, player.name, damage);
                                player.hp -= damage;
                                break;
                            }
                            else                        //IF: Shot misses
                            {                                
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
                                enemy.cover = ui.hunkerDown(enemy.name, enemy.cover);
                                e_hunker = true;
                                break;
                            }
                            else                        // (3) Move Up
                            {
                                if (alreadyMoved == false)
                                {
                                    distance--;
                                    enemy.cover = ui.moveUp(enemy.name, distance, half_cover, full_cover);
                                    alreadyMoved = true;
                                }
                                else
                                {
                                    distance--;
                                    enemy.cover = ui.moveUp(enemy.name, distance, half_cover, full_cover);
                                }
                            }
                        }
                    }
                    else                                // If (1) is false, then Hunker down
                    {
                        enemy.cover = ui.hunkerDown(enemy.name, enemy.cover);
                        e_hunker = true;
                        break;
                    }
                }   // End of Loop: Alien Activity
            }   // End of Loop: Turn
        }   // End of Method: Main
    }    // End of Class: Program
}    // End of Namespace  

