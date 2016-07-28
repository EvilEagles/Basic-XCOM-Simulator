using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicXCOMFight
{
    class Action
    {
        // INSTANCING RANDOM CLASS
        Random rnd = new Random();

        // INSTANCING UI CLASS
        UI ui = new UI();

        // INSTANCING CALCULATION CLASS
        Calculation calc = new Calculation();

        // TEXT TO BE SLOW-PRINTED
        string text;

        // SLOWPRINT SPEED
        int slowprint_spd = 20;

        // ACTION: TAKING SHOT
        public void takeShot(Unit user, Unit target, int hitChance)
        {
            Console.WriteLine();
            text = user.name + " took a shot at " + target.name + ". ";
            ui.slowprint(text, slowprint_spd);
            System.Threading.Thread.Sleep(500);
            int dice = rnd.Next(1, 100);
            if (dice <= hitChance)     // IF: Shot hits
            {
                int damage = rnd.Next(1, 3);
                text = target.name + " took " + Convert.ToString(damage) + " damage.\n";
                ui.slowprint(text, slowprint_spd);
                target.hp -= damage;
            }
            else                        // IF: Shot misses
            {
                text = "It is a miss.\n";
                ui.slowprint(text, slowprint_spd);
            }
            user.overwatch = false;
        }
        // ACTION: GO INTO OVERWATCH
        public void overwatch(Unit user)
        {
            Console.WriteLine();
            text = user.name + " went into Overwatch.\n";
            ui.slowprint(text, slowprint_spd);
            user.overwatch = true;
        }
        // ACTION: HUNKER DOWN
        public int hunkerDown(Unit user)
        {
            Console.WriteLine();
            text = user.name + " hunkered down, doubling cover bonus. (+" + Convert.ToString(user.cover) + " Defense)\n";
            ui.slowprint(text, slowprint_spd);
            int hunker = user.cover * 2;
            return hunker;
        }
        // ACTION: MOVING UP
        public bool moveUp(Unit user, Unit target, int distance, int half_cover, int full_cover)
        {
            Console.WriteLine();
            Random rnd = new Random();
            if (user.alreadyMoved == false)
            {
                int dice = rnd.Next(1, 3);
                if (dice == 1)
                {
                    text = user.name + " moves forward towards Full Cover.\n";
                    ui.slowprint(text, slowprint_spd);
                    if (target.overwatch == true) takeShot(target, user, calc.hitChance);
                    user.cover = full_cover;
                }
                else
                {
                    text = user.name + " moves forward towards Half Cover.\n";
                    ui.slowprint(text, slowprint_spd);
                    if (target.overwatch == true) takeShot(target, user, calc.hitChance);
                    user.cover = half_cover;
                }
                text = "Distance decreased by 1. Current distance: " + Convert.ToString(distance);
                ui.slowprint(text, slowprint_spd);
                user.alreadyMoved = true;
                return true;
            }
            else
            {
                int dice = rnd.Next(1, 3);
                if (dice == 1)
                {
                    text = user.name + " moves forward towards Full Cover.\n";
                    ui.slowprint(text, slowprint_spd);
                    if (target.overwatch == true) takeShot(target, user, calc.hitChance);
                    user.cover = full_cover;
                }
                else
                {
                    text = user.name + " moves forward towards Full Cover.\n";
                    ui.slowprint(text, slowprint_spd);
                    if (target.overwatch == true) takeShot(target, user, calc.hitChance);
                    user.cover = half_cover;
                }
                text = "Distance decreased by 1. Current distance: " + Convert.ToString(distance);
                ui.slowprint(text, slowprint_spd);
                user.alreadyMoved = false;
                return false;
            }
        }

    }
}
