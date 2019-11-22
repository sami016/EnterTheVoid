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
        private Entity _drone1;
        private Entity _drone2;
        private Entity _drone3;

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
            var shipTransform = Ship.Entity.Get<Transform>();

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


            _drone3 = Entity.EntityManager.Create();
            _drone3.Add(new Transform
            {
                Rotation = Quaternion.CreateFromYawPitchRoll(0f, (float)Math.PI, 0f),
                Location = shipTransform.Location + Vector3.Forward * 100,
            });
            _drone3.AddShipBasics(CreateDrone());
            _drone3.Add(new ChaseDroneBrain(Ship, 10, -10));
        }

        public override void Stop()
        {
            //_drone1.Delete();
            //_drone2.Delete();
        }
    }
}
