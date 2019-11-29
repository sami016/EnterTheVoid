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

namespace EnterTheVoid.Phases.Combat
{
    public class StormWallBossPhase : Phase
    {
        private IList<Entity> _enemies = new List<Entity>();
        private IList<StormWallBrain> _enemyBrains = new List<StormWallBrain>();

        FlightShip Ship { get; set; }

        private FlightCameraControl _camera;
        private float _oldCameraScale;
        private PhaseKillTarget _target;
        private int _wave = 0;
        private readonly bool _armoured;

        //FlightCameraControl _camera;

        public StormWallBossPhase(bool armoured)
        {
            Title = "\"Storm Wall\"";
            Description = "Defeat the Storm Wall ships.";
            CompleteMessage = "Storm wall defeated.";
            Duration = null;
            _armoured = armoured;
        }

        private ShipTopology CreateWall(int wallWave)
        {
            var topology = new ShipTopology(14, 3);
            for (var i=0; i<14; i++)
            {
                for (var j=0; j<3; j++)
                {
                    if (j == 2)
                    {
                        topology.SetSection(new Point(i, j), new Section(new RocketModule(), ConnectionLayouts.FullyConnected, i == 0 ? 0 : i == 13 ? 2 : 1));
                    }
                    else if (j == 1 && i > 0 && i < 13)
                    {
                        topology.SetSection(new Point(i, j), new Section(new EnergyModule(), ConnectionLayouts.FullyConnected, i == 0 ? 0 : i == 3 ? 2 : 1));
                    }
                    else
                    {
                        topology.SetSection(new Point(i, j), new Section(_armoured ? new ArmourModule() as Module : new EmptyModule(), ConnectionLayouts.FullyConnected, 4));
                    }
                }
            }

            if (wallWave == 1)
            {
                topology.SetSection(new Point(6, 1), new Section(new EmptyModule(), ConnectionLayouts.FullyConnected, 0));
            }
            if (wallWave == 2)
            {
                topology.SetSection(new Point(4, 1), new Section(new EmptyModule(), ConnectionLayouts.FullyConnected, 0));
                topology.SetSection(new Point(8, 1), new Section(new EmptyModule(), ConnectionLayouts.FullyConnected, 0));
            }

            topology.SetSection(new Point(1, 0), new Section(new BlasterModule(), ConnectionLayouts.FullyConnected, 4));
            topology.SetSection(new Point(3, 0), new Section(new BombardModule(), ConnectionLayouts.FullyConnected, 4));
            topology.SetSection(new Point(5, 0), new Section(new BlasterModule(), ConnectionLayouts.FullyConnected, 4));
            topology.SetSection(new Point(7, 0), new Section(new BombardModule(), ConnectionLayouts.FullyConnected, 4));
            topology.SetSection(new Point(9, 0), new Section(new BlasterModule(), ConnectionLayouts.FullyConnected, 4));
            topology.SetSection(new Point(11, 0), new Section(new BombardModule(), ConnectionLayouts.FullyConnected, 4));
            topology.SetSection(new Point(13, 0), new Section(new BlasterModule(), ConnectionLayouts.FullyConnected, 4));
            return topology;
        }

        public override void Start()
        {

            Ship = Entity.EntityManager.GetAll<FlightShip>().First();
            _camera = Entity.EntityManager.GetAll<FlightCameraControl>().First();
            _oldCameraScale = _camera.CameraScale;
            _camera.CameraScale = 60f;
            SpawnWave(new Vector3(15, 0, -8), 10, 0, true);
            SpawnWave(new Vector3(15, 0, -8), 30, 1);
            SpawnWave(new Vector3(15, 0, -8), 50, 2);

            var targetEnt = Entity.Create();
            _target = targetEnt.Add(new PhaseKillTarget(this, _enemies.Select(x => x.Get<FlightShip>()), 0));
            targetEnt.Add(new PhaseKillTargetRenderable());
        }

        public override void Tick(TickContext context)
        {
            if (_target.Remaining == 2 && _wave == 0)
            {
                _wave = 1;
                _enemyBrains[1].Active = true;
            }
            if (_target.Remaining == 1 && _wave == 1)
            {
                _wave = 2;
                _enemyBrains[2].Active = true;
            }
        }

        private void SpawnWave(Vector3 playerOffset, float spawnDistace, int wallWave, bool startActive = false)
        {
            var shipTransform = Ship.Entity.Get<Transform>();
            var enemy = Entity.EntityManager.Create();
            enemy.Add(new Transform
            {
                Location = shipTransform.Location + playerOffset * spawnDistace,
            });
            var ship = enemy.AddShipBasics(CreateWall(wallWave));
            ship.Rotation = (float)Math.PI;
            var brain = enemy.Add(new StormWallBrain(Ship, playerOffset)
            {
                Active = startActive
            });
            _enemyBrains.Add(brain);
            

            _enemies.Add(enemy);
        }

        public override void Stop()
        {
            _target.Entity.Delete();
            _camera.CameraScale = _oldCameraScale;
            foreach (var drone in _enemies)
            {
                drone.Delete();
            }
        }
    }
}
