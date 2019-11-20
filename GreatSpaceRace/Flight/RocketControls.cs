using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
using Forge.Core.Interfaces;
using GreatSpaceRace.Ships;
using GreatSpaceRace.Ships.Modules;
using GreatSpaceRace.Upgrades;
using GreatSpaceRace.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private static float Sin60 = (float)Math.Sin(Math.PI / 3);
        private static float Sin30 = (float)Math.Sin(Math.PI / 6);

        public RocketControls(ShipTopology topology)
        {
            _topology = topology;
            _accelerationCapabilities = new Dictionary<int, int>();
        }

        public void Tick(TickContext context)
        {
            float passiveAccel = 0.15f;
            float passiveAccelBackwards = 0.25f;
            float rocketAccel = 0.5f;
            float rotationPenalty = 1f;
            float rotationSpeedBase = 1f;
            //float rotationLimit = (float)Math.PI;// * 2 / 6;

            var upgrades = FlightShip.Upgrades
                .Select(x => x.GetType())
                .ToArray();

            if (upgrades.Contains(typeof(EnhancedRotation))) 
            {
                rotationSpeedBase += 1f;
                rotationPenalty = 0f;
                //rotationLimit = (float)Math.PI;
            }
            if (upgrades.Contains(typeof(PrecisionRocketry)))
            {
                passiveAccel += 0.1f;
                passiveAccelBackwards += 0.1f;
            }

            var keys = Keyboard.GetState();
            //TODO - exclude damanged/destroyed.
            _accelerationCapabilities[0] = 0;
            _accelerationCapabilities[1] = 0;
            _accelerationCapabilities[2] = 0;
            _accelerationCapabilities[3] = 0;
            _accelerationCapabilities[4] = 0;
            _accelerationCapabilities[5] = 0;

            var rocketModuleSections = new List<Section>();

            // Accumulate acceleration capability.
            var rotationCapabilities = 0;
            var sectionCount = 0;
            foreach (var section in _topology.AllSections)
            {
                sectionCount++;
                if (section.Module is RocketModule rocketModule)
                {
                    _accelerationCapabilities[section.Rotation]++;
                    rocketModule.On = false;
                    rocketModuleSections.Add(section);
                }
                if (section.Module is RotaryEngine)
                {
                    rotationCapabilities++;
                }
            }

            var rotation = _rotation;

            var acceleration = Vector3.Zero;
            if (keys.IsKeyDown(Keys.W))
            {
                acceleration += Vector3.Forward * (passiveAccel + rocketAccel * _accelerationCapabilities[(int)OffDirection.South]);

                // Activate rockets for effects.
                foreach (var section in rocketModuleSections)
                {
                    if (section.Rotation == (int)OffDirection.South)
                    {
                        (section.Module as RocketModule).On = true;
                    }
                }
            }
            if (keys.IsKeyDown(Keys.A))
            {
                acceleration += Vector3.Left * (passiveAccel + rocketAccel * (_accelerationCapabilities[(int)OffDirection.SouthEast] + _accelerationCapabilities[(int)OffDirection.NorthEast]) * Sin60);
                acceleration += Vector3.Forward * rocketAccel * _accelerationCapabilities[(int)OffDirection.SouthEast] * Sin30;
                acceleration += Vector3.Backward * rocketAccel * _accelerationCapabilities[(int)OffDirection.NorthEast] * Sin30;

                // Activate rockets for effects.
                foreach (var section in rocketModuleSections)
                {
                    if (section.Rotation == (int)OffDirection.SouthEast
                        || section.Rotation == (int)OffDirection.NorthEast)
                    {
                        (section.Module as RocketModule).On = true;
                    }
                }
            }
            if (keys.IsKeyDown(Keys.D))
            {
                acceleration += Vector3.Right * (passiveAccel + rocketAccel * (_accelerationCapabilities[(int)OffDirection.SouthWest] + _accelerationCapabilities[(int)OffDirection.NorthWest]) * Sin60);
                acceleration += Vector3.Forward * rocketAccel * _accelerationCapabilities[(int)OffDirection.SouthWest] * Sin30;
                acceleration += Vector3.Backward * rocketAccel * _accelerationCapabilities[(int)OffDirection.NorthWest] * Sin30;

                // Activate rockets for effects.
                foreach (var section in rocketModuleSections)
                {
                    if (section.Rotation == (int)OffDirection.SouthWest
                        || section.Rotation == (int)OffDirection.NorthWest)
                    {
                        (section.Module as RocketModule).On = true;
                    }
                }
            }
            if (keys.IsKeyDown(Keys.S))
            {
                acceleration += Vector3.Backward * (passiveAccel + rocketAccel * _accelerationCapabilities[(int)OffDirection.South]);

                // Activate rockets for effects.
                foreach (var section in rocketModuleSections)
                {
                    if (section.Rotation == (int)OffDirection.North)
                    {
                        (section.Module as RocketModule).On = true;
                    }
                }
            }

            var rotationCoefficient = Math.Max(rotationSpeedBase + rotationCapabilities - sectionCount * rotationPenalty * 0.05f, 0.5f);
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
                    rotation += -1 * context.DeltaTimeSeconds * 0.2f * rotationCoefficient * (float)Math.Sin((Math.PI / 2) * rotation / Math.PI);
                }
            }
            if (rotation > Math.PI)
            {
                rotation -= (float)Math.PI * 2;
            }
            if (rotation < -Math.PI)
            {
                rotation += (float)Math.PI * 2;
            }

            var rotationQuaternion = Quaternion.CreateFromYawPitchRoll(rotation, 0, 0);

            var velocity = FlightShip.Velocity + context.DeltaTimeSeconds * Vector3.Transform(acceleration, rotationQuaternion);
            if (velocity.Z > 1)
            {
                velocity = new Vector3(FlightShip.Velocity.X, FlightShip.Velocity.Y, FlightShip.Velocity.Z * 0.98f);
            }

            FlightShip.Update(() =>
            {
                _rotation = rotation;
                FlightShip.Velocity = velocity;
                Transform.Rotation = rotationQuaternion;
            });
        }
    }
}
