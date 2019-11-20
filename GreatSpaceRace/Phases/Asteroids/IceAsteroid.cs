using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
using Forge.Core.Interfaces;
using Forge.Core.Rendering;
using Forge.Core.Rendering.Cameras;
using Forge.Core.Space.Shapes;
using Forge.Core.Utilities;
using GreatSpaceRace.Flight;
using GreatSpaceRace.Projectiles;
using GreatSpaceRace.Ships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Phases.Asteroids
{
    public class IceAsteroid : AsteroidBase
    {
        private static Model _iceCrystalsModel;
        private static Model _asteroid1Model;

        public Matrix _rockScale = Matrix.CreateScale((float)Random.NextDouble() * 2);

        public IceAsteroid()
        {
            Health = 50;
            Radius = 2f;
        }

        public override void Initialise()
        {
            if (_iceCrystalsModel == null)
            {
                _iceCrystalsModel = Content.Load<Model>("Models/ice");
                _iceCrystalsModel.EnableDefaultLighting();
                _asteroid1Model = Content.Load<Model>("Models/asteroid3");
                _asteroid1Model.EnableDefaultLighting();
                _asteroid1Model.SetDiffuseColour(Color.Gray);
            }
            base.Initialise();
        }

        public override void Render(RenderContext context)
        {
            context.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            var camera = CameraManager.ActiveCamera;
            _iceCrystalsModel.Draw(Transform.WorldTransform, camera.View, camera.Projection);
            _asteroid1Model.Draw(_rockScale * Transform.WorldTransform, camera.View, camera.Projection);
        }

    }
}
