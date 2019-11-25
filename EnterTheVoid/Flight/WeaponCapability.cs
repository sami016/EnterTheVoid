using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Interfaces;
using Forge.Core.Utilities;
using IntoTheVoid.Projectiles;
using IntoTheVoid.Ships.Modules;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntoTheVoid.Flight
{
    public class WeaponCapability : Component, ITick
    {
        private CompletionTimer _lightweightCooldown = new CompletionTimer(TimeSpan.FromSeconds(0.5f));
        private CompletionTimer _heavyweightCooldown = new CompletionTimer(TimeSpan.FromSeconds(10f));

        [Inject] FlightShip FlightShip { get; set; }
        [Inject] Transform Transform { get; set; }

        public WeaponCapability()
        {
            _lightweightCooldown.Complete();
            _heavyweightCooldown.Complete();
        }

        public void StandardFire()
        {
            if (_lightweightCooldown.Completed)
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
                        new LightBlasterProjectile(
                            FlightShip.ShipGuid,
                            FlightShip.Velocity,
                            Vector3.Transform(Vector3.Transform(Vector3.Forward, rotationQuat), shipTransform.Rotation),
                            false
                        )
                    );
                }

                _lightweightCooldown.Restart();
            }
        }

        public void HeavyFire()
        {
            if (_heavyweightCooldown.Completed)
            {
                var guns = FlightShip.Topology.AllSections
                    .Where(x => x.Module is BlasterModule);
                var shipTransform = Transform;
                foreach (var gun in guns)
                {
                    if (!FlightShip.TryTakeEnergy(1))
                    {
                        return;
                    }
                    var flightNode = FlightShip.GetNodeForSection(gun.GridLocation);
                    var rotation = (float)((-gun.Rotation - 2) * Math.PI / 3);
                    var rotationQuat = Quaternion.CreateFromYawPitchRoll(rotation, 0, 0);
                    //var nodeTransform = flightNode.Entity.Get<Transform>();
                    var location = flightNode.GlobalLocation;
                    var shell = Entity.EntityManager.Create();
                    shell.Add(new Transform
                    {
                        Location = location
                    });
                    shell.Add(
                        new HeavyBlasterProjectile(
                            FlightShip.ShipGuid,
                            FlightShip.Velocity,
                            Vector3.Transform(Vector3.Transform(Vector3.Forward, rotationQuat), shipTransform.Rotation),
                            false
                        )
                    );
                }

                _heavyweightCooldown.Restart();
            }
        }

        public void Tick(TickContext context)
        {
            _lightweightCooldown.Tick(context.DeltaTime);
            _heavyweightCooldown.Tick(context.DeltaTime);
        }
    }
}
