using GreatSpaceRace.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Ships.Connections
{
    public static class ConnectionLayouts
    {
        public static ConnectionLayout FullyConnected = new ConnectionLayout(
            new List<Direction>() { Direction.East, Direction.SouthEast, Direction.SouthWest, Direction.West, Direction.NorthEast, Direction.NorthWest },
            new List<Direction>() { Direction.East, Direction.SouthEast, Direction.SouthWest, Direction.West, Direction.NorthEast, Direction.NorthWest }
        );

        public static ConnectionLayout FullyConnectedLarge = new ConnectionLayout(
            new List<Direction>() { Direction.East, Direction.SouthEast, Direction.SouthWest, Direction.West, Direction.NorthEast, Direction.NorthWest },
            new List<Direction>() { }
        );

        public static ConnectionLayout FullyConnectedSmall = new ConnectionLayout(
            new List<Direction>() { },
            new List<Direction>() { Direction.East, Direction.SouthEast, Direction.SouthWest, Direction.West, Direction.NorthEast, Direction.NorthWest }
        );

        public static ConnectionLayout HalfConnectedLarge = new ConnectionLayout(
            new List<Direction>() { Direction.East, Direction.NorthWest, Direction.SouthWest },
            new List<Direction>() { }
        );

        public static ConnectionLayout HalfConnectedSmall = new ConnectionLayout(
            new List<Direction>() { },
            new List<Direction>() { Direction.East, Direction.NorthWest, Direction.SouthWest }
        );

        public static ConnectionLayout PassThroughLarge = new ConnectionLayout(
            new List<Direction>() { Direction.East, Direction.West },
            new List<Direction>() { }
        );

        public static ConnectionLayout PassThroughSmall = new ConnectionLayout(
            new List<Direction>() { },
            new List<Direction>() { Direction.East, Direction.West }
        );

        public static ConnectionLayout PassThrough = new ConnectionLayout(
            new List<Direction>() { Direction.East, Direction.West },
            new List<Direction>() { Direction.East, Direction.West }
        );

        public static ConnectionLayout PassThroughA = new ConnectionLayout(
            new List<Direction>() { Direction.East, Direction.West },
            new List<Direction>() { Direction.East }
        );

        public static ConnectionLayout PassThroughB = new ConnectionLayout(
            new List<Direction>() { Direction.East },
            new List<Direction>() { Direction.East, Direction.West }
        );

        public static ConnectionLayout SingleLarge = new ConnectionLayout(
            new List<Direction>() { Direction.East },
            new List<Direction>() { }
        );

        public static ConnectionLayout SingleSmall = new ConnectionLayout(
            new List<Direction>() { },
            new List<Direction>() { Direction.East }
        );

        public static ConnectionLayout SingleBoth = new ConnectionLayout(
            new List<Direction>() { },
            new List<Direction>() { Direction.East }
        );

        public static ConnectionLayout AccuteVSmallA = new ConnectionLayout(
            new List<Direction>() { },
            new List<Direction>() { Direction.East, Direction.SouthEast }
        );

        public static ConnectionLayout AccuteVSmallB = new ConnectionLayout(
            new List<Direction>() { },
            new List<Direction>() { Direction.East, Direction.NorthWest }
        );

        public static ConnectionLayout AccuteVA = new ConnectionLayout(
            new List<Direction>() { Direction.East },
            new List<Direction>() { Direction.East, Direction.SouthEast }
        );

        public static ConnectionLayout AccuteVB = new ConnectionLayout(
            new List<Direction>() { Direction.East },
            new List<Direction>() { Direction.East, Direction.NorthWest }
        );

        public static ConnectionLayout WideVSmallA = new ConnectionLayout(
            new List<Direction>() { Direction.East, Direction.SouthWest },
            new List<Direction>() { }
        );

        public static ConnectionLayout WideVSmallB = new ConnectionLayout(
            new List<Direction>() { },
            new List<Direction>() { Direction.East, Direction.SouthWest }
        );

        public static ConnectionLayout WideVA = new ConnectionLayout(
            new List<Direction>() { Direction.East },
            new List<Direction>() { Direction.East, Direction.SouthWest }
        );

        public static ConnectionLayout WideVB = new ConnectionLayout(
            new List<Direction>() { Direction.SouthWest },
            new List<Direction>() { Direction.East, Direction.SouthWest }
        );




    }
}
