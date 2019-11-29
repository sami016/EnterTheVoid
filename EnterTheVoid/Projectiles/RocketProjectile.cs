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
using EnterTheVoid.General;

namespace EnterTheVoid.Projectiles
{
    public class RocketProjectile : ProjectileBase
    {
        private static Model _projectileModel;
        private readonly Matrix _rotationMatrix;

        public RocketProjectile(Guid shipGuid, Vector3 parentVelocity, Vector3 direction, Matrix rotationMatrix): base(shipGuid, parentVelocity, direction, 6f, TimeSpan.FromSeconds(12))
        {
            Radius = 0.2f;
            _rotationMatrix = Matrix.CreateRotationY(-(float)Math.PI/2) * rotationMatrix;
        }

        public override void Initialise()
        {
            if (_projectileModel == null)
            {
                _projectileModel = Content.Load<Model>("Models/rocket");
                _projectileModel.EnableDefaultLighting();
                _projectileModel.SetDiffuseColour(Color.White);
            }
            base.Initialise();
        }


        public override float GetDamage(Entity hitEnt, IComponent hitComponent)
        {
            return 22f;
        }

        public override void Render(RenderContext context)
        {
            context.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            var camera = CameraManager.ActiveCamera;
            _projectileModel.Draw(Matrix.CreateScale(1f) * _rotationMatrix * Transform.WorldTransform, camera.View, camera.Projection);
        }

        public override void EntityDidHit(Entity entity)
        {
            base.EntityDidHit(entity);


            var explosionEnt = Entity.EntityManager.Create();
            explosionEnt.Add(new Transform() { Location = Transform.Location });
            explosionEnt.Add(new ClusterExplosionEffect() { 
                DistanceScaleFactor = 5f,
                ScaleFactor = 2f,
            });
        }
    }
}
