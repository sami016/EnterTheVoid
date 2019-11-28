using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
using Forge.Core.Interfaces;
using Forge.Core.Rendering;
using Forge.Core.Rendering.Cameras;
using Forge.Core.Utilities;
using EnterTheVoid.Flight;
using EnterTheVoid.Obstacles;
using EnterTheVoid.Ships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.Projectiles
{
    public abstract class ProjectileBase : Component, IInit, ITick, IRenderable, IShipCollider, IProjectileCollider
    {
        private static Random Random = new Random();
        private static Model _projectileModel;

        public uint RenderOrder { get; set; } = 10;

        public bool AutoRender { get; } = true;
        public bool HitProjectiles { get; set; } = false;
        public float Radius { get; set; } = 0.1f;

        [Inject] public ContentManager Content { get; set; }
        [Inject] public Transform Transform { get; set; }
        [Inject] public CameraManager CameraManager { get; set; }
        [Inject] public FlightSpaces FlightSpaces { get; set; }

        public Vector3 Velocity { get; set; } = Vector3.Zero;
        public Guid ShipGuid { get; }

        private readonly CompletionTimer _despawnTimer;

        public ProjectileBase(Guid shipGuid, Vector3 parentVelocity, Vector3 direction, float speed, TimeSpan? despawnDuration = null)
        {
            ShipGuid = shipGuid;
            direction.Normalize();
            Velocity = direction * speed + parentVelocity;
            _despawnTimer = new CompletionTimer(despawnDuration ?? TimeSpan.FromSeconds(10));
        }

        public virtual void Initialise()
        {
            if (_projectileModel == null)
            {
                _projectileModel = Content.Load<Model>("Models/bullet");
                //_projectileModel.EnableDefaultLighting();
                _projectileModel.SetDiffuseColour(Color.White);
            }
            FlightSpaces?.ProjectileSpace?.Add(Entity);
        }

        public void Tick(TickContext context)
        {
            _despawnTimer.Tick(context.DeltaTime);
            if (_despawnTimer.Completed)
            {
                Entity.Delete();
                return;
            }

            var location = Transform.Location;
            // Get every obstacle within 5 units of the node.
            var obstacle = FlightSpaces.ObstacleSpace.GetNearby(location, 5f);
            foreach (var entity in obstacle)
            {
                var pos = entity.Get<Transform>().Location;
                var distance = (location - pos).Length();
                var radius = entity.Get<IObstacle>()?.Radius ?? 1f;
                if (distance < Radius + radius)
                {
                    if (entity.Has<IProjectileCollider>())
                    {
                        entity.Get<IProjectileCollider>().OnHit(Entity, this);
                        EntityDidHit(entity);
                    }
                }
            }
            foreach (var entity in FlightSpaces.ShipSpace.GetNearby(location, 5f))
            {
                var node = entity.Get<FlightNode>();
                if (!node.Active || node.ShipGuid == ShipGuid)
                {
                    continue;
                }
                var pos = node.GlobalLocation;
                var distance = (location - pos).Length();
                if (distance < 1 + Radius)
                {
                    if (entity.Has<IProjectileCollider>())
                    {
                        entity.Get<IProjectileCollider>().OnHit(Entity, this);
                        EntityDidHit(entity);
                    }
                }
            }
            if (HitProjectiles)
            {
                foreach (var entity in FlightSpaces.ProjectileSpace.GetNearby(location, 5f))
                {
                    if (entity == this.Entity)
                    {
                        continue;
                    }
                    var pos = entity.Get<Transform>().Location;
                    var distance = (location - pos).Length();
                    var radius = entity.Get<ProjectileBase>()?.Radius ?? 1f;
                    if (distance < Radius + radius)
                    {
                        if (entity.Has<IProjectileCollider>())
                        {
                            entity.Get<IProjectileCollider>().OnHit(Entity, this);
                            EntityDidHit(entity);
                        }
                    }
                }
            }

            UpdatePosition(context);

        }

        protected virtual void UpdatePosition(TickContext context)
        {
            //var spin = _spin * context.DeltaTimeSeconds;
            Transform.Update(() =>
            {
                //Transform.Rotation *= Quaternion.CreateFromYawPitchRoll(spin.X, spin.Y, spin.Z);
                Transform.Location += Velocity * context.DeltaTimeSeconds;
            });
        }

        public virtual float GetDamage(Entity hitEnt, IComponent hitComponent)
        {
            return 10f;
        }

        public abstract void Render(RenderContext context);

        public override void Dispose()
        {
            FlightSpaces.ProjectileSpace.Remove(Entity);
        }

        public virtual void EntityDidHit(Entity entity)
        {
            Entity.Delete();
        }

        public virtual void OnHit(FlightNode node, FlightShip ship, Point gridLocation, Vector3 nodeLocation, Section section)
        {
        }

        public virtual void OnHit(Entity projectileEntity, ProjectileBase projectile)
        {
        }
    }
}
