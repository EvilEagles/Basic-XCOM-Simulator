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

        // ACTION: TAKING SHOT
        public void takeShot(Unit user, Unit target, int hitChance)
        {
            int dice = rnd.Next(1, 100);
            if (dice <= hitChance)     // IF: Shot hits
            {
                int damage = rnd.Next(1, 3);
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine();
                Console.WriteLine("{0} took an accurate shot and {1} took {2} damage.", user.name, target.name, damage);
                target.hp -= damage;
            }
            else                        // IF: Shot misses
            {
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine();
                Console.WriteLine("{0} took a shot at {1}. It is a miss.", user.name, target.name);
            }
        }
        // ACTION: GO INTO OVERWATCH
        public void overwatch(Unit user)
        {
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine();
            Console.WriteLine("{0} went into Overwatch.", user.name);
            user.overwatch = true;
        }
        // ACTION: HUNKER DOWN
        public int hunkerDown(Unit user)
        {
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine();
            Console.WriteLine("{0} hunkered down, doubling cover bonus. (+{1} Defense)", user.name, user.cover);
            int hunker = user.cover * 2;
            return hunker;
        }
        // ACTION: MOVING UP
        public bool moveUp(Unit user, int distance, int half_cover, int full_cover)
        {
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine();
            Random rnd = new Random();
            if (user.alreadyMoved == false)
            {
                int dice = rnd.Next(1, 3);
                if (dice == 1)
                {
                    Console.WriteLine("{0} moves forward to Full Cover.", user.name);
                    user.cover = full_cover;
                }
                else
                {
                    Console.WriteLine("{0} moves forward to Half Cover.", user.name);
                    user.cover = half_cover;
                }
                Console.WriteLine("Distance decreased by 1. Current distance: {0}", distance);
                user.alreadyMoved = true;
                return true;
            }
            else
            {
                int dice = rnd.Next(1, 3);
                if (dice == 1)
                {
                    Console.WriteLine("{0} moves forward to Full Cover.", user.name);
                    user.cover = full_cover;
                }
                else
                {
                    Console.WriteLine("{0} moves forward to Half Cover.", user.name);
                    user.cover = half_cover;
                }
                Console.WriteLine("Distance decreased by 1. Current distance: {0}", distance);
                user.alreadyMoved = false;
                return false;
            }
        }

    }
}
