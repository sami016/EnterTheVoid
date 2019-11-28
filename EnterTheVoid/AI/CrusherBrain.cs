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
    public class CrusherBrain : Brain
    {
        private readonly CompletionTimer _fireTimer = new CompletionTimer(TimeSpan.FromSeconds(3));
        private readonly CompletionTimer _alternateTimer = new CompletionTimer(TimeSpan.FromSeconds(7));
        private readonly CompletionTimer _speedAlternateTimer = new CompletionTimer(TimeSpan.FromSeconds(6));
        private readonly CompletionTimer _shieldTimer = new CompletionTimer(TimeSpan.FromSeconds(10));

        private PositionChaserBehaviour _positionChaserBehaviour;
        private DeathRunBehaviour _deathRunBehaviour;
        private bool _shootMode;
        private bool _speedMode;
        private readonly FlightShip _playerShip;
        private readonly Vector3 _playerOffset;

        public bool Active { get; set; } = false;

        public CrusherBrain(FlightShip playerShip, Vector3 playerOffset)
        {
            _playerShip = playerShip;
            _playerOffset = playerOffset;
        }

        public override void Initialise()
        {
            _positionChaserBehaviour = new PositionChaserBehaviour(FlightShip, Transform, _playerShip.Entity.Get<Transform>().Location)
            {
               CatchupSpeed = 2f 
            };
            _deathRunBehaviour = new DeathRunBehaviour(FlightShip, _playerShip, _positionChaserBehaviour)
            {
                SectionLowBound = 10
            };
        }

        public override void Tick(TickContext context)
        {
            if (!Active)
            {
                return;
            }
            _deathRunBehaviour.Tick(context);
            if (!_deathRunBehaviour.Running)
            {
                _positionChaserBehaviour.Target = _playerShip.Entity.Get<Transform>().Location + _playerOffset;
            }
            _positionChaserBehaviour.Tick(context);

            _speedAlternateTimer.Tick(context.DeltaTime);
            if (_speedAlternateTimer.Completed)
            {
                _speedAlternateTimer.Restart();
                _speedMode = !_speedMode;
                //_positionChaserBehaviour.CatchupSpeed = _speedMode ? 0f : 2f;
                _speedAlternateTimer.ChangeTarget(_speedMode ? TimeSpan.FromSeconds(9999) : TimeSpan.FromSeconds(1));
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
                }
                _fireTimer.Restart();
            }

            _shieldTimer.Tick(context.DeltaTime);
            if (_shieldTimer.Completed)
            {
                _playerShip.AddEnergy(100);
                WeaponCapability.ShieldDeploy();
            }
        }
    }
}
