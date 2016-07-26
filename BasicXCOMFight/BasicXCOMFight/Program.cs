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
            bool loop = true;

            // RNG SYSTEM
            Random rnd = new Random();
            int dice;

            int distance = rnd.Next(10, 18);
            int close_range = 7;

            // AI SYSTEM
            int hitChance;
            double takeShot_influence = 0;
            int takeShotPercent = 0;

            double hunker_influence = 0;
            int hunkerPercent = 0;

            double move_influence = 0;
            int movePercent = 0;

            int minPercent = 0;
            int maxPercent = 0;
            // BATTLE SCENE
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
                    hitChance = ui.calculateHitChance(distance, close_range, player, enemy);
                    // SHOW COMMANDS                    
                    ui.showCommand(hitChance, player.crit);
                    // INPUT COMMANDS
                    input = ui.inputCommand();

                    // XCOM TURN
                    Console.Clear();
                    if (input == 1)         // IF: Take a Shot
                    {
                        dice = rnd.Next(1, 100);
                        if (dice <= hitChance)     // IF: Shot hits
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
                            player.alreadyMoved = true;
                        }
                        else
                        {
                            distance--;
                            ui.moveUp(player, distance, half_cover, full_cover);
                            player.alreadyMoved = false;
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

                while (loop == true)
                {
                    // CALCULATING TAKE SHOT PERCENT
                    hitChance = ui.calculateHitChance(distance, close_range, enemy, player);
                    takeShot_influence = ui.calculateTakeShot_influence(hitChance, enemy, player);
                    takeShotPercent = Convert.ToInt32(100 * takeShot_influence);

                    // CALCULATING HUNKER DOWN PERCENT
                    hunker_influence = ui.calculateHunkering_influence(takeShotPercent, enemy, half_cover);
                    hunkerPercent = Convert.ToInt32((100 - takeShotPercent) * hunker_influence);

                    // CALCULATING MOVE PERCENT
                    move_influence = ui.calculateMoving_influence(takeShotPercent, enemy, full_cover);
                    movePercent = Convert.ToInt32((100 - takeShotPercent - hunkerPercent) * move_influence);

                    // ROLLING THE DICE
                    dice = rnd.Next(1, 100);

                    // TAKE A SHOT
                    minPercent = maxPercent;
                    maxPercent += takeShotPercent;
                    if (dice > minPercent && dice <= maxPercent)            
                    {
                        dice = rnd.Next(1, 100);
                        if (dice <= hitChance)     //IF: Shot hits
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

                    // MOVE UP
                    minPercent = maxPercent;
                    maxPercent += movePercent;
                    if (dice > minPercent && dice <= maxPercent)
                    {
                        if (enemy.alreadyMoved == false)
                        {
                            distance--;
                            ui.moveUp(enemy, distance, half_cover, full_cover);
                            enemy.alreadyMoved = true;
                        }
                        else
                        {
                            distance--;
                            ui.moveUp(enemy, distance, half_cover, full_cover);
                            player.alreadyMoved = false;
                            break;
                        }
                    }
                    else                              
                    {
                        ui.hunkerDown(enemy);
                        break;
                    }
                }   // End of Loop: Alien Activity
            }   // End of Loop: Turn
        }   // End of Method: Main
    }    // End of Class: Program
}    // End of Namespace  

