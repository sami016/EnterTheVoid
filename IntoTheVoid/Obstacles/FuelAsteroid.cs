﻿using Forge.Core.Components;
using Forge.Core.Engine;
using Forge.Core.Rendering;
using Forge.Core.Utilities;
using IntoTheVoid.Phases.Asteroids;
using IntoTheVoid.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntoTheVoid.Obstacles
{
    public class FuelAsteroid : AsteroidBase
    {
        private static Model _asteroid1;


        public FuelAsteroid()
        {
            Damage = 20;
            Health = 40;
        }

        public override void Initialise()
        {
            if (_asteroid1 == null)
            {
                _asteroid1 = Content.Load<Model>("Models/asteroid2");
                _asteroid1.EnableDefaultLighting();
                _asteroid1.SetDiffuseColour(new Color(60, 30, 30));
            }
            base.Initialise();
        }

        public override void Render(RenderContext context)
        {
            context.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            var camera = CameraManager.ActiveCamera;
            _asteroid1.Draw(Transform.WorldTransform, camera.View, camera.Projection);
        }

        public override void OnHit(Entity projectileEntity, ProjectileBase projectile)
        {
            Entity.Delete();

            var fuelEnt = Entity.EntityManager.Create();
            fuelEnt.Add(new Transform
            {
                Location = Transform.Location
            });
            fuelEnt.Add(new Fuel());
        }
    }
}