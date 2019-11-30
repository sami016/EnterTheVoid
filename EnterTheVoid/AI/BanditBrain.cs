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
    public class BanditBrain : Brain
    {
        private CompletionTimer _fireTimer = new CompletionTimer(TimeSpan.FromSeconds(3));
        private CompletionTimer _changeAimTimer = new CompletionTimer(TimeSpan.FromSeconds(5));

        private PositionChaserBehaviour _positionChaserBehaviour;
        private TakeAimBehaviour _takeAimBehaviour;
        private DeathRunBehaviour _deathRunBehaviour;
        private bool _oscillateMode = false;
        private float _pos = 0f;

        private readonly FlightShip _playerShip;
        private readonly float _circleRadius;
        private readonly Vector3 _playerOffset;

        public BanditBrain(FlightShip playerShip, Vector3 playerOffset, float circleRadius)
        {
            _playerShip = playerShip;
            _circleRadius = circleRadius;
            _playerOffset = playerOffset;
        }

        public override void Initialise()
        {
            _positionChaserBehaviour = new PositionChaserBehaviour(FlightShip, Transform, _playerShip.Entity.Get<Transform>().Location)
            {
               CatchupSpeed = 0.1f 
            };
            _takeAimBehaviour = new TakeAimBehaviour(_playerShip, FlightShip, Transform);

            _deathRunBehaviour = new DeathRunBehaviour(FlightShip, _playerShip, _positionChaserBehaviour)
            {
                SectionLowBound = 5
            };
        }

        public override void Tick(TickContext context)
        {
            _deathRunBehaviour.Tick(context);

            _pos += context.DeltaTimeSeconds * (float)(Math.PI / 10f);
            _positionChaserBehaviour.Target = _playerShip.Entity.Get<Transform>().Location + new Vector3(
                (float)Math.Cos(_pos) * _circleRadius,
                0f, 
                (float)Math.Sin(_pos) * _circleRadius
            ) + _playerOffset;
            _positionChaserBehaviour.Tick(context);
            _takeAimBehaviour.Tick(context);

            _fireTimer.Tick(context.DeltaTime);
            if (_fireTimer.Completed)
            {
                WeaponCapability.StandardFire();
                _fireTimer.Restart();
            }
            _changeAimTimer.Tick(context.DeltaTime);
            if (_changeAimTimer.Completed)
            {
                _oscillateMode = !_oscillateMode;
                _changeAimTimer.Restart();
                _takeAimBehaviour.SetRandomAimOffset((float)Math.PI/3);
            }
        }
    }
}
