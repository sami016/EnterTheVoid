using GreatSpaceRace.Ships.Modules;
using GreatSpaceRace.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreatSpaceRace.Ships
{
    public class ShipTopology
    {
        private Section[,] Sections { get; }

        public int GridWidth => Sections.GetLength(0);
        public int GridHeight => Sections.GetLength(1);

        public IEnumerable<Section> AllSections
        {
            get
            {
                foreach (var section in Sections)
                {
                    if (section != null)
                    {
                        yield return section;
                    }
                }
            }
        }

        public ShipTopology(int gridWidth, int gridHeight)
        {
            Sections = new Section[gridWidth, gridHeight];
        }

        public int Health
        {
            get
            {
                var health = 0;
                foreach (var section in Sections)
                {
                    if (section == null)
                    {
                        continue;
                    }
                    health += section.Health;
                }
                return health;
            }
        }

        public int MaxHealth
        {
            get
            {
                var maxHealth = 0;
                foreach (var section in Sections)
                {
                    if (section == null)
                    {
                        continue;
                    }
                    maxHealth += section.Module.MaxHealth;
                }
                return maxHealth;
            }
        }

        public int MaxEnergy
        {
            get
            {
                var maxEnergy = 20;
                foreach (var section in Sections)
                {
                    if (section.Module is EnergyModule energyModule)
                    {
                        maxEnergy += energyModule.PassiveCapacity;
                    }
                }
                return maxEnergy;
            }
        }
        public int MaxFuel
        {
            get
            {
                var maxEnergy = 20;
                foreach (var section in Sections)
                {
                    if (section.Module is FuelModule fuelModule)
                    {
                        maxEnergy += fuelModule.PassiveCapacity;
                    }
                }
                return maxEnergy;
            }
        }

        public Section SectionAt(Point location)
        {
            if (location.X < 0 
                || location.X >= GridWidth
                || location.Y < 0
                || location.Y >= GridHeight)
            {
                return null;
            }
            return Sections[location.X, location.Y];
        }

        public bool LegalSectionCheck(Section section, Point gridLocation)
        {
            var connected = false;
            foreach (var surroundingLocation in HexagonHelpers.GetSurroundingCells(gridLocation))
            {
                var neighbour = SectionAt(surroundingLocation);
                if (neighbour != null)
                {
                    var res = LegalSectionCheck(section, gridLocation, neighbour, surroundingLocation);
                    if (!res.legal)
                    {
                        return false;
                    }
                    connected = connected || res.connected;
                }
            }
            return connected;
        }

        public (bool legal, bool connected) LegalSectionCheck(Section a, Point aLocation, Section b, Point bLocation)
        {
            var bFromA = HexagonHelpers.GetDirectionFromOffset(aLocation, bLocation - aLocation);
            var aFromB = HexagonHelpers.GetDirectionFromOffset(bLocation, aLocation - bLocation);

            if (!bFromA.HasValue || !aFromB.HasValue)
            {
                return (false, false);
            }

            var aBig = a.ConnectionLayout.GetLargeConnectorDirections(a.Rotation).Contains(bFromA.Value);
            var bBig = b.ConnectionLayout.GetLargeConnectorDirections(b.Rotation).Contains(aFromB.Value);
            var aSmall = a.ConnectionLayout.GetSmallConnectorDirections(a.Rotation).Contains(bFromA.Value);
            var bSmall = b.ConnectionLayout.GetSmallConnectorDirections(b.Rotation).Contains(aFromB.Value);

            Console.WriteLine($"a pos {aLocation} b pos {bLocation}");
            Console.WriteLine($"a->b dir {HexagonHelpers.GetDirectionString(bFromA)} ");
            Console.WriteLine($"b->a dir {HexagonHelpers.GetDirectionString(aFromB)} ");
            Console.WriteLine($"a rot: {a.Rotation} b rot: {b.Rotation}");
            Console.WriteLine($"a big: {aBig} small: {aSmall}");
            Console.WriteLine($"b big: {bBig} small: {bSmall}");
            Console.WriteLine("a small:");
            foreach (var dir in a.ConnectionLayout.SmallConectors)
            {
                Console.WriteLine(HexagonHelpers.GetDirectionString(dir));
            }
            Console.WriteLine("a large:");
            foreach (var dir in a.ConnectionLayout.LargeConnectors)
            {
                Console.WriteLine(HexagonHelpers.GetDirectionString(dir));
            }
            Console.WriteLine("b small:");
            foreach (var dir in b.ConnectionLayout.SmallConectors)
            {
                Console.WriteLine(HexagonHelpers.GetDirectionString(dir));
            }
            Console.WriteLine("b large:");
            foreach (var dir in b.ConnectionLayout.LargeConnectors)
            {
                Console.WriteLine(HexagonHelpers.GetDirectionString(dir));
            }

            var matchBig = aBig && bBig;
            var matchSmall = aSmall && bSmall;
            var noConnections = !aSmall && !aBig && !bSmall && !bBig;
            var connected = matchBig || matchSmall;
            return (connected || noConnections, connected);
        }

        public void SetSection(Point gridLocation, Section section)
        {
            section.GridLocation = gridLocation;
            Sections[gridLocation.X, gridLocation.Y] = section;
        }

        public void Remove(Point gridLocation)
        {
            Sections[gridLocation.X, gridLocation.Y] = null;
        }
    }
}
