using Forge.Core.Components;
using Forge.Core.Engine;
using Forge.Core.Rendering;
using Forge.Core.Utilities;
using EnterTheVoid.Phases.Asteroids;
using EnterTheVoid.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.Obstacles
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

    }
}
