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
    public class DroneStrikePhase : Phase
    {
        private IList<Entity> _droneEntities = new List<Entity>();

        FlightShip Ship { get; set; }

        private FlightCameraControl _camera;
        private float _oldCameraScale;
        private PhaseKillTarget _target;
        private readonly int _numDrones;

        //FlightCameraControl _camera;

        public DroneStrikePhase(int numDrones)
        {
            _numDrones = numDrones;
            Title = "Drone Zone";
            Description = "Combat warning. Fend off incoming attack drones. Survive 45 seconds.";
            CompleteMessage = "Combat completed.";
            // To prevent player getting stuck.
            Duration = TimeSpan.FromSeconds(60);
        }

        private ShipTopology CreateDrone()
        {
            var topology = new ShipTopology(2, 2);
            topology.SetSection(new Point(0, 0), new Section(new BlasterModule(), ConnectionLayouts.FullyConnected, 4));
            topology.SetSection(new Point(0, 1), new Section(new RocketModule(), ConnectionLayouts.FullyConnected, 1));
            topology.SetSection(new Point(1, 0), new Section(new BlasterModule(), ConnectionLayouts.FullyConnected, 4));
            return topology;
        }

        public override void Start()
        {
            Ship = Entity.EntityManager.GetAll<FlightShip>().First();
            _camera = Entity.EntityManager.GetAll<FlightCameraControl>().First();
            _oldCameraScale = _camera.CameraScale;
            _camera.CameraScale = 70f;
            if (_numDrones >= 1) SpawnDrone(new Vector3(0, 0, -5), 2, 10);
            if (_numDrones >= 2) SpawnDrone(new Vector3(0, 0, 10), 2, 20);
            if (_numDrones >= 3) SpawnDrone(new Vector3(5, 0, 10), 3, 30);
            if (_numDrones >= 4) SpawnDrone(new Vector3(5, 0, -5), 3, 40);
            if (_numDrones >= 5) SpawnDrone(new Vector3(10, 0, 5), 3, 50);
            if (_numDrones >= 5) SpawnDrone(new Vector3(0, 0, 0), 3, 60);


            var targetEnt = Entity.Create();
            _target = targetEnt.Add(new PhaseKillTarget(this, _droneEntities.Select(x => x.Get<FlightShip>()), 0));
            targetEnt.Add(new PhaseKillTargetRenderable());
        }

        private void SpawnDrone(Vector3 playerOffset, float rotationRadius, float spawnDistace)
        {
            var shipTransform = Ship.Entity.Get<Transform>();
            var drone = Entity.EntityManager.Create();
            drone.Add(new Transform
            {
                Rotation = Quaternion.CreateFromYawPitchRoll(0f, (float)(Random.NextDouble() * Math.PI * 2), 0f),
                Location = shipTransform.Location + playerOffset * spawnDistace,
            });
            drone.AddShipBasics(CreateDrone());
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
