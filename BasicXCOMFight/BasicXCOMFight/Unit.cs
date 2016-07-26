﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicXCOMFight
{
    public class Unit
    {
        public string name;
        public int hp, maxHP, aim, crit, cover, def;    // stats
        public bool hunker = false;
        public bool alreadyMoved = false;
        public int hitChanceCheck = 20;
        public const int half_cover = 20;
        public const int full_cover = 35;
        public void getPlayer()
        {
            Console.Write("Please input character name: ");
            name = Console.ReadLine();
            hp = 6;
            maxHP = 6;
            aim = 65;
            crit = 0;
            cover = half_cover;
            def = 5;
            Console.Clear();
        }
        public void enemy_Sectoid()
        {
            name = "Sectoid";
            hp = 6;
            maxHP = 6;
            aim = 65;
            crit = 0;
            cover = half_cover;
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
            name = "Thin Man";
            hp = 7;
            maxHP = 6;
            aim = 75;
            crit = 10;
            cover = half_cover;
            def = 10;
            hitChanceCheck = 15; 
        }
    }
}
