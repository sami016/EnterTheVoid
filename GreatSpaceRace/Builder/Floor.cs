using Forge.Core.Components;
using Forge.Core.Interfaces;
using Forge.Core.Rendering;
using Forge.Core.Rendering.Cameras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Builder
{
    class Floor : Component, IRenderable, IInit
    {
        private Model _floorModel;

        [Inject] ContentManager Content { get; set; }
        [Inject] CameraManager CameraManager { get; set; }

        public void Initialise()
        {
            _floorModel = Content.Load<Model>("Models/cell");
            foreach (var mesh in _floorModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                }
            }
        }

        public void Render(RenderContext context)
        {
            var camera = CameraManager.ActiveCamera;
            _floorModel.Draw(Matrix.Identity, camera.View, camera.Projection);
        }
    }
}
