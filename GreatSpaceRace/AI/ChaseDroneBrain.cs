using System;
using System.Collections.Generic;
using System.Text;
using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Utilities;
using GreatSpaceRace.Flight;

namespace GreatSpaceRace.AI
{
    public class ChaseDroneBrain : Brain
    {
        private CompletionTimer _fireTimer = new CompletionTimer(TimeSpan.FromSeconds(3));
        private CompletionTimer _oscillateTimer = new CompletionTimer(TimeSpan.FromSeconds(5));
        private CompletionTimer _rotateOscillateTimer = new CompletionTimer(TimeSpan.FromSeconds(1));
        [Inject] EnemyHarness EnemyHarness { get; set; }
        private bool _oscillateMode = false;
        private bool _rotateOscillateMode = false;

        public override void Tick(TickContext context)
        {
            if (EnemyHarness.FixZ > 10)
            {
                EnemyHarness.FixZ -= 1f * context.DeltaTimeSeconds;
            } else
            {
                EnemyHarness.FixZ = 10f;
            }
            EnemyHarness.FixX += context.DeltaTimeSeconds * (_oscillateMode ? 1f : -1f);
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
