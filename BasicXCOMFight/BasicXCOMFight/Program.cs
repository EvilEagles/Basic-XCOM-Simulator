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
            int actionTaken;

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
            // XCOM TURN
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

                // XCOM TURN: ACTION BEGINS
                loop = true;
                while (loop == true)
                {
                    // PRE-CONDITIONS
                    hitChance = ui.calculateHitChance(distance, close_range, player, enemy);
                    // SHOW COMMANDS                    
                    ui.showCommand(hitChance, player.crit);
                    // INPUT COMMANDS
                    input = ui.inputCommand();

                    // XCOM TURN: ACTIONS
                    Console.Clear();
                    switch (input)
                    {
                        case 1:
                            ui.takeShot(player, enemy, hitChance);
                            loop = false;
                            break;
                        case 2:
                            ui.hunkerDown(player);
                            loop = false;
                            break;
                        case 3:
                            distance--;
                            loop = ui.moveUp(player, distance, half_cover, full_cover);
                            break;
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

                // XCOM TURN: ACTION BEGINS
                loop = true;
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

                    // ROLLING THE ACTION CHANCE DICE
                    actionTaken = rnd.Next(1, 100);

                    // TAKE A SHOT
                    minPercent = maxPercent;
                    maxPercent += takeShotPercent;
                    if (actionTaken > minPercent && actionTaken <= maxPercent)            
                    {
                        ui.takeShot(enemy, player, hitChance);
                        break;
                    }

                    // MOVE UP
                    minPercent = maxPercent;
                    maxPercent += movePercent;
                    if (actionTaken > minPercent && actionTaken <= maxPercent)
                    {
                        distance--;
                        loop = ui.moveUp(enemy, distance, half_cover, full_cover);
                    }

                    // HUNKER DOWN
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

