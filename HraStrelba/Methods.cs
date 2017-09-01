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
    }
}
