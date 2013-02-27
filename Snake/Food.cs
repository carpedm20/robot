﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace robot.Snake
{
    public class Food
    {
        static public Point foodPos;
        private int x, y, width, height;
        private SolidBrush brush;
        public Rectangle foodRec;
        bool isEnemy;

        public Food(Random randFood, bool isEnemy)
        {
            this.isEnemy = isEnemy;

            x = randFood.Next(1, 28) * 15;
            y = randFood.Next(1, 28) * 15;

            brush = new SolidBrush(Color.Black);

            width = 15;
            height = 15;

            if (isEnemy == false)
            {
                foodPos = new Point(x, y);
            }
            else
            {
                Point p = new Point(x, y);

                while (p == foodPos)
                {
                    x = randFood.Next(1, 28) * 15;
                    y = randFood.Next(1, 28) * 15;

                    p = new Point(x, y);
                }
            }

            foodRec = new Rectangle(x, y, width, height);
        }

        public void foodLocation(Random randFood, List<Food> unFood)
        {
            x = randFood.Next(1, 28) * 15;
            y = randFood.Next(1, 28) * 15;

            List<Point> unFoodRec = new List<Point>();

            foreach (Food f in unFood)
            {
                unFoodRec.Add(f.foodRec.Location);
            }

            for (int i = -2; i < 3; i++)
            {
                for (int j = -2; j < 3; j++)
                {
                    unFoodRec.Add(new Point (SnakeForm.snakeHeadPos.X + i, SnakeForm.snakeHeadPos.Y + j));
                }
            }

            Point p = new Point(x, y);
            bool isOverlaped=false;

            while (true)
            {
                foreach (Point uFRP in unFoodRec)
                {
                    if (uFRP == p)
                    {
                        isOverlaped = true;
                    }
                }

                if (isOverlaped == true)
                {
                    x = randFood.Next(1, 28) * 15;
                    y = randFood.Next(1, 28) * 15;
                    p = new Point(x, y);
                }
                else
                {
                    break;
                }
            }

            foodRec.X = x;
            foodRec.Y = y;
        }

        /*
        public void foodMove(Graphics paper)
        {
            Random rand = new Random((int)(DateTime.Now.Ticks));

            int xRand = rand.Next(0, 3);
            int yRand = rand.Next(0, 3);

            if (x + (xRand - 1) * 15 > 0 && x + (xRand - 1) * 15 < 420)
            {
                x = x + (xRand - 1) * 15;
            }

            if (y + (yRand - 1) * 15 > 0 && y + (yRand - 1) * 15 < 420)
            {
                y = y + (yRand - 1) * 15;
            }

            foodRec.X = x;
            foodRec.Y = y;

            paper.DrawIcon(new Icon(GetType(), "favicon.ico"), foodRec);
        }
        */

        public void drawFood(Graphics paper)
        {
            foodRec.X = x;
            foodRec.Y = y;

            if (isEnemy == false)
            {
                paper.DrawIcon(new Icon(GetType(), "favicon.ico"), foodRec);
            }
            else
            {
                paper.DrawIcon(new Icon(GetType(), "enemycon.ico"), foodRec);
            }
        }
    }
}
