using Forge.Core.Components;
using Forge.Core.Interfaces;
using GreatSpaceRace.Ships;
using GreatSpaceRace.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Flight
{
    public class FlightShip : Component, IInit
    {
        private readonly ShipTopology _topology;

        public FlightShip(ShipTopology topology)
        {
            _topology = topology;
        }

        public void Initialise()
        {
            for (var gridX = 0; gridX < _topology.GridWidth; gridX++)
            {
                for (var gridZ = 0; gridZ < _topology.GridHeight; gridZ++)
                {
                    var gridPosition = new Point(gridX, gridZ);
                    var child = Entity.Create();
                    child.Add(new Transform
                    {
                        Location = HexagonHelpers.GetGridWorldPosition(gridPosition),
                        Rotation = Quaternion.CreateFromYawPitchRoll(0, 0, 0)
                    });
                    child.Add(new FlightNode(gridPosition, _topology));
                }
            }
        }
    }
}
