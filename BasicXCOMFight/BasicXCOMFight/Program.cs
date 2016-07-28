using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicXCOMFight
{
    class Program
    {
        public void xcom()
        {
            // CREATE CHARACTER
            Unit player = new Unit();         
            player.getPlayer();
            // SELECT ENEMY
            Unit enemy = new Unit();
            enemy.enemy_Sectoid();
            // UI INSTANCE
            UI ui = new UI();
            // ACTION INSTANCE
            Action action = new Action();
            // CALCULATION INSTANCE
            Calculation calc = new Calculation();

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
                calc.unhunker(player);   // Check if unit hunkered last turn, if yes then un-hunker

                // PRINTING USER INTERFACE
                ui.distance = calc.diceroll(10, 18);
                ui.showUI(turn, ui.distance, ui.half_cover, ui.full_cover, player, enemy);

                // XCOM TURN: ACTION BEGINS
                ui.loop = true;
                while (ui.loop == true)
                {
                    // PRE-CONDITIONS
                    calc.hitChance =calc.calculateHitChance(ui.distance, ui.close_range, player, enemy);
                    // SHOW COMMANDS                    
                    ui.showCommand(calc.hitChance, player.crit);
                    // INPUT COMMANDS
                    ui.input = ui.inputCommand();

                    // XCOM TURN: ACTIONS
                    Console.Clear();
                    switch (ui.input)
                    {
                        case 1:
                            action.takeShot(player, enemy, calc.hitChance);
                            ui.loop = false;
                            break;
                        case 2:
                            action.hunkerDown(player);
                            ui.loop = false;
                            break;
                        case 3:
                            ui.distance--;
                            ui.loop = action.moveUp(player, ui.distance, ui.half_cover, ui.full_cover);
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
                calc.unhunker(enemy);   // Check if unit hunkered last turn. If yes then un-hunker

                // XCOM TURN: ACTION BEGINS
                ui.loop = true;
                while (ui.loop == true)
                {
                    // CALCULATING TAKE SHOT PERCENT
                    calc.hitChance = calc.calculateHitChance(ui.distance, ui.close_range, enemy, player);
                    calc.takeShot_influence = calc.calculateTakeShot_influence(calc.hitChance, enemy, player);
                    calc.takeShotPercent = Convert.ToInt32(100 * calc.takeShot_influence);

                    // CALCULATING HUNKER DOWN PERCENT
                    calc.hunker_influence = calc.calculateHunkering_influence(calc.takeShotPercent, enemy, ui.half_cover);
                    calc.hunkerPercent = Convert.ToInt32((100 - calc.takeShotPercent) * calc.hunker_influence);

                    // CALCULATING MOVE PERCENT
                    calc.move_influence = calc.calculateMoving_influence(calc.takeShotPercent, enemy, ui.full_cover);
                    calc.movePercent = Convert.ToInt32((100 - calc.takeShotPercent - calc.hunkerPercent) * calc.move_influence);

                    // ROLLING THE ACTION CHANCE DICE
                    calc.actionTaken = calc.diceroll(1, 100);

                    // TAKE A SHOT
                    calc.minPercent = calc.maxPercent;
                    calc.maxPercent += calc.takeShotPercent;
                    if (calc.actionTaken > calc.minPercent && calc.actionTaken <= calc.maxPercent)            
                    {
                        action.takeShot(enemy, player, calc.hitChance);
                        break;
                    }

                    // MOVE UP
                    calc.minPercent = calc.maxPercent;
                    calc.maxPercent += calc.movePercent;
                    if (calc.actionTaken > calc.minPercent && calc.actionTaken <= calc.maxPercent)
                    {
                        ui.distance--;
                        ui.loop = action.moveUp(enemy, ui.distance, ui.half_cover, ui.full_cover);
                    }

                    // HUNKER DOWN
                    else
                    {
                        action.hunkerDown(enemy);
                        break;
                    }
                }   // End of Loop: Alien Activity
            }   // End of Loop: Turn
        }   // End of Method: Main
    }    // End of Class: Program
}    // End of Namespace  

