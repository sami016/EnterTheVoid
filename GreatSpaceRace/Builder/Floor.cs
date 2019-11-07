using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
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
    class Floor : Component, IRenderable, IInit, ITick
    {
        private Model _floorModel;
        private Model _cellModel;

        [Inject] ContentManager Content { get; set; }
        [Inject] CameraManager CameraManager { get; set; }
        [Inject] Transform Transform { get; set; }

        public void Initialise()
        {
            _floorModel = Content.Load<Model>("Models/floor");
            _cellModel = Content.Load<Model>("Models/floor");
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
            context.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            var camera = CameraManager.ActiveCamera;
            //var camera = new
            //{
            //    View = Matrix.CreateLookAt(Vector3.Backward, Vector3.Zero, Vector3.Up),
            //    Projection = CameraManager.ActiveCamera.Projection
            //};
            _floorModel.Draw(Transform.WorldTransform, camera.View, camera.Projection);
        }

        public void Tick(TickContext context)
        {
            this.Update(() =>
            {
                //Transform.Rotation *= Quaternion.CreateFromYawPitchRoll(0, (float)(Math.PI * context.DeltaTimeSeconds), 0);
                //Transform.Location += Vector3.Up * context.DeltaTimeSeconds * 0.1f;
            });
        }
    }
}
