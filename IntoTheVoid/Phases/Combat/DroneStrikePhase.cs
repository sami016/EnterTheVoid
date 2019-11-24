using Forge.Core.Components;
using Forge.Core.Engine;
using IntoTheVoid.AI;
using IntoTheVoid.Flight;
using IntoTheVoid.Ships;
using IntoTheVoid.Ships.Connections;
using IntoTheVoid.Ships.Modules;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntoTheVoid.Phases.Combat
{
    public class DroneStrikePhase : Phase
    {
        private IList<Entity> _droneEntities = new List<Entity>();

        FlightShip Ship { get; set; }

        private FlightCameraControl _camera;
        private float _oldCameraScale;
        private readonly int _numDrones;

        //FlightCameraControl _camera;

        public DroneStrikePhase(int numDrones)
        {
            _numDrones = numDrones;
            Title = "Drone Zone";
            Description = "Combat warning. Fend off incoming attack drones.";
            CompleteMessage = "Combat completed.";
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
            if (_numDrones >= 1) SpawnDrone(new Vector3(0, 0, -5), 2, 3);
            if (_numDrones >= 2) SpawnDrone(new Vector3(0, 0, 10), 2, 20);
            if (_numDrones >= 3) SpawnDrone(new Vector3(5, 0, 10), 3, 40);
            if (_numDrones >= 4) SpawnDrone(new Vector3(5, 0, -5), 3, 40);
            if (_numDrones >= 5) SpawnDrone(new Vector3(0, 0, 10), 3, 40);
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
        }
    }
}
