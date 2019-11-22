using System;
using System.Collections.Generic;
using System.Text;
using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Utilities;
using IntoTheVoid.Flight;

namespace IntoTheVoid.AI
{
    public class ChaseDroneBrain : Brain
    {
        private CompletionTimer _fireTimer = new CompletionTimer(TimeSpan.FromSeconds(3));
        private CompletionTimer _oscillateTimer = new CompletionTimer(TimeSpan.FromSeconds(5));
        private CompletionTimer _rotateOscillateTimer = new CompletionTimer(TimeSpan.FromSeconds(1));

        private EnemyHarness _enemyHarness;
        private bool _oscillateMode = false;
        private bool _rotateOscillateMode = false;

        private readonly FlightShip _playerShip;
        private readonly float _fixXBase;
        private readonly float _fixZBase;

        public ChaseDroneBrain(FlightShip playerShip, float fixXBase, float fixZBase = 15f)
        {
            _playerShip = playerShip;
            _fixXBase = fixXBase;
            _fixZBase = fixZBase;
        }

        public override void Initialise()
        {
            _enemyHarness = Entity.Add(new EnemyHarness(_playerShip)
            {
                FixZ = _fixZBase,
                FixX = _fixXBase
            });
        }

        public override void Tick(TickContext context)
        {
            if (_enemyHarness.FixZ > 10)
            {
                _enemyHarness.FixZ -= 1f * context.DeltaTimeSeconds;
            }
            else if(_enemyHarness.FixZ < 10)
            {
                _enemyHarness.FixZ += 1f * context.DeltaTimeSeconds;
            } else
            {
                _enemyHarness.FixZ = 10f;
            }
            _enemyHarness.FixX += context.DeltaTimeSeconds * (_oscillateMode ? 1f : -1f);
            RocketCapability.RocketControl = new RocketControl
            {
                Forwards = true,
                RotatePort = _oscillateMode,
                RotateStarboard = !_oscillateMode
            };

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
            }
            _rotateOscillateTimer.Tick(context.DeltaTime);
            if (_oscillateTimer.Completed)
            {
                _rotateOscillateMode = !_rotateOscillateMode;
                _rotateOscillateTimer.Restart();
            }
        }
    }
}
