using EnterTheVoid.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.Ships.Connections
{
    public static class ConnectionLayouts
    {
        public static ConnectionLayout OnlyEast = new ConnectionLayout(
            new List<int>() { Direction.East },
            new List<int>() { Direction.East }
        );

        public static ConnectionLayout OnlyWest = new ConnectionLayout(
            new List<int>() { Direction.West },
            new List<int>() { Direction.West }
        );

        public static ConnectionLayout OnlySouthEast = new ConnectionLayout(
            new List<int>() { Direction.SouthEast },
            new List<int>() { Direction.SouthEast }
        );

        public static ConnectionLayout OnlySouthWest = new ConnectionLayout(
            new List<int>() { Direction.SouthWest },
            new List<int>() { Direction.SouthWest }
        );

        public static ConnectionLayout OnlyNorthEast = new ConnectionLayout(
            new List<int>() { Direction.NorthEast },
            new List<int>() { Direction.NorthEast }
        );

        public static ConnectionLayout OnlyNorthWest = new ConnectionLayout(
            new List<int>() { Direction.NorthWest },
            new List<int>() { Direction.NorthWest }
        );

        public static ConnectionLayout FullyConnected = new ConnectionLayout(
            new List<int>() { Direction.East, Direction.SouthEast, Direction.SouthWest, Direction.West, Direction.NorthEast, Direction.NorthWest },
            new List<int>() { Direction.East, Direction.SouthEast, Direction.SouthWest, Direction.West, Direction.NorthEast, Direction.NorthWest }
        );

        public static ConnectionLayout FullyConnectedLarge = new ConnectionLayout(
            new List<int>() { Direction.East, Direction.SouthEast, Direction.SouthWest, Direction.West, Direction.NorthEast, Direction.NorthWest },
            new List<int>() { }
        );

        public static ConnectionLayout FullyConnectedSmall = new ConnectionLayout(
            new List<int>() { },
            new List<int>() { Direction.East, Direction.SouthEast, Direction.SouthWest, Direction.West, Direction.NorthEast, Direction.NorthWest }
        );

        public static ConnectionLayout HalfConnectedLarge = new ConnectionLayout(
            new List<int>() { Direction.East, Direction.NorthWest, Direction.SouthWest },
            new List<int>() { }
        );

        public static ConnectionLayout HalfConnectedSmall = new ConnectionLayout(
            new List<int>() { },
            new List<int>() { Direction.East, Direction.NorthWest, Direction.SouthWest }
        );

        public static ConnectionLayout HalfConnected = new ConnectionLayout(
            new List<int>() { Direction.East, Direction.NorthWest, Direction.SouthWest },
            new List<int>() { Direction.East, Direction.NorthWest, Direction.SouthWest }
        );

        public static ConnectionLayout HalfConnectedAlternatingA = new ConnectionLayout(
            new List<int>() { Direction.West, Direction.SouthEast, Direction.NorthEast },
            new List<int>() { Direction.East, Direction.NorthWest, Direction.SouthWest }
        );

        public static ConnectionLayout HalfConnectedAlternatingB = new ConnectionLayout(
            new List<int>() { Direction.East, Direction.NorthWest, Direction.SouthWest },
            new List<int>() { Direction.West, Direction.SouthEast, Direction.NorthEast }
        );

        public static ConnectionLayout PassThroughLarge = new ConnectionLayout(
            new List<int>() { Direction.East, Direction.West },
            new List<int>() { }
        );

        public static ConnectionLayout PassThroughSmall = new ConnectionLayout(
            new List<int>() { },
            new List<int>() { Direction.East, Direction.West }
        );

        public static ConnectionLayout PassThrough = new ConnectionLayout(
            new List<int>() { Direction.East, Direction.West },
            new List<int>() { Direction.East, Direction.West }
        );

        public static ConnectionLayout PassThroughA = new ConnectionLayout(
            new List<int>() { Direction.East, Direction.West },
            new List<int>() { Direction.East }
        );

        public static ConnectionLayout PassThroughB = new ConnectionLayout(
            new List<int>() { Direction.East },
            new List<int>() { Direction.East, Direction.West }
        );

        public static ConnectionLayout PassThroughC1 = new ConnectionLayout(
            new List<int>() { Direction.SouthWest, Direction.NorthEast },
            new List<int>() { Direction.SouthWest, Direction.NorthEast }
        );

        public static ConnectionLayout PassThroughC2 = new ConnectionLayout(
            new List<int>() { Direction.SouthWest, Direction.NorthEast },
            new List<int>() { }
        );

        public static ConnectionLayout PassThroughC3 = new ConnectionLayout(
            new List<int>() { },
            new List<int>() { Direction.SouthWest, Direction.NorthEast }
        );

        public static ConnectionLayout PassThroughD1 = new ConnectionLayout(
            new List<int>() { Direction.SouthEast, Direction.NorthWest},
            new List<int>() { Direction.SouthEast, Direction.NorthWest }
        );

        public static ConnectionLayout PassThroughD2 = new ConnectionLayout(
            new List<int>() { Direction.SouthEast, Direction.NorthWest },
            new List<int>() { }
        );

        public static ConnectionLayout PassThroughD3 = new ConnectionLayout(
            new List<int>() { },
            new List<int>() { Direction.SouthEast, Direction.NorthWest }
        );

        public static ConnectionLayout SingleLarge = new ConnectionLayout(
            new List<int>() { Direction.East },
            new List<int>() { }
        );

        public static ConnectionLayout SingleSmall = new ConnectionLayout(
            new List<int>() { },
            new List<int>() { Direction.East }
        );

        public static ConnectionLayout SingleBoth = new ConnectionLayout(
            new List<int>() { },
            new List<int>() { Direction.East }
        );

        public static ConnectionLayout AccuteVSmallA = new ConnectionLayout(
            new List<int>() { },
            new List<int>() { Direction.East, Direction.SouthEast }
        );

        public static ConnectionLayout AccuteVSmallB = new ConnectionLayout(
            new List<int>() { },
            new List<int>() { Direction.East, Direction.NorthWest }
        );

        public static ConnectionLayout AccuteVA = new ConnectionLayout(
            new List<int>() { Direction.East },
            new List<int>() { Direction.East, Direction.SouthEast }
        );

        public static ConnectionLayout AccuteVB = new ConnectionLayout(
            new List<int>() { Direction.East },
            new List<int>() { Direction.East, Direction.NorthWest }
        );

        public static ConnectionLayout WideVSmallA = new ConnectionLayout(
            new List<int>() { Direction.East, Direction.SouthWest },
            new List<int>() { }
        );

        public static ConnectionLayout WideVSmallB = new ConnectionLayout(
            new List<int>() { },
            new List<int>() { Direction.East, Direction.SouthWest }
        );

        public static ConnectionLayout WideVA = new ConnectionLayout(
            new List<int>() { Direction.East },
            new List<int>() { Direction.East, Direction.SouthWest }
        );

        public static ConnectionLayout WideVB = new ConnectionLayout(
            new List<int>() { Direction.SouthWest },
            new List<int>() { Direction.East, Direction.SouthWest }
        );




    }
}
