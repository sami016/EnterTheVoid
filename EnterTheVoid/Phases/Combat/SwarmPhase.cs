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

namespace EnterTheVoid.Phases.Combat
{
    public class SwarmPhase : Phase
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

        public SwarmPhase()
        {
            Title = "\"Swarm\"";
            Description = "Survive the swarm.";
            CompleteMessage = "Swarm complete.";
            Duration = TimeSpan.FromSeconds(45);
        }

        //private ShipTopology CreateWall(int wallWave)
        //{
        //    var topology = new ShipTopology(30, 2);
        //    for (var i = 0; i < 30; i++)
        //    {
        //        for (var j = 0; j < 2; j++)
        //        {
        //            if (j == 1)
        //            {
        //                topology.SetSection(new Point(i, j), new Section(new RocketModule(), ConnectionLayouts.FullyConnected, i == 0 ? 0 : i == 13 ? 2 : 1));
        //            }
        //            else if (j == 0)
        //            {
        //                if (i % 4 == 2 || i % 4 == 0)
        //                {
        //                    topology.SetSection(new Point(i, j), new Section(new ForcefieldShieldModule(), ConnectionLayouts.FullyConnected, 0));
        //                }
        //                if (i % 4 == 1)
        //                {
        //                    topology.SetSection(new Point(i, j), new Section(new BlasterModule(), ConnectionLayouts.FullyConnected, 4));
        //                }
        //            }
        //            else
        //            {
        //                topology.SetSection(new Point(i, j), new Section(new EmptyModule(), ConnectionLayouts.FullyConnected, 4));
        //            }
        //        }
        //    }

        //    return topology;
        //}


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
            //SpawnWave(new Vector3(28, 0, -13), 10, 0, (float)Math.PI);
            //SpawnWave(new Vector3(-22, 0, 15), 30, 1, 0f);

            //var targetEnt = Entity.Create();
            //_target = targetEnt.Add(new PhaseKillTarget(this, _enemies.Select(x => x.Get<FlightShip>()), 0));
            //targetEnt.Add(new PhaseKillTargetRenderable());
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
                SpawnWaveSideways(new Vector3(-30, 0, ((float)Random.NextDouble() * 10f) - 10f));
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
        }
    }
}
