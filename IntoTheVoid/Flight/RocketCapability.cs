using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
using Forge.Core.Interfaces;
using IntoTheVoid.Ships;
using IntoTheVoid.Ships.Modules;
using IntoTheVoid.Upgrades;
using IntoTheVoid.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntoTheVoid.Flight
{
    public struct RocketControl
    {
        public bool Forwards;
        public bool Backwards;
        public bool Left;
        public bool Right;
        public bool RotateStarboard;
        public bool RotatePort;
    }

    public class RocketCapability : Component, ITick
    {
        private ShipTopology _topology => FlightShip.Topology;
        private readonly Dictionary<int, int> _accelerationCapabilities;

        [Inject] FlightShip FlightShip { get; set; }
        [Inject] Transform Transform { get; set; }

        private static float Sin60 = (float)Math.Sin(Math.PI / 3);
        private static float Sin30 = (float)Math.Sin(Math.PI / 6);

        public RocketControl RocketControl { get; set; }

        public RocketCapability()
        {
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


            var acceleration = Vector3.Zero;
            if (RocketControl.Forwards)
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
            if (RocketControl.Left)
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
            if (RocketControl.Right)
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
            if (RocketControl.Backwards)
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

            var rotationDiff = 0f;
            var rotationCoefficient = Math.Max(rotationSpeedBase + rotationCapabilities - sectionCount * rotationPenalty * 0.05f, 0.5f);
            if (RocketControl.RotatePort)
            {
                rotationDiff = 1 * context.DeltaTimeSeconds * 0.3f * rotationCoefficient;
            }
            else if (RocketControl.RotateStarboard)
            {
                rotationDiff = -1 * context.DeltaTimeSeconds * 0.3f * rotationCoefficient;
            }
            else
            {
                //if (Math.Abs(rotation) < 0.001)
                //{
                //    rotation = 0;
                //}
                //else
                //{
                //    rotation += -1 * context.DeltaTimeSeconds * 0.2f * rotationCoefficient * (float)Math.Sin((Math.PI / 2) * rotation / Math.PI);
                //}
            }
            if (rotationDiff > Math.PI)
            {
                rotationDiff = -(float)Math.PI * 2;
            }
            if (rotationDiff < -Math.PI)
            {
                rotationDiff = (float)Math.PI * 2;
            }



            FlightShip.Update(() =>
            {
                FlightShip.Rotation += rotationDiff;
                var velocity = FlightShip.Velocity + context.DeltaTimeSeconds * Vector3.Transform(acceleration, Transform.Rotation);
                FlightShip.Velocity = velocity;
            });
        }
    }
}
