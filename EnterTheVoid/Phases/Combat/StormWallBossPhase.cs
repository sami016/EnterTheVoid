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

namespace EnterTheVoid.Phases.Combat
{
    public class StormWallBossPhase : Phase
    {
        private IList<Entity> _enemies = new List<Entity>();

        FlightShip Ship { get; set; }

        private FlightCameraControl _camera;
        private float _oldCameraScale;

        //FlightCameraControl _camera;

        public StormWallBossPhase()
        {
            Title = "\"Storm Wall\"";
            Description = "Escape the Storm Wall.";
            CompleteMessage = "Escape complete.";
        }

        private ShipTopology CreateWall()
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
                        topology.SetSection(new Point(i, j), new Section(new EmptyModule(), ConnectionLayouts.FullyConnected, 4));
                    }
                }
            }

            topology.SetSection(new Point(1, 0), new Section(new BlasterModule(), ConnectionLayouts.FullyConnected, 4));
            topology.SetSection(new Point(3, 0), new Section(new BombardModule(), ConnectionLayouts.FullyConnected, 4));
            topology.SetSection(new Point(5, 0), new Section(new BlasterModule(), ConnectionLayouts.FullyConnected, 4));
            topology.SetSection(new Point(7, 0), new Section(new BombardModule(), ConnectionLayouts.FullyConnected, 4));
            topology.SetSection(new Point(9, 0), new Section(new BlasterModule(), ConnectionLayouts.FullyConnected, 4));
            topology.SetSection(new Point(11, 0), new Section(new BombardModule(), ConnectionLayouts.FullyConnected, 4));
            topology.SetSection(new Point(13, 0), new Section(new BlasterModule(), ConnectionLayouts.FullyConnected, 4));

            //topology.SetSection(new Point(0, 0), new Section(new BlasterModule(), ConnectionLayouts.FullyConnected, 3));
            //topology.SetSection(new Point(1, 0), new Section(new BlasterModule(), ConnectionLayouts.FullyConnected, 4));
            //topology.SetSection(new Point(2, 0), new Section(new BlasterModule(), ConnectionLayouts.FullyConnected, 5));
            //topology.SetSection(new Point(3, 0), new Section(new BlasterModule(), ConnectionLayouts.FullyConnected, 6));
            //topology.SetSection(new Point(0, 3), new Section(new RocketModule(), ConnectionLayouts.FullyConnected, 0));
            //topology.SetSection(new Point(1, 3), new Section(new RocketModule(), ConnectionLayouts.FullyConnected, 1));
            //topology.SetSection(new Point(2, 3), new Section(new RocketModule(), ConnectionLayouts.FullyConnected, 1));
            //topology.SetSection(new Point(3, 3), new Section(new RocketModule(), ConnectionLayouts.FullyConnected, 2));

            //topology.SetSection(new Point(1, 2), new Section(new EnergyModule(), ConnectionLayouts.FullyConnected, 4));
            //topology.SetSection(new Point(2, 2), new Section(new EnergyModule(), ConnectionLayouts.FullyConnected, 2));
            return topology;
        }

        public override void Start()
        {
            Ship = Entity.EntityManager.GetAll<FlightShip>().First();
            _camera = Entity.EntityManager.GetAll<FlightCameraControl>().First();
            _oldCameraScale = _camera.CameraScale;
            _camera.CameraScale = 60f;
            SpawnWave(new Vector3(15, 0, -8), 10);
        }

        private void SpawnWave(Vector3 playerOffset, float spawnDistace)
        {
            var shipTransform = Ship.Entity.Get<Transform>();
            var enemy = Entity.EntityManager.Create();
            enemy.Add(new Transform
            {
                Location = shipTransform.Location + playerOffset * spawnDistace,
            });
            var ship = enemy.AddShipBasics(CreateWall());
            ship.Rotation = (float)Math.PI;
            enemy.Add(new StormWallBrain(Ship, playerOffset));

            _enemies.Add(enemy);
        }

        public override void Stop()
        {
            _camera.CameraScale = _oldCameraScale;
            foreach (var drone in _enemies)
            {
                drone.Delete();
            }
        }
    }
}
