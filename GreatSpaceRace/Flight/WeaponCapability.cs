using Forge.Core.Components;
using GreatSpaceRace.Projectiles;
using GreatSpaceRace.Ships.Modules;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreatSpaceRace.Flight
{
    public class WeaponCapability : Component
    {
        [Inject] FlightShip FlightShip { get; set; }
        [Inject] Transform Transform { get; set; }

        public void StandardFire()
        {
            var guns = FlightShip.Topology.AllSections
                .Where(x => x.Module is BlasterModule);
            var shipTransform = Transform;
            foreach (var gun in guns)
            {
                var flightNode = FlightShip.GetNodeForSection(gun.GridLocation);
                var rotation = (float)((-gun.Rotation - 2) * Math.PI / 3);
                var rotationQuat = Quaternion.CreateFromYawPitchRoll(rotation, 0, 0);
                var nodeTransform = flightNode.Entity.Get<Transform>();
                var location = Vector3.Transform(Vector3.Zero, nodeTransform.WorldTransform * shipTransform.WorldTransform);
                var shell = Entity.EntityManager.Create();
                shell.Add(new Transform
                {
                    Location = location
                });
                shell.Add(
                    new Projectile(
                        FlightShip.ShipGuid,
                        Vector3.Transform(Vector3.Transform(Vector3.Forward, rotationQuat), shipTransform.Rotation), 
                        20f
                    )
                );
            }
        }

        public void HeavyFire()
        {

        }
    }
}
