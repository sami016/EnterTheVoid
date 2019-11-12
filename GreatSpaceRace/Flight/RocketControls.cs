using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Interfaces;
using GreatSpaceRace.Ships;
using GreatSpaceRace.Ships.Modules;
using GreatSpaceRace.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Flight
{
    public class RocketControls : Component, ITick
    {
        private readonly ShipTopology _topology;
        private readonly Dictionary<int, int> _accelerationCapabilities;

        [Inject] FlightShip FlightShip { get; set; }
        [Inject] Transform Transform { get; set; }

        public RocketControls(ShipTopology topology)
        {
            _topology = topology;
            _accelerationCapabilities = new Dictionary<int, int>();
        }

        public void Tick(TickContext context)
        {
            var keys = Keyboard.GetState();
            //TODO - exclude damanged/destroyed.
            _accelerationCapabilities[0] = 0;
            _accelerationCapabilities[1] = 0;
            _accelerationCapabilities[2] = 0;
            _accelerationCapabilities[3] = 0;
            _accelerationCapabilities[4] = 0;
            _accelerationCapabilities[5] = 0;

            // Accumulate acceleration capability.
            foreach (var section in _topology.Sections)
            {
                if (section != null)
                {
                    if (section.Module is RocketModule)
                    {
                        _accelerationCapabilities[section.Rotation]++;
                    }
                }
            }

            if (keys.IsKeyDown(Keys.W))
            {
                FlightShip.Velocity += Vector3.Forward * context.DeltaTimeSeconds * (0.1f + 0.05f * _accelerationCapabilities[(int)OffDirection.South]);
            }
            if (keys.IsKeyDown(Keys.S))
            {
                FlightShip.Velocity += Vector3.Backward * context.DeltaTimeSeconds * (0.05f + 0.05f * _accelerationCapabilities[(int)OffDirection.South]);
                if (FlightShip.Velocity.Z < -1)
                {
                    FlightShip.Velocity = new Vector3(FlightShip.Velocity.X, FlightShip.Velocity.Y, FlightShip.Velocity.Z * 0.98f);
                }
            }


        }
    }
}
