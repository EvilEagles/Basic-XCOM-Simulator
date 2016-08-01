using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicXCOMFight
{
    class Unit : Program
    {
        // BASIC PROPERTIES & CONDITIONS
        public string name;
        public int hp, maxHP, aim, crit, cover, def;    // stats        
        public bool alreadyMoved;
        
        // AI ONLY        
        public int hitChanceCheck = 20;

        /* SKILLS & PERKS
        perks[0]: Hunker Down   
        perks[1]: Overwatch     
        perks[2]: Opportunist
        */
        public bool[] perks = { false, false, false };

        UI ui = new UI();
        public Unit()
        {
            Console.WriteLine("Welcome to Basic XCOM Simulator v0.5!");
            xcom();
        }
        public void getPlayer()
        {
            // STATS
            Console.Write("Please input character name: ");
            name = Console.ReadLine();
            hp = 6;
            maxHP = 6;
            aim = 65;
            crit = 0;            
            cover = ui.half_cover;
            def = 5;

            // SKILLS & PERKS
            perks[2] = true;

            Console.Clear();
        }
        public void enemy_Sectoid()
        {
            name = "Sectoid";
            hp = 6;
            maxHP = 6;
            aim = 65;
            crit = 0;
            cover = ui.half_cover;
            def = 5;
        }
        public void enemy_Floater()
        {
            name = "Floater";
            hp = 6;
            maxHP = 6;
            aim = 70;
            crit = 0;
            cover = 0;
            def = 30;
        }
        public void enemy_ThinMan()
        {
            // STATS
            name = "Thin Man";
            hp = 7;
            maxHP = 7;
            aim = 75;
            crit = 10;
            cover = ui.half_cover;
            def = 10;
            hitChanceCheck = 15;

            // SKILLS & PERKS
            perks[2] = true;
        }
    }
}
