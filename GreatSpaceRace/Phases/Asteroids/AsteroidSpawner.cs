using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
using Forge.Core.Interfaces;
using Forge.Core.Utilities;
using GreatSpaceRace.Flight;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreatSpaceRace.Phases.Asteroids
{
    public class AsteroidSpawner : Component, IInit, ITick
    {
        private static readonly Random Random = new Random();
        private IList<Entity> _entities = new List<Entity>();
        private float _lastZ = -5f;
        private bool _running = true;

        [Inject] FlightShip FlightShip { get; set; }
        private Transform _flightShipTransform;

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
            if (_running 
                && _entities.Count() <= 20)
            {
                // Locks spawn to once ever 3 units is moved.
                if (_lastZ > _flightShipTransform.Location.Z)
                {
                    _lastZ -= 3;
                    for (var i = 0; i < 10; i++)
                    {
                        var ent = Entity.Create();
                        ent.Add(new Transform()
                        {
                            Location = _flightShipTransform.Location + Vector3.Forward * 30 + new Vector3((float)(Random.NextDouble() - 0.5f) * 50, 0f, (float)Random.NextDouble() * 20)
                        });
                        ent.Add(new Asteroid()
                        {
                            Velocity = new Vector3((float)Random.NextDouble() - 0.5f, 0f, (float)(0.8f + Random.NextDouble())) * 0.3f
                        });
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
