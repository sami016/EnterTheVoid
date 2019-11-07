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
using Forge.Core.Utilities;
using Microsoft.Xna.Framework.Input;

namespace GreatSpaceRace.Builder
{
    class Floor : Component, IRenderable, IInit, ITick
    {
        private Model _floorModel;
        private Model _cellModel;
        private readonly Point _gridPosition;

        [Inject] ContentManager Content { get; set; }
        [Inject] CameraManager CameraManager { get; set; }
        [Inject] Transform Transform { get; set; }

        public uint RenderOrder { get; } = 0;

        public Floor(Point gridPosition)
        {
            _gridPosition = gridPosition;
        }

        public void Initialise()
        {
            _floorModel = Content.Load<Model>("Models/floor");
            _floorModel.EnableDefaultLighting();
            _cellModel = Content.Load<Model>("Models/cell");
            _cellModel.EnableDefaultLighting(); 
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
            _cellModel.Draw(Transform.WorldTransform, camera.View, camera.Projection);
            _floorModel.Draw(Matrix.CreateTranslation(Transform.Location - Vector3.Up * 0.1f), camera.View, camera.Projection);
        }

        public void Tick(TickContext context)
        {
            var keyboard = Keyboard.GetState();
            this.Update(() =>
            {
                if (keyboard.IsKeyDown(Keys.T))
                {
                    Transform.Rotation *= Quaternion.CreateFromYawPitchRoll(0, (float)(Math.PI * context.DeltaTimeSeconds), 0);
                }
                //Transform.Location += Vector3.Up * context.DeltaTimeSeconds * 0.1f;
            });
        }
    }
}
