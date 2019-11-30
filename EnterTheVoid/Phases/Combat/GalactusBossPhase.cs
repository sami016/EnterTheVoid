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
using EnterTheVoid.Upgrades;
using EnterTheVoid.Phases.Asteroids;

namespace EnterTheVoid.Phases.Combat
{
    public class GalactusBossPhase : Phase
    {
        private IList<Entity> _enemies = new List<Entity>();

        FlightShip Ship { get; set; }

        private FlightCameraControl _camera;
        private float _oldCameraScale;
        private PhaseKillTarget _target;
        private int _wave = 0;
        private FlightShip _ship;
        private AsteroidSpawner _asteroidSpawner;

        //FlightCameraControl _camera;

        public GalactusBossPhase()
        {
            Title = "\"Galactus\"";
            Description = "Destroy enemy warship.";
            CompleteMessage = "Galactus is no more.";
            Duration = null;
        }

        private ShipTopology CreateWall(int wallWave)
        {
            var topology = new ShipTopology(10, 10);
            for (var i=0; i<10; i++)
            {
                for (var j=0; j<10; j++)
                {
                    if (j == 9)
                    {
                        topology.SetSection(new Point(i, j), new Section(new RocketModule(), ConnectionLayouts.FullyConnected, i == 0 ? 0 : i == 9 ? 2 : 1));
                    }
                    else
                    {
                        topology.SetSection(new Point(i, j), new Section(new ArmourModule(), ConnectionLayouts.FullyConnected, 4));
                    }
                }
            }


            topology.SetSection(new Point(0, 0), new Section(new ShieldBubbleGeneratorModule(), ConnectionLayouts.FullyConnected, 2));
            topology.SetSection(new Point(4, 4), new Section(new ResearchCenterModule(), ConnectionLayouts.FullyConnected, 2));
            topology.SetSection(new Point(3, 4), new Section(new ResearchCenterModule(), ConnectionLayouts.FullyConnected, 2));
            topology.SetSection(new Point(4, 5), new Section(new EnergyModule(), ConnectionLayouts.FullyConnected, 2));
            topology.SetSection(new Point(6, 7), new Section(new EnergyModule(), ConnectionLayouts.FullyConnected, 2));
            topology.SetSection(new Point(8, 5), new Section(new EnergyModule(), ConnectionLayouts.FullyConnected, 2));
            topology.SetSection(new Point(3, 4), new Section(new EnergyModule(), ConnectionLayouts.FullyConnected, 2));
            topology.SetSection(new Point(2, 2), new Section(new EnergyModule(), ConnectionLayouts.FullyConnected, 2));
            topology.SetSection(new Point(2, 3), new Section(new ShieldBubbleGeneratorModule(), ConnectionLayouts.FullyConnected, 2));
            topology.SetSection(new Point(2, 5), new Section(new EnergyModule(), ConnectionLayouts.FullyConnected, 2));
            topology.SetSection(new Point(2, 6), new Section(new ShieldBubbleGeneratorModule(), ConnectionLayouts.FullyConnected, 2));
            topology.SetSection(new Point(2, 8), new Section(new EnergyModule(), ConnectionLayouts.FullyConnected, 2));
            topology.SetSection(new Point(8, 2), new Section(new EnergyModule(), ConnectionLayouts.FullyConnected, 2));
            topology.SetSection(new Point(8, 5), new Section(new EnergyModule(), ConnectionLayouts.FullyConnected, 2));
            topology.SetSection(new Point(8, 8), new Section(new EnergyModule(), ConnectionLayouts.FullyConnected, 2));
            topology.SetSection(new Point(4, 2), new Section(new EnergyModule(), ConnectionLayouts.FullyConnected, 2));
            topology.SetSection(new Point(4, 3), new Section(new ShieldBubbleGeneratorModule(), ConnectionLayouts.FullyConnected, 2));
            topology.SetSection(new Point(4, 5), new Section(new EnergyModule(), ConnectionLayouts.FullyConnected, 2));
            topology.SetSection(new Point(4, 6), new Section(new ShieldBubbleGeneratorModule(), ConnectionLayouts.FullyConnected, 2));
            topology.SetSection(new Point(4, 8), new Section(new EnergyModule(), ConnectionLayouts.FullyConnected, 2));
            

            topology.SetSection(new Point(1, 0), new Section(new BlasterModule(), ConnectionLayouts.FullyConnected, 4));
            topology.SetSection(new Point(2, 0), new Section(new BombardModule(), ConnectionLayouts.FullyConnected, 3));
            topology.SetSection(new Point(3, 0), new Section(new BombardModule(), ConnectionLayouts.FullyConnected, 4));
            topology.SetSection(new Point(4, 0), new Section(new BombardModule(), ConnectionLayouts.FullyConnected, 5));
            topology.SetSection(new Point(5, 0), new Section(new ShieldBubbleGeneratorModule(), ConnectionLayouts.FullyConnected, 2));
            topology.SetSection(new Point(6, 0), new Section(new BombardModule(), ConnectionLayouts.FullyConnected, 3));
            topology.SetSection(new Point(7, 0), new Section(new BombardModule(), ConnectionLayouts.FullyConnected, 4));
            topology.SetSection(new Point(8, 0), new Section(new BombardModule(), ConnectionLayouts.FullyConnected, 5));
            topology.SetSection(new Point(9, 0), new Section(new BlasterModule(), ConnectionLayouts.FullyConnected, 4));

            topology.SetSection(new Point(0, 1), new Section(new BombardModule(), ConnectionLayouts.FullyConnected, 2));
            topology.SetSection(new Point(0, 2), new Section(new BombardModule(), ConnectionLayouts.FullyConnected, 3));
            topology.SetSection(new Point(0, 3), new Section(new BombardModule(), ConnectionLayouts.FullyConnected, 4));
            topology.SetSection(new Point(0, 4), new Section(new ShieldBubbleGeneratorModule(), ConnectionLayouts.FullyConnected, 4));
            topology.SetSection(new Point(0, 5), new Section(new BombardModule(), ConnectionLayouts.FullyConnected, 2));
            topology.SetSection(new Point(0, 6), new Section(new BombardModule(), ConnectionLayouts.FullyConnected, 3));
            topology.SetSection(new Point(0, 7), new Section(new BombardModule(), ConnectionLayouts.FullyConnected, 4));
            topology.SetSection(new Point(0, 8), new Section(new BlasterModule(), ConnectionLayouts.FullyConnected, 3));
            topology.SetSection(new Point(0, 9), new Section(new BlasterModule(), ConnectionLayouts.FullyConnected, 3));

            topology.SetSection(new Point(9, 1), new Section(new BlasterModule(), ConnectionLayouts.FullyConnected, 5));
            topology.SetSection(new Point(9, 2), new Section(new BombardModule(), ConnectionLayouts.FullyConnected, 4));
            topology.SetSection(new Point(9, 3), new Section(new BombardModule(), ConnectionLayouts.FullyConnected, 5));
            topology.SetSection(new Point(9, 4), new Section(new BombardModule(), ConnectionLayouts.FullyConnected, 0));
            topology.SetSection(new Point(9, 5), new Section(new BlasterModule(), ConnectionLayouts.FullyConnected, 5));
            topology.SetSection(new Point(9, 6), new Section(new BombardModule(), ConnectionLayouts.FullyConnected, 4));
            topology.SetSection(new Point(9, 7), new Section(new BombardModule(), ConnectionLayouts.FullyConnected, 5));
            topology.SetSection(new Point(9, 8), new Section(new BombardModule(), ConnectionLayouts.FullyConnected, 0));
            topology.SetSection(new Point(9, 9), new Section(new BlasterModule(), ConnectionLayouts.FullyConnected, 5));
            topology.ApplyUpgrade(new BlastRocketry());
            // topology.ApplyUpgrade(new BombardOverload());
            topology.ApplyUpgrade(new ShieldAmplification());
            topology.ApplyUpgrade(new ShieldFortification());

            return topology;
        }

        public override void Start()
        {

            Ship = Entity.EntityManager.GetAll<FlightShip>().First();
            _camera = Entity.EntityManager.GetAll<FlightCameraControl>().First();
            _oldCameraScale = _camera.CameraScale;
            _camera.CameraScale = 60f;
            SpawnWave(new Vector3(0, 0, -20), 2, 0, true);

            var targetEnt = Entity.Create();
            _target = targetEnt.Add(new PhaseKillTarget(this, _enemies.Select(x => x.Get<FlightShip>()), 0));
            targetEnt.Add(new PhaseKillTargetRenderable());

            _asteroidSpawner = Entity.Create().Add(new AsteroidSpawner(Ship, 5, AsteroidDistributions.StandardAsteroidDistribution));
            MusicManager.Start("Boss2");
        }

        public override void Tick(TickContext context)
        {
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
            var brain = enemy.Add(new GalactusBrain(Ship, playerOffset, 4f));

            _enemies.Add(enemy);
            _ship = ship;
        }

        public override void Stop()
        {
            _target.Entity.Delete();
            _camera.CameraScale = _oldCameraScale;
            foreach (var drone in _enemies)
            {
                drone.Delete();
            }
            _ship.Entity.Delete();
            _asteroidSpawner.Stop();
            _asteroidSpawner.Entity.Delete();
        }
    }
}
