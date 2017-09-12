using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShootingGame
{
    /// <summary>
    /// Class containing additional methods used in the programm
    /// </summary>
    public static class Methods
    {
        /// <summary>
        /// Calculates the distance of 2 points.
        /// </summary>
        /// <param name="x1">X coordinate of the 1. point</param>
        /// <param name="y1">Y coordinate of the 1. point</param>
        /// <param name="x2">X coordinate of the 2. point</param>
        /// <param name="y2">Y coordinate of the 2. point</param>
        /// <returns>Absolute distance of the given points</returns>
        public static float Distance(float x1, float y1, float x2, float y2)
        {
            float d = (float)Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            return d;
        }
        /// <summary>
        /// Generates new random XY coordinates at the border of the form.
        /// </summary>
        /// <param name="width">Width of the form</param>
        /// <param name="height">Height of the form</param>
        /// <param name="random"></param>
        /// <returns>Field containing X coordinate and Y coordinate</returns>
        public static int[] GetBorderPosition(int width, int height, Random random)
        {
            int side = random.Next(4);
            int x = random.Next(width);
            int y = random.Next(height);

            switch (side)
            {
                case 0:
                    y = -30;
                    break;
                case 1:
                    x = width + 30;
                    break;
                case 2:
                    y = height + 30;
                    break;
                case 3:
                    x = -30;
                    break;
            }

            return new int[] { x, y };
        }

        public static List<string[]> SortTwo_dimentionalArray(List<string[]> array)
        {
            int mistakes = 0;
            do
            {
                mistakes = 0;
                for (int i = 0; i < array.Count - 1; i++)
                    if (int.Parse(array[i][0]) < int.Parse(array[i + 1][0]))
                    {
                        array.Reverse(i, 2);
                        mistakes++;
                    }
            }
            while (mistakes > 0);
            return array;
        }
    }
}
