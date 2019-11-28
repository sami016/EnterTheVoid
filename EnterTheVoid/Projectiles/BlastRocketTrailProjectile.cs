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
    public class BlastRocketProjectile : ProjectileBase
    {
        private static Model _projectileModel;
        private readonly Matrix _rotation;

        public BlastRocketProjectile(Guid shipGuid, Vector3 parentVelocity, Vector3 direction, Matrix rotation) : base(shipGuid, parentVelocity, direction, 10f, TimeSpan.FromSeconds(10))
        {
            RenderOrder = 120;
            _rotation = Matrix.CreateFromYawPitchRoll((float)Math.PI / 2, 0, 0) * rotation;
        }

        public override void Initialise()
        {
            if (_projectileModel == null)
            {
                _projectileModel = Content.Load<Model>("Models/trail2");
                //_projectileModel.EnableDefaultLighting();
                _projectileModel.SetDiffuseColour(Color.Yellow);
                _projectileModel.ConfigureBasicEffects(x =>
                {
                    x.Alpha = 0.4f;
                });
            }
            base.Initialise();
        }


        public override float GetDamage(Entity hitEnt, IComponent hitComponent)
        {
            return 10f;
        }

        public override void Render(RenderContext context)
        {
            context.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            var camera = CameraManager.ActiveCamera;
            _projectileModel.Draw(Matrix.CreateScale(1.1f) * _rotation * Transform.WorldTransform, camera.View, camera.Projection);
        }

    }
}
