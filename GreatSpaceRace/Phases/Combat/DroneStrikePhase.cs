using Forge.Core.Components;
using GreatSpaceRace.AI;
using GreatSpaceRace.Flight;
using GreatSpaceRace.Ships;
using GreatSpaceRace.Ships.Connections;
using GreatSpaceRace.Ships.Modules;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreatSpaceRace.Phases.Combat
{
    public class DroneStrikePhase : Phase
    {
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

            var drone = Entity.EntityManager.Create();
            drone.Add(new Transform { 
                Location = shipTransform.Location + Vector3.Forward * 5,
            });
            drone.Add(new FlightShip(CreateDrone()));
            drone.Add(new WeaponCapability());
            drone.Add(new RocketCapability());
            drone.Add(new EnemyHarness(Ship)
            {
                FixZ = 15f,
                FixX = -5f
            });
            drone.Add(new ChaseDroneBrain());
        }

        public override void Stop()
        {
        }
    }
}
