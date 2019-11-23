using Forge.Core;
using Forge.Core.Engine;
using Forge.Core.Components;
using IntoTheVoid.Flight;
using IntoTheVoid.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntoTheVoid.AI
{
    public class TakeAimBehaviour
    {
        private static readonly Random Random = new Random();

        private readonly FlightShip _playerShip;
        private readonly FlightShip _flightShip;
        private readonly Transform _transform;

        private float aimOffset = (float)Random.NextDouble() * 0.2f - 0.1f;

        public float RotationRate { get; set; } = 0.1f;

        public TakeAimBehaviour(FlightShip playerShip, FlightShip flightShip, Transform transform)
        {
            _playerShip = playerShip;
            _flightShip = flightShip;
            _transform = transform;
        }

        public void Tick(TickContext context)
        {
            var displacement = (_playerShip.GetNodes().First().GloalLocation - _transform.Location);
            var rotation = -RotationHelper.GetAngle(displacement.X, displacement.Z) - (float)Math.PI / 2 + aimOffset;
            var rotVel = 0f;
            if (_flightShip.Rotation > rotation + RotationRate)
            {
                rotVel -= 0.01f;
            }
            else if (_flightShip.Rotation < rotation - 0.01f)
            {
                rotVel = 0.01f;
            }
            _flightShip.Update(() =>
            {
                _flightShip.RotationalSpeed = rotVel;
                if (rotVel == 0)
                {
                _flightShip.Rotation = rotation;
                }
            });
        }

        public void SetRandomAimOffset(float randomArcAngle = 0.5f)
        {
            aimOffset = (float)Random.NextDouble() * randomArcAngle/2 - randomArcAngle/2;
        }

        public void SetAimOffset(float angle)
        {
            aimOffset = angle;
        }
    }
}
