using System;
using System.Collections.Generic;
using System.Text;

namespace IntoTheVoid.Utility
{
    public static class RotationHelper
    {
        public static float GetAngle(float dx, float dy)
        {
            var angle = Math.Atan2(dy, dx);
            Console.WriteLine(angle);
            return (float)angle;
        }
    }
}
