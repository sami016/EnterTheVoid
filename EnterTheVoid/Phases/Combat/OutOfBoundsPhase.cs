using Forge.Core.Components;
using Forge.Core.Engine;
using EnterTheVoid.AI;
using EnterTheVoid.Flight;
using EnterTheVoid.Ships;
using EnterTheVoid.Ships.Connections;
using EnterTheVoid.Ships.Modules;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forge.Core.Interfaces;
using Forge.Core;
using Forge.Core.Utilities;
using EnterTheVoid.Phases.Asteroids;

namespace EnterTheVoid.Phases.Combat
{
    public class OutOfBoundsPhase : Phase
    {
        private IList<Entity> _enemies = new List<Entity>();
        private IList<CrusherBrain> _enemyBrains = new List<CrusherBrain>();

        FlightShip Ship { get; set; }

        private readonly CompletionTimer _spawnTimer = new CompletionTimer(TimeSpan.FromSeconds(3));
        private readonly CompletionTimer _phaseSpawnTimer = new CompletionTimer(TimeSpan.FromSeconds(40));

        private FlightCameraControl _camera;
        private float _oldCameraScale;
        private PhaseKillTarget _target;
        private int _wave = 0;
        private AsteroidSpawner _asteroidSpawner;
        private PhaseDistanceTarget _phaseDistanceTarget;

        public OutOfBoundsPhase()
        {
            Title = "\"Out of bounds\"";
            Description = "Survive the restricted area.";
            CompleteMessage = "Out of bounds complete.";
            Duration = TimeSpan.FromSeconds(35);
        }

        private ShipTopology CreateWallSide(int wallWave)
        {
            var topology = new ShipTopology(2, 4);
            for (var i = 0; i < 2; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    if (j == 3)
                    {
                        topology.SetSection(new Point(i, j), new Section(new RocketModule(), ConnectionLayouts.FullyConnected, 1));
                    }
                    else
                    {
                        topology.SetSection(new Point(i, j), new Section(new EmptyModule(), ConnectionLayouts.FullyConnected, 4));
                    }
                }
            }

            topology.SetSection(new Point(0, 0), new Section(new BlasterModule(), ConnectionLayouts.FullyConnected, 4));
            topology.SetSection(new Point(1, 0), new Section(new BlasterModule(), ConnectionLayouts.FullyConnected, 4));

            return topology;
        }

        public override void Start()
        {

            Ship = Entity.EntityManager.GetAll<FlightShip>().First();
            _camera = Entity.EntityManager.GetAll<FlightCameraControl>().First();
            _oldCameraScale = _camera.CameraScale;
            _camera.CameraScale = 60f;

            var targetEnt = Entity.Create();
            _phaseDistanceTarget = targetEnt.Add(new PhaseDistanceTarget(this, Entity.EntityManager.GetAll<FlightShip>().First(), 150f));
            targetEnt.Add(new PhaseDistanceTargetRenderable());

            _asteroidSpawner = Entity.Create().Add(new AsteroidSpawner(Ship, 5, AsteroidDistributions.StandardAsteroidDistribution, _phaseDistanceTarget));

        }


        public override void Tick(TickContext context)
        {
            _phaseSpawnTimer.Tick(context.DeltaTime);
            if (_phaseSpawnTimer.Completed)
            {
                return;
            }

            _spawnTimer.Tick(context.DeltaTime);
            if (_spawnTimer.Completed)
            {
                SpawnWaveSideways(new Vector3(-30, 0, ((float)Random.NextDouble() * 50f) - 25f));
                _spawnTimer.Restart();
            }
        }

        //private void SpawnWave(Vector3 playerOffset, float spawnDistace, int wallWave, float rotation)
        //{
        //    var shipTransform = Ship.Entity.Get<Transform>();
        //    var enemy = Entity.EntityManager.Create();
        //    enemy.Add(new Transform
        //    {
        //        Location = shipTransform.Location + playerOffset * spawnDistace,
        //    });
        //    var ship = enemy.AddShipBasics(CreateWall(wallWave));
        //    ship.Rotation = rotation;
        //    var brain = enemy.Add(new CrusherBrain(Ship, playerOffset)
        //    {
        //        Active = true
        //    });
        //    _enemyBrains.Add(brain);


        //    _enemies.Add(enemy);
        //}

        private void SpawnWaveSideways(Vector3 playerOffset)
        {
            var shipTransform = Ship.Entity.Get<Transform>();
            var enemy = Entity.EntityManager.Create();
            enemy.Add(new Transform
            {
                Location = shipTransform.Location + playerOffset,
            });
            var ship = enemy.AddShipBasics(CreateWallSide(0));
            ship.Rotation = 3 * (float)Math.PI / 2;
            enemy.Add(new SwarmShipBrain(Ship)
            {
                Active = true
            });

            _enemies.Add(enemy);
        }

        public override void Stop()
        {
            //_target.Entity.Delete();
            _camera.CameraScale = _oldCameraScale;
            foreach (var drone in _enemies)
            {
                drone.Delete();
            }
            _asteroidSpawner.Stop();
            _asteroidSpawner.Entity.Delete();
            _phaseDistanceTarget.Entity.Delete();
        }
    }
}
