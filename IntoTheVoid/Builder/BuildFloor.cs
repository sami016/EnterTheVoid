using Forge.Core.Components;
using Forge.Core.Interfaces;
using GreatSpaceRace.Constants;
using GreatSpaceRace.Ships;
using GreatSpaceRace.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Builder
{
    public class BuildFloor : Component, IInit
    {
        private readonly ShipTopology _shipTopology;

        public BuildFloor(ShipTopology shipTopology)
        {
            _shipTopology = shipTopology;
        }

        public void Initialise()
        {
            for (var gridX=0; gridX< _shipTopology.GridWidth; gridX++)
            {
                for (var gridZ=0; gridZ< _shipTopology.GridHeight; gridZ++)
                {
                    var gridPosition = new Point(gridX, gridZ);
                    var child = Entity.Create();
                    child.Add(new Transform
                    {
                        Location = HexagonHelpers.GetGridWorldPosition(gridPosition),
                        Rotation = Quaternion.CreateFromYawPitchRoll(0, 0, 0)
                    });
                    child.Add(new BuildNode(gridPosition, _shipTopology));
                }
            }
        }
    }
}
