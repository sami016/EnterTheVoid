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
    public class BanditPhase : Phase
    {
        private IList<Entity> _droneEntities = new List<Entity>();

        FlightShip Ship { get; set; }

        private FlightCameraControl _camera;
        private float _oldCameraScale;
        private PhaseKillTarget _target;

        //FlightCameraControl _camera;

        public BanditPhase(int difficulty)
        {
            Title = "Bandit Vessel Inbound";
            Description = "Combat warning. Fend off incoming bandits. Survive 45 seconds.";
            CompleteMessage = "Combat completed.";
            // To prevent player getting stuck.
            Duration = TimeSpan.FromSeconds(120);
        }

        private ShipTopology CreateBandit()
        {
            var topology = new ShipTopology(4, 4);
            for (var i=0; i<4; i++)
            {
                for (var j=0; j<4; j++)
                {
                    topology.SetSection(new Point(i, j), new Section(new EmptyModule(), ConnectionLayouts.FullyConnected, 4));
                }
            }
            topology.SetSection(new Point(0, 0), new Section(new BlasterModule(), ConnectionLayouts.FullyConnected, 3));
            topology.SetSection(new Point(1, 0), new Section(new BlasterModule(), ConnectionLayouts.FullyConnected, 4));
            topology.SetSection(new Point(2, 0), new Section(new BlasterModule(), ConnectionLayouts.FullyConnected, 5));
            topology.SetSection(new Point(3, 0), new Section(new BlasterModule(), ConnectionLayouts.FullyConnected, 6));
            topology.SetSection(new Point(0, 3), new Section(new RocketModule(), ConnectionLayouts.FullyConnected, 0));
            topology.SetSection(new Point(1, 3), new Section(new RocketModule(), ConnectionLayouts.FullyConnected, 1));
            topology.SetSection(new Point(2, 3), new Section(new RocketModule(), ConnectionLayouts.FullyConnected, 1));
            topology.SetSection(new Point(3, 3), new Section(new RocketModule(), ConnectionLayouts.FullyConnected, 2));

            topology.SetSection(new Point(1, 2), new Section(new EnergyModule(), ConnectionLayouts.FullyConnected, 4));
            topology.SetSection(new Point(2, 2), new Section(new EnergyModule(), ConnectionLayouts.FullyConnected, 2));
            return topology;
        }

        public override void Start()
        {
            Ship = Entity.EntityManager.GetAll<FlightShip>().First();
            _camera = Entity.EntityManager.GetAll<FlightCameraControl>().First();
            _oldCameraScale = _camera.CameraScale;
            _camera.CameraScale = 60f;
            SpawnEnemy(new Vector3(0, 0, -5), 2, 3);

            var targetEnt = Entity.Create();
            _target = targetEnt.Add(new PhaseKillTarget(this, _droneEntities.Select(x => x.Get<FlightShip>()), 0));
            targetEnt.Add(new PhaseKillTargetRenderable());
        }

        private void SpawnEnemy(Vector3 playerOffset, float rotationRadius, float spawnDistace)
        {
            var shipTransform = Ship.Entity.Get<Transform>();
            var drone = Entity.EntityManager.Create();
            drone.Add(new Transform
            {
                Rotation = Quaternion.CreateFromYawPitchRoll(0f, (float)(Random.NextDouble() * Math.PI * 2), 0f),
                Location = shipTransform.Location + playerOffset * spawnDistace,
            });
            drone.AddShipBasics(CreateBandit());
            drone.Add(new ChaseDroneBrain(Ship, playerOffset, rotationRadius));
            _droneEntities.Add(drone);
        }

        public override void Stop()
        {
            _camera.CameraScale = _oldCameraScale;
            foreach (var drone in _droneEntities)
            {
                drone.Delete();
            }
            _target.Entity.Delete();
        }
    }
}
