﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Utilities;
using EnterTheVoid.Flight;
using EnterTheVoid.Utility;
using Microsoft.Xna.Framework;
using Forge.Core.Engine;

namespace EnterTheVoid.AI
{
    public class GalactusBrain : Brain
    {
        private CompletionTimer _fireTimer = new CompletionTimer(TimeSpan.FromSeconds(3));
        private CompletionTimer _changeAimTimer = new CompletionTimer(TimeSpan.FromSeconds(5));
        private CompletionTimer _alternateTimer = new CompletionTimer(TimeSpan.FromSeconds(7));

        private PositionChaserBehaviour _positionChaserBehaviour;
        private TakeAimBehaviour _takeAimBehaviour;

        private bool _oscillateMode = false;
        private float _pos = 0f;
        private bool _shootMode;
        private readonly FlightShip _playerShip;
        private readonly float _circleRadius;
        private readonly Vector3 _playerOffset;

        public GalactusBrain(FlightShip playerShip, Vector3 playerOffset, float circleRadius)
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
            _takeAimBehaviour = new TakeAimBehaviour(_playerShip, FlightShip, Transform)
            {
                RotationRate = 0.001f
            };
        }

        public override void Tick(TickContext context)
        {
            _pos += context.DeltaTimeSeconds * (float)(Math.PI / 10f);
            _positionChaserBehaviour.Target = _playerShip.Entity.Get<Transform>().Location + new Vector3(
                (float)Math.Cos(_pos) * _circleRadius,
                0f, 
                (float)Math.Sin(_pos) * _circleRadius
            ) + _playerOffset;
            _positionChaserBehaviour.Tick(context);
            //_takeAimBehaviour.Tick(context);

            _fireTimer.Tick(context.DeltaTime);
            if (_fireTimer.Completed)
            {
                WeaponCapability.StandardFire();
                _fireTimer.Restart();
            }
            //_changeAimTimer.Tick(context.DeltaTime);
            //if (_changeAimTimer.Completed)
            //{
            //    _oscillateMode = !_oscillateMode;
            //    _changeAimTimer.Restart();
            //    _takeAimBehaviour.SetRandomAimOffset((float)Math.PI/3);
            //}

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
                    WeaponCapability.BombardFire();
                }
                else
                {
                    WeaponCapability.StandardFire();
                }
                _fireTimer.Restart();
            }

            WeaponCapability.ShieldDeploy();
            WeaponCapability.RocketBlast();

            FlightShip.Update(() =>
            {
                FlightShip.Rotation += 0.001f * context.DeltaTimeSeconds;
                FlightShip.AddEnergy(100);
            });
        }
    }
}
