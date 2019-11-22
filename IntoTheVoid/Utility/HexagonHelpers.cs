using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntoTheVoid.Utility
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

        public static IEnumerable<Point> GetSurroundingCells(Point gridPoint)
        {
            var surrounding = new Point[6];
            for (var i=0; i<6; i++)
            {
                surrounding[i] = GetFromDirection(gridPoint, i);
                Console.WriteLine(surrounding[i]);
            }
            return surrounding;
        }

        public static Point GetFromDirection(Point gridPosition, int direction)
        {
            int eastDir;
            int westDir;
            if (gridPosition.Y % 2 == 1)
            {
                westDir = 0;
                eastDir = 1;
            }
            else
            {
                westDir = -1;
                eastDir = 0;
            }
            var offset = new Point(0, 0);
            switch (direction % 6)
            {
                case Direction.East:
                    offset = new Point(1, 0);
                    break;
                case Direction.West:
                    offset = new Point(-1, 0);
                    break;
                case Direction.SouthWest:
                    offset = new Point(westDir, 1);
                    break;
                case Direction.NorthWest:
                    offset = new Point(westDir, -1);
                    break;
                case Direction.SouthEast:
                    offset = new Point(eastDir, 1);
                    break;
                case Direction.NorthEast:
                    offset = new Point(eastDir, -1);
                    break;
            }
            return gridPosition + offset;
        }

        public static int? GetDirectionFromOffset(Point gridPosition, Point offset)
        {
            int eastDir;
            int westDir;
            if (gridPosition.Y % 2 == 1)
            {
                westDir = 0;
                eastDir = 1;
            }
            else
            {
                westDir = -1;
                eastDir = 0;
            }
            if (offset == new Point(1, 0)) 
            {
                return Direction.East;
            }
            if (offset == new Point(-1, 0))
            {
                return Direction.West;
            }
            if (offset == new Point(westDir, 1))
            {
                return Direction.SouthWest;
            }
            if (offset == new Point(westDir, -1))
            {
                return Direction.NorthWest;
            }
            if (offset == new Point(eastDir, 1))
            {
                return Direction.SouthEast;
            }
            if (offset == new Point(eastDir, -1))
            {
                return Direction.NorthEast;
            }
            return null;
        }

        internal static object GetDirectionString(int? bFromA)
        {
            switch (bFromA)
            {
                case Direction.East:
                    return nameof(Direction.East);
                case Direction.West:
                    return nameof(Direction.West);
                case Direction.SouthEast:
                    return nameof(Direction.SouthEast);
                case Direction.SouthWest:
                    return nameof(Direction.SouthWest);
                case Direction.NorthEast:
                    return nameof(Direction.NorthEast);
                case Direction.NorthWest:
                    return nameof(Direction.NorthWest);
            }
            return "Undefined";
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
