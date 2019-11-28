using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Utilities;
using EnterTheVoid.Flight;
using EnterTheVoid.Utility;
using Microsoft.Xna.Framework;

namespace EnterTheVoid.AI
{
    public class SwarmShipBrain : Brain
    {
        private readonly CompletionTimer _fireTimer = new CompletionTimer(TimeSpan.FromSeconds(3));
        private readonly CompletionTimer _alternateTimer = new CompletionTimer(TimeSpan.FromSeconds(7));
        private readonly CompletionTimer _shieldTimer = new CompletionTimer(TimeSpan.FromSeconds(10));
        private readonly CompletionTimer _despawnTimer = new CompletionTimer(TimeSpan.FromSeconds(20));

        private PositionChaserBehaviour _positionChaserBehaviour;
        private bool _shootMode;
        private readonly FlightShip _playerShip;

        private Vector3 _pos = Vector3.Zero;

        [Inject] RocketCapability RocketCapability { get; set; }

        public bool Active { get; set; } = false;

        public SwarmShipBrain(FlightShip playerShip)
        {
            _playerShip = playerShip;
        }

        public override void Initialise()
        {
            _positionChaserBehaviour = new PositionChaserBehaviour(FlightShip, Transform, _playerShip.Entity.Get<Transform>().Location)
            {
               CatchupSpeed = 1f 
            };

            var direction = Random.Next(3);
            var rotate = Random.Next(3);

            RocketCapability.RocketControl = new RocketControl
            {
                Forwards = true,
                Left = direction == 0,
                Right = direction == 2,
                //RotatePort = rotate == 0,
                //RotateStarboard = rotate == 2,
            };
            //FlightShip.Velocity = new Vector3(2f + 2f * (float)Random.NextDouble(), 0, 0);
        }

        public override void Tick(TickContext context)
        {
            if (!Active)
            {
                return;
            }

            _alternateTimer.Tick(context.DeltaTime);
            if (_alternateTimer.Completed)
            {
                _alternateTimer.Restart();
                _shootMode = !_shootMode;
            }

            _fireTimer.Tick(context.DeltaTime);
            if (_fireTimer.Completed)
            {
                if (_shootMode)
                {
                    WeaponCapability.StandardFire();
                } else
                {
                    WeaponCapability.HeavyFire();
                }
                _fireTimer.Restart();
            }

            _shieldTimer.Tick(context.DeltaTime);
            if (_shieldTimer.Completed)
            {
                _playerShip.AddEnergy(100);
                WeaponCapability.ShieldDeploy();
            }

            _despawnTimer.Tick(context.DeltaTime);
            if (_despawnTimer.Completed)
            {
                Entity.Delete();
            }
        }
    }
}
