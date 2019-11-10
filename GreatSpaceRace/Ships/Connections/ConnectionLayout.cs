using GreatSpaceRace.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Ships.Connections
{
    public class ConnectionLayout
    {
        public IList<Direction> LargeConnectors { get; }
        public IList<Direction> SmallConectors { get; }

        public ConnectionLayout(IList<Direction> largeConnectors, IList<Direction> smallConnectors)
        {
            LargeConnectors = largeConnectors;
            SmallConectors = smallConnectors;
        }
    }
}
