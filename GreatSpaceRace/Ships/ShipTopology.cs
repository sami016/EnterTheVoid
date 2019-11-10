using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Ships
{
    public class ShipTopology
    {
        public Section[,] Sections { get; }

        public int GridWidth => Sections.GetLength(0);
        public int GridHeight => Sections.GetLength(1);

        public ShipTopology(int gridWidth, int gridHeight)
        {
            Sections = new Section[gridWidth, gridHeight];
        }

    }
}
