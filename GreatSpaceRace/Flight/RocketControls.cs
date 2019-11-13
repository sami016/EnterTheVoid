using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
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
        private float _rotation = 0;
        private const float rotationLimit = (float)Math.PI / 6;

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
            var rotationCapabilities = 0;
            var sectionCount = 0;
            foreach (var section in _topology.Sections)
            {
                if (section != null)
                {
                    sectionCount++;
                    if (section.Module is RocketModule)
                    {
                        _accelerationCapabilities[section.Rotation]++;
                    }
                    if (section.Module is RotaryEngine)
                    {
                        rotationCapabilities++;
                    }
                }
            }

            var velocity = FlightShip.Velocity;
            var rotation = _rotation;

            if (keys.IsKeyDown(Keys.W))
            {
                velocity += Vector3.Forward * context.DeltaTimeSeconds * (0.2f + 0.1f * _accelerationCapabilities[(int)OffDirection.South]);
            }
            if (keys.IsKeyDown(Keys.A))
            {
                velocity += Vector3.Left * context.DeltaTimeSeconds * (0.2f + 0.1f * _accelerationCapabilities[(int)OffDirection.South]);
            }
            if (keys.IsKeyDown(Keys.D))
            {
                velocity += Vector3.Right * context.DeltaTimeSeconds * (0.2f + 0.1f * _accelerationCapabilities[(int)OffDirection.South]);
            }
            if (keys.IsKeyDown(Keys.S))
            {
                velocity += Vector3.Backward * context.DeltaTimeSeconds * (0.05f + 0.1f * _accelerationCapabilities[(int)OffDirection.South]);
            }
            if (velocity.Z < -1)
            {
                velocity = new Vector3(FlightShip.Velocity.X, FlightShip.Velocity.Y, FlightShip.Velocity.Z * 0.98f);
            }

            var rotationCoefficient = Math.Max(1 + rotationCapabilities - sectionCount * 0.05f, 0.5f);
            if (keys.IsKeyDown(Keys.Q))
            {
                rotation += 1 * context.DeltaTimeSeconds * 0.3f * rotationCoefficient;
            }
            else if (keys.IsKeyDown(Keys.E))
            {
                rotation += -1 * context.DeltaTimeSeconds * 0.3f * rotationCoefficient;
            } else
            {
                if (Math.Abs(rotation) < 0.001)
                {
                    rotation = 0;
                }
                else
                {
                    rotation += -1 * context.DeltaTimeSeconds * 0.2f * rotationCoefficient * (float)Math.Sin((Math.PI / 2) * rotation / rotationLimit);
                }
            }
            if (rotation > rotationLimit)
            {
                rotation = rotationLimit;
            }
            if (rotation < -rotationLimit)
            {
                rotation = -rotationLimit;
            }


            FlightShip.Update(() =>
            {
                _rotation = rotation;
                FlightShip.Velocity = velocity;
                Transform.Rotation = Quaternion.CreateFromYawPitchRoll(rotation, 0, 0);
            });
        }
    }
}
