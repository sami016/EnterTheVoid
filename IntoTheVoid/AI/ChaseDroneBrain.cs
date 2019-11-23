using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Utilities;
using IntoTheVoid.Flight;
using Microsoft.Xna.Framework;

namespace IntoTheVoid.AI
{
    public class ChaseDroneBrain : Brain
    {
        private CompletionTimer _fireTimer = new CompletionTimer(TimeSpan.FromSeconds(3));
        private CompletionTimer _oscillateTimer = new CompletionTimer(TimeSpan.FromSeconds(5));

        public PositionChaserBehaviour PositionChaserBehaviour { get; private set; }

        private bool _oscillateMode = false;
        private float _pos = 0f;

        private readonly FlightShip _playerShip;
        private readonly float _circleRadius;
        private readonly Vector3 _playerOffset;

        public ChaseDroneBrain(FlightShip playerShip, Vector3 playerOffset, float circleRadius)
        {
            _playerShip = playerShip;
            _circleRadius = circleRadius;
            _playerOffset = playerOffset;
        }

        public override void Initialise()
        {
            PositionChaserBehaviour = new PositionChaserBehaviour(FlightShip, Transform, _playerShip.Entity.Get<Transform>().Location);
        }

        public override void Tick(TickContext context)
        {
            _pos += context.DeltaTimeSeconds * (float)(Math.PI / 10f);
            PositionChaserBehaviour.Target = _playerShip.Entity.Get<Transform>().Location + new Vector3(
                (float)Math.Cos(_pos) * _circleRadius,
                0f, 
                (float)Math.Sin(_pos) * _circleRadius
            ) + _playerOffset;
            PositionChaserBehaviour.Tick(context);

            _fireTimer.Tick(context.DeltaTime);
            if (_fireTimer.Completed)
            {
                WeaponCapability.StandardFire();
                _fireTimer.Restart();
            }
            _oscillateTimer.Tick(context.DeltaTime);
            if (_oscillateTimer.Completed)
            {
                _oscillateMode = !_oscillateMode;
                _oscillateTimer.Restart();

                var aimAt = _playerShip.GetNodeForSection(_playerShip.Topology.AllSections.First().GridLocation).GlobalLocation;
                var lookAt = Matrix.CreateLookAt(Transform.Location, aimAt, Vector3.Up);
                Transform.Rotation = Quaternion.CreateFromRotationMatrix(lookAt);
            }
            // Rotate controls.
            RocketCapability.RocketControl = new RocketControl
            {
                //RotatePort = _oscillateMode,
            };
        }
    }
}
