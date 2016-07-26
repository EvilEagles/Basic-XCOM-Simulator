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
            int input;
            int hit_chance;
            bool loop = true;

            // RNG SYSTEM
            Random rnd = new Random();
            int dice;

            int act_chance;
            int distance = rnd.Next(10, 18);
            int close_range = 7;

            // AI SYSTEM
            int alreadyMoved_influence = 20;
            int shootAfterMove_influence = 20;
            for (int turn = 1; turn >= 1; turn++)
            {
                // WIN CHECK            
                if (player.hp <= 0)     
                {
                    ui.alienWin(player.name);
                    break;
                }

                // UNHUNKER IF HUNKERED
                ui.unhunker(player);   // Check if unit hunkered last turn, if yes then un-hunker

                // PRINTING USER INTERFACE
                ui.showUI(turn, distance, half_cover, full_cover, player, enemy);

                // BATTLE SCENE
                while (loop == true)
                {
                    // PRE-CONDITIONS
                    hit_chance = ui.calculateHitChance(distance, close_range, "player", player, enemy);
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
                            ui.takeShot(player, enemy);
                            break;
                        }
                        else                        // IF: Shot misses
                        {
                            ui.shotMissed(player, enemy);
                            break;
                        }
                    }
                    else if (input == 2)    // IF: Hunker Down
                    {
                        ui.hunkerDown(player);
                        break;
                    }
                    else if (input == 3)    // IF: Move Up
                    {
                        if (player.alreadyMoved == false)
                        {
                            distance--;
                            ui.moveUp(player, distance, half_cover, full_cover);
                        }
                        else
                        {
                            distance--;
                            ui.moveUp(player, distance, half_cover, full_cover);
                            break;
                        }
                    }
                }   // END of Loop: XCOM Activity

                // ALIEN TURN
                ui.alienActivity();

                // WIN CHECK
                if (enemy.hp <= 0)      // Check if Enemy's HP <= 0. If yes then end battle
                {
                    ui.xcomWin(enemy.name);
                    break;
                }
                // UNHUNKER IF HUNKERED
                ui.unhunker(enemy);   // Check if unit hunkered last turn. If yes then un-hunker

                enemy.alreadyMoved = false;

                while (loop == true)
                {
                    // Calculating Hit Chance influenced by Distance
                    hit_chance = ui.calculateHitChance(distance, close_range, "enemy", player, enemy);
                    act_chance = ui.AI_alreadyMoved(alreadyMoved_influence, enemy);
                    if (hit_chance > enemy.hitChanceCheck + act_chance)   // AI: If hit chance is higher than X% (1)
                    {
                        act_chance = hit_chance + shootAfterMove_influence;
                        dice = rnd.Next(1, 100);
                        if (dice <= act_chance)     // AI: (1) Roll a dice to determine decision. If yes, take a shot (2)
                        {
                            dice = rnd.Next(1, 100);
                            if (dice <= hit_chance)     //IF: Shot hits
                            {
                                ui.takeShot(enemy, player);
                                break;
                            }
                            else                        //IF: Shot misses
                            {                                
                                ui.shotMissed(enemy, player);
                                break;
                            }
                        }
                        else                            // IF (2) is false, then ... (3)
                        {
                            act_chance = 50;
                            dice = rnd.Next(1, 100);
                            if (dice <= act_chance)     // (3) Hunker Down
                            {
                                ui.hunkerDown(enemy);
                                break;
                            }
                            else                        // (3) Move Up
                            {
                                if (enemy.alreadyMoved == false)
                                {
                                    distance--;
                                    ui.moveUp(enemy, distance, half_cover, full_cover);
                                }
                                else
                                {
                                    distance--;
                                    ui.moveUp(enemy, distance, half_cover, full_cover);
                                    break;
                                }
                            }
                        }
                    }
                    else                                // If (1) is false, then Hunker down
                    {
                        ui.hunkerDown(enemy);
                        break;
                    }
                }   // End of Loop: Alien Activity
            }   // End of Loop: Turn
        }   // End of Method: Main
    }    // End of Class: Program
}    // End of Namespace  

