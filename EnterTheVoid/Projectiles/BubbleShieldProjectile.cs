using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
using Forge.Core.Interfaces;
using Forge.Core.Rendering;
using Forge.Core.Rendering.Cameras;
using Forge.Core.Space.Shapes;
using Forge.Core.Utilities;
using EnterTheVoid.Flight;
using EnterTheVoid.Obstacles;
using EnterTheVoid.Phases.Asteroids;
using EnterTheVoid.Ships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.Projectiles
{
    public class BubbleShieldProjectile : ProjectileBase
    {
        private static Model _projectileModel;
        private readonly FlightNode _parent;

        public int Health { get; private set; }

        public BubbleShieldProjectile(Guid shipGuid, FlightNode parent, Vector3 parentVelocity, Vector3 direction, bool sizeUpgrade, bool healthUpdate): base(shipGuid, parentVelocity, direction, 0f, TimeSpan.FromSeconds(10))
        {
            Radius = 2f;
            Health = healthUpdate ? 75 : 50;
            if (sizeUpgrade)
            {
                Radius += 1f;
            }
            RenderOrder = 120;
            HitProjectiles = true;
            _parent = parent;
        }

        public override void Initialise()
        {
            if (_projectileModel == null)
            {
                _projectileModel = Content.Load<Model>("Models/bubble");
                _projectileModel.EnableDefaultLighting();
                _projectileModel.SetDiffuseColour(new Color(0, 180, 0));
                _projectileModel.ConfigureBasicEffects(x =>
                {
                    x.Alpha = 0.4f;
                });
            }
            base.Initialise();
        }

        protected override void UpdatePosition(TickContext context)
        {
            this.Update(() =>
            {
                Transform.Location = _parent.GloalLocation;
            });
        }

        public override float GetDamage(Entity hitEnt, IComponent hitComponent)
        {
            return 100f;
        }

        public override void Render(RenderContext context)
        {
            var previous = context.GraphicsDevice.BlendState;
            context.GraphicsDevice.BlendState = BlendState.AlphaBlend;
            context.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            var camera = CameraManager.ActiveCamera;
            _projectileModel.Draw(Matrix.CreateScale(Radius) * Transform.WorldTransform, camera.View, camera.Projection);
            context.GraphicsDevice.BlendState = previous;
        }

        public override void EntityDidHit(Entity entity)
        {
            if (entity.Has<ProjectileBase>())
            {
                var projectile = entity.Get<ProjectileBase>();
                var damage = (int)projectile.GetDamage(Entity, this);
                this.Update(() =>
                {
                    Health -= damage;
                    if (Health < 0)
                    {

                        base.EntityDidHit(entity);
                    }
                });
                return;
            }

            base.EntityDidHit(entity);
        }
    }
}
