using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Interfaces;
using Forge.Core.Utilities;
using EnterTheVoid.Projectiles;
using EnterTheVoid.Ships.Modules;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnterTheVoid.Upgrades;

namespace EnterTheVoid.Flight
{
    public class WeaponCapability : Component, ITick
    {
        private static readonly Random Random = new Random();

        private CompletionTimer _lightweightCooldown = new CompletionTimer(TimeSpan.FromSeconds(0.5f));
        private CompletionTimer _heavyweightCooldown = new CompletionTimer(TimeSpan.FromSeconds(10f));
        private CompletionTimer _bubbleShieldCooldown = new CompletionTimer(TimeSpan.FromSeconds(10f));
        private CompletionTimer _blastRocketCooldown = new CompletionTimer(TimeSpan.FromSeconds(2f));
        private CompletionTimer _bombardCooldown = new CompletionTimer(TimeSpan.FromSeconds(10f));

        [Inject] FlightShip FlightShip { get; set; }
        [Inject] Transform Transform { get; set; }

        public WeaponCapability()
        {
            _lightweightCooldown.Complete();
            _heavyweightCooldown.Complete();
            _bubbleShieldCooldown.Complete();
            _blastRocketCooldown.Complete();
            _bombardCooldown.Complete();
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


        public void ShieldDeploy()
        {
            if (_bubbleShieldCooldown.Completed)
            {
                var guns = FlightShip.Topology.AllSections
                    .Where(x => x.Module is ForcefieldShieldModule);
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
                        new BubbleShieldProjectile(
                            FlightShip.ShipGuid,
                            FlightShip.GetNodeForSection(gun.GridLocation),
                            FlightShip.Velocity,
                            Vector3.Transform(Vector3.Transform(Vector3.Forward, rotationQuat), shipTransform.Rotation),
                            FlightShip.HasUpgrade<ShieldAmplification>(),
                            FlightShip.HasUpgrade<ShieldFortification>()
                        )
                    );
                }

                _bubbleShieldCooldown.Restart();
            }
        }


        public void BombardFire()
        {
            var numRockets = 4;
            var angleOffset = 0.3f;
            if (FlightShip.HasUpgrade<BombardOverload>())
            {
                numRockets = 8;
                angleOffset = 1.1f;
            }
            if (_bombardCooldown.Completed)
            {
                var guns = FlightShip.Topology.AllSections
                    .Where(x => x.Module is BombardModule);
                var shipTransform = Transform;
                foreach (var gun in guns)
                {
                    if (!FlightShip.TryTakeEnergy(1))
                    {
                        return;
                    }
                    for (var i = 0; i < numRockets; i++)
                    {
                        var flightNode = FlightShip.GetNodeForSection(gun.GridLocation);
                        var rotation = (float)((-gun.Rotation - 2) * Math.PI / 3 + angleOffset * Random.NextDouble() - angleOffset / 2f);
                        var rotationQuat = Quaternion.CreateFromYawPitchRoll(rotation, 0, 0);
                        var nodeTransform = flightNode.Entity.Get<Transform>();
                        var location = Vector3.Transform(Vector3.Zero, nodeTransform.WorldTransform * shipTransform.WorldTransform);
                        var shell = Entity.EntityManager.Create();
                        shell.Add(new Transform
                        {
                            Location = location + new Vector3((float)Random.NextDouble() * 0.3f, 0, (float)Random.NextDouble() * 0.3f)
                        });

                        var rotationMatrix = Matrix.CreateFromYawPitchRoll(rotation, 0, 0)
                            * Matrix.CreateFromQuaternion(shipTransform.Rotation);

                        shell.Add(
                            new RocketProjectile(
                                FlightShip.ShipGuid,
                                FlightShip.Velocity,
                                Vector3.Transform(Vector3.Transform(Vector3.Forward, rotationQuat), shipTransform.Rotation),
                                rotationMatrix
                            )
                        );
                    }
                }

                _bombardCooldown.Restart();
            }
        }


        public void RocketBlast()
        {
            if (!FlightShip.HasUpgrade<BlastRocketry>())
            {
                return;
            }
            if (_blastRocketCooldown.Completed)
            {
                var guns = FlightShip.Topology.AllSections
                    .Where(x => x.Module is RocketModule);
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

                    var rotationMatrix = Matrix.CreateFromYawPitchRoll(rotation, 0, 0)
                        * Matrix.CreateFromQuaternion(shipTransform.Rotation);

                    shell.Add(
                        new BlastRocketProjectile(
                            FlightShip.ShipGuid,
                            FlightShip.Velocity,
                            Vector3.Transform(Vector3.Transform(Vector3.Forward, rotationQuat), shipTransform.Rotation),
                            rotationMatrix
                        )
                    );
                }

                _blastRocketCooldown.Restart();
            }
        }

        public void Tick(TickContext context)
        {
            _lightweightCooldown.Tick(context.DeltaTime);
            _heavyweightCooldown.Tick(context.DeltaTime);
            _bubbleShieldCooldown.Tick(context.DeltaTime);
            _blastRocketCooldown.Tick(context.DeltaTime);
            _bombardCooldown.Tick(context.DeltaTime);
        }
    }
}
