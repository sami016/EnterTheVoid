﻿using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
using Forge.Core.Interfaces;
using Forge.Core.Utilities;
using EnterTheVoid.Flight;
using EnterTheVoid.Ships.Generation;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnterTheVoid.Phases.Asteroids
{
    public class AsteroidSpawner : Component, IInit, ITick
    {
        private static readonly Random Random = new Random();
        private readonly PhaseDistanceTarget _distanceTarget;
        private readonly Distribution<Type> _distribution;
        private readonly int _difficult;
        private readonly int _spawnPerWave;
        private IList<Entity> _entities = new List<Entity>();
        private float _lastZ = -5f;
        private bool _running = true;

        [Inject] FlightShip FlightShip { get; set; }
        private Transform _flightShipTransform;

        public AsteroidSpawner(int difficult, Distribution<Type> distribution, PhaseDistanceTarget distanceTarget = null)
        {
            _distanceTarget = distanceTarget;
            _distribution = distribution;
            _difficult = difficult;
            _spawnPerWave = 1 + (int)Math.Ceiling(_difficult / 25.0);
        }

        public void Stop()
        {
            this.Update(() =>
            {
                _running = false;
            });
        }

        public void Initialise()
        {
            _flightShipTransform = FlightShip.Entity.Get<Transform>();
        }

        public void Tick(TickContext context)
        {
            CheckSpawn(context);
            CheckClear(context);
        }

        private void CheckSpawn(TickContext context)
        {
            // Don't spawn when within 5 of target.
            var remaining = _distanceTarget?.Remaining;
            if (remaining.HasValue && (-remaining.Value) < 40)
            {
                return;
            }
            if (_running)
            {
                // Locks spawn to once ever 3 units is moved.
                if (_lastZ > _flightShipTransform.Location.Z)
                {
                    _lastZ -= (int)Math.Ceiling((100 - _difficult) / 10.0);
                    for (var i = 0; i < _spawnPerWave; i++)
                    {
                        var ent = Entity.Create();
                        ent.Add(new Transform()
                        {
                            Location = _flightShipTransform.Location + Vector3.Forward * 30 + new Vector3((float)(Random.NextDouble() - 0.5f) * 50, 0f, (float)Random.NextDouble() * 20)
                        });
                        var asteroidComponent = Activator.CreateInstance(_distribution.Sample());
                        (asteroidComponent as IVelocity).Velocity = new Vector3((float)Random.NextDouble() - 0.5f, 0f, (float)(0.8f + Random.NextDouble())) * 0.3f;
                        ent.Add(asteroidComponent as IComponent);
                        _entities.Add(ent);
                    }
                }
            }
        }

        private void CheckClear(TickContext context)
        {
            foreach (var asteroidEntity in _entities.ToArray())
            {
                var transform = asteroidEntity.Get<Transform>();
                if (transform.Location.Z > _flightShipTransform.Location.Z + 25)
                {
                    asteroidEntity.Delete();
                    _entities.Remove(asteroidEntity);
                }
            }
        }
    }
}
