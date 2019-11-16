using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
using Forge.Core.Interfaces;
using GreatSpaceRace.Ships;
using GreatSpaceRace.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Flight
{
    public class FlightShip : Component, IInit, ITick
    {
        private readonly ShipTopology _topology;
        public Vector3 Velocity { get; set; } = Vector3.Zero;
        [Inject] Transform Transform { get; set; }

        public FlightShip(ShipTopology topology)
        {
            _topology = topology;
        }

        public void Initialise()
        {
            this.Update(() =>
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
            });
        }

        public void Tick(TickContext context)
        {
            Transform.Update(() =>
            {
                Transform.Location += Velocity * context.DeltaTimeSeconds;
                Console.WriteLine($"Ship location: {Transform.Location}   (Velocity: {Velocity})");
            });
        }
    }
}
