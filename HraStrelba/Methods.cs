using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HraStrelba
{
    public static class Methods
    {
        public static float Distance(float x1, float y1, float x2, float y2)
        {
            float d = (float)Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            return d;
        }
    }
}
