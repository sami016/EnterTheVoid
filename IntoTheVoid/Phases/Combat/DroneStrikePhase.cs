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
        //FlightCameraControl _camera;

        public DroneStrikePhase()
        {
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
            Ship = Entity.EntityManager.GetAll<FlightShip>().First();

            //_drone1 = Entity.EntityManager.Create();
            //_drone1.Add(new Transform
            //{
            //    Location = shipTransform.Location + Vector3.Forward * 5,
            //});
            //_drone1.AddShipBasics(CreateDrone());
            //_drone1.Add(new ChaseDroneBrain(Ship, -5));


            //_drone2 = Entity.EntityManager.Create();
            //_drone2.Add(new Transform
            //{
            //    Location = shipTransform.Location + Vector3.Forward * 10,
            //});
            //_drone2.AddShipBasics(CreateDrone());
            //_drone2.Add(new ChaseDroneBrain(Ship, 10));

            SpawnDrone(new Vector3(0, 0, -5), 2);
            SpawnDrone(new Vector3(0, 0, 10), 2);
        }

        private void SpawnDrone(Vector3 playerOffset, float rotationRadius)
        {
            var shipTransform = Ship.Entity.Get<Transform>();
            var drone = Entity.EntityManager.Create();
            drone.Add(new Transform
            {
                Rotation = Quaternion.CreateFromYawPitchRoll(0f, (float)(Random.NextDouble() * Math.PI * 2), 0f),
                Location = shipTransform.Location + playerOffset * 10,
            });
            drone.AddShipBasics(CreateDrone());
            drone.Add(new ChaseDroneBrain(Ship, playerOffset, rotationRadius));
            _droneEntities.Add(drone);
        }
        public override void Stop()
        {
            //_drone1.Delete();
            //_drone2.Delete();
        }
    }
}
