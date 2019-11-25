using EnterTheVoid.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnterTheVoid.Ships.Connections
{
    public class ConnectionLayout
    {
        public IList<int> LargeConnectors { get; }
        public IList<int> SmallConectors { get; }

        public ConnectionLayout(IList<int> largeConnectors, IList<int> smallConnectors)
        {
            LargeConnectors = largeConnectors;
            SmallConectors = smallConnectors;
        }

        public IEnumerable<int> GetSmallConnectorDirections(int rotation)
        {
            return SmallConectors
                .Select(x => (x + rotation) % 6)
                .ToArray();
        }

        public IEnumerable<int> GetLargeConnectorDirections(int rotation)
        {
            return LargeConnectors
                .Select(x => (x + rotation) % 6)
                .ToArray();
        }
    }
}
