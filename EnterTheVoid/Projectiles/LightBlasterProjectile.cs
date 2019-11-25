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
    public class LightBlasterProjectile : ProjectileBase
    {
        private static Model _projectileModel;

        public LightBlasterProjectile(Guid shipGuid, Vector3 parentVelocity, Vector3 direction, bool speedUpgrade): base(shipGuid, parentVelocity, direction, speedUpgrade ? 20f : 10f, TimeSpan.FromSeconds(4))
        {
        }

        public override void Initialise()
        {
            if (_projectileModel == null)
            {
                _projectileModel = Content.Load<Model>("Models/bullet");
                //_projectileModel.EnableDefaultLighting();
                _projectileModel.SetDiffuseColour(Color.White);
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
            _projectileModel.Draw(Matrix.CreateScale(0.1f) * Transform.WorldTransform, camera.View, camera.Projection);
        }

    }
}
