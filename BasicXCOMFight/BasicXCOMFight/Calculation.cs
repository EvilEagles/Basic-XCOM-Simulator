using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicXCOMFight
{
    class Calculation
    {
        public int hitChance;
        public double takeShot_influence = 0;
        public int takeShotPercent = 0;

        public double hunker_influence = 0;
        public int hunkerPercent = 0;

        public double move_influence = 0;
        public int movePercent = 0;

        public int minPercent = 0;
        public int maxPercent = 0;

        Random rnd = new Random();
        public int actionTaken;        

        // CALCULATION: DEFAULT DISTANCE
        public int defaultDistance(int min, int max)
        {
            int distance = rnd.Next(min, max);
            return distance;
        }
        // CALCULATION: ROLLING A DICE
        public int diceroll(int min, int max)
        {            
            int dice = rnd.Next(min, max);
            return dice;
        }
        // CALCULATION: CALCULATING HIT CHANCE
        public int calculateHitChance(int distance, int close_range, Unit user, Unit target)
        {
            int hit_chance;
            if (distance >= close_range) hit_chance = user.aim - target.def - target.cover + ((18 - distance) * 2);
            else hit_chance = user.aim - target.def - target.cover + 22 + ((7 - distance) * 4);
            return hit_chance;
        }
        // CALCULATION: CALCULATING HIT CHANCE INFLUENCE
        public double calculateTakeShot_influence(int hitChance, Unit user, Unit target)
        {
            double influence = 0;
            if (hitChance <= user.hitChanceCheck)
                influence = 0.5;
            else if (hitChance > user.hitChanceCheck && hitChance <= user.hitChanceCheck + 20)
                influence = 0.6;
            else if (hitChance > user.hitChanceCheck + 20 && hitChance <= user.hitChanceCheck + 40)
                influence = 0.7;
            else if (hitChance > user.hitChanceCheck + 40 && hitChance <= user.hitChanceCheck + 60)
                influence = 0.8;
            else
                influence = 0.9;
            if (user.alreadyMoved == true) influence += 0.05;
            if (user.hp <= user.maxHP / 3) influence -= 0.3;
            if (target.hp <= target.maxHP / 3) influence += 0.2;
            return influence;
        }
        // CALCULATION: CALCULATING HUNKERING INFLUENCE
        public double calculateHunkering_influence(int takeShotPercent, Unit user, int half_cover)
        {
            double influence = 0;
            if (takeShotPercent <= 50) influence = 0.5;
            if (user.hp <= user.maxHP / 3) influence += 0.4;
            if (user.cover == half_cover) influence -= 0.2;
            return influence;
        }
        // CALCULATION: CALCULATING MOVING INFLUENCE
        public double calculateMoving_influence(int takeShotPercent, Unit user, int full_cover)
        {
            double influence = 1;
            if (user.cover == full_cover) influence -= 0.8;
            if (takeShotPercent >= 50) influence -= 0.5;
            else influence += 0.3;
            if (user.alreadyMoved == true) influence -= 0.2;
            return influence;
        }
        // CALCULATION: CHECK IF UNIT IS HUNKERED, IF YES THEN UNHUNKER
        public void unhunker(Unit user)
        {
            if (user.hunker == true)
            {
                user.cover /= 2;
                user.hunker = false;
            }
        }
    }
}
