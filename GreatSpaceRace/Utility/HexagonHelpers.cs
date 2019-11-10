using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Utility
{
    /// <summary>
    /// Assumptions:
    /// The radius a grid hexagon fits in has radius 1.
    /// The edge length of each side is sqrt(3/4).
    /// 
    /// </summary>
    public static class HexagonHelpers
    {

        private static float root3over4 = (float)Math.Sqrt(3f / 4f);

        public static Vector3 GetGridWorldPosition(Point gridPoint, int verticalPosition = 0)
        {
            var xOffset = 0f;
            if (gridPoint.Y % 2 == 1)
            {
                xOffset += root3over4;
            }
            return new Vector3(gridPoint.X * 2f * root3over4 + xOffset, verticalPosition * 2f, gridPoint.Y * 1.5f);
        }


        // A region bounded by the lx and lz. Lx is a multiple of root3over4, whilst lz is a multiple of 1.5.
        private static Point GetUpperFromBoundedRegion(int lxg, int lzg)
        {
            if (lxg % 1 == 0)
            {
                return new Point(lxg, lzg);
            }
            return new Point(lxg + 1, lzg);
        }

        private static Point GetLowerFromBoundedRegion(int lxg, int lzg)
        {
            if (lxg % 1 == 0)
            {
                return new Point(lxg + 1, lzg + 1);
            }
            return new Point(lxg, lzg + 1);
        }

        private static Point ToFullGridPosition(Point gridPosPartial)
        {
            return new Point(gridPosPartial.X / 2, gridPosPartial.Y);
        }

        public static Point GetWorldGridPosition(Vector3 position)
        {
            var xOffset = 0f;
            var lzg = (int)Math.Round(position.Z / 1.5f);
            if (lzg % 2 == 1)
            {
                xOffset += root3over4;
            }

            var lxg = (int)Math.Round((position.X - xOffset) / (root3over4 * 2));
            return new Point(lxg, lzg);
        }
    }
}
