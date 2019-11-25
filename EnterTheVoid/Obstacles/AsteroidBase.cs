using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
using Forge.Core.Interfaces;
using Forge.Core.Rendering;
using Forge.Core.Rendering.Cameras;
using Forge.Core.Space.Bodies;
using Forge.Core.Space.Shapes;
using Forge.Core.Utilities;
using EnterTheVoid.Flight;
using EnterTheVoid.Obstacles;
using EnterTheVoid.Projectiles;
using EnterTheVoid.Ships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.Obstacles
{
    public abstract class AsteroidBase : Component, IInit, ITick, IRenderable, IShipCollider, IProjectileCollider, IVelocity, IObstacle
    {
        protected static readonly Random Random = new Random();

        public uint RenderOrder { get; } = 10;

        public bool AutoRender { get; } = true;

        [Inject] public ContentManager Content { get; set; }
        [Inject] protected Transform Transform { get; set; }
        [Inject] public CameraManager CameraManager { get; set; }
        [Inject] public FlightSpaces FlightSpaces { get; set; }

        private Vector3 _spin;
        public Vector3 Velocity { get; set; } = Vector3.Zero;
        public float Health { get; set; } = 1f;
        public int Damage { get; set; } = 1;

        public float Radius { get; protected set; } = 1f;

        public virtual void Initialise()
        {
            _spin = new Vector3((float)Random.NextDouble(), (float)Random.NextDouble(), (float)Random.NextDouble());
            FlightSpaces.ObstacleSpace.Add(Entity);
        }

        public void Tick(TickContext context)
        {
            var spin = _spin * context.DeltaTimeSeconds;
            Transform.Update(() =>
            {
                Transform.Rotation *= Quaternion.CreateFromYawPitchRoll(spin.X, spin.Y, spin.Z);
                Transform.Location += Velocity * context.DeltaTimeSeconds;
            });
        }

        public abstract void Render(RenderContext context);

        public override void Dispose()
        {
            FlightSpaces.ObstacleSpace.Remove(Entity);
        }

        public virtual void OnHit(FlightNode node, FlightShip ship, Point gridLocation, Vector3 nodeLocation, Section section)
        {
            ship.Damage(gridLocation, Damage);
            Entity.Delete();
        }

        public virtual void OnHit(Entity projectileEntity, ProjectileBase projectile)
        {
            this.Update(() =>
            {
                Health -= projectile.GetDamage(Entity, this);
                if (Health <= 0)
                {
                    OnDie();
                    Entity.Delete();
                }
            });
        }

        protected virtual void OnDie()
        {

        }
    }
}
