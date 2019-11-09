using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Interfaces;
using Forge.Core.Rendering;
using Forge.Core.Rendering.Cameras;
using Forge.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Ships
{
    public class ShipSectionRenderable : Component, IRenderable, IInit, ITick
    {
        private Model _cellModel;
        private Model _connectorLargeModel;
        private Model _connectorSmallModel;
        private readonly Point _gridPosition;

        [Inject] ContentManager Content { get; set; }
        [Inject] CameraManager CameraManager { get; set; }
        [Inject] Transform Transform { get; set; }

        public uint RenderOrder { get; } = 0;

        public ShipSectionRenderable(Point gridPosition)
        {
            _gridPosition = gridPosition;
        }

        public void Initialise()
        {
            _cellModel = Content.Load<Model>("Models/cell");
            _cellModel.EnableDefaultLighting();
            _connectorLargeModel = Content.Load<Model>("Models/connector1");
            _connectorLargeModel.EnableDefaultLighting();
            _connectorSmallModel = Content.Load<Model>("Models/connector2");
            _connectorSmallModel.EnableDefaultLighting();
        }

        public void Render(RenderContext context)
        {
            context.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            var camera = CameraManager.ActiveCamera;
            _cellModel.Draw(Transform.WorldTransform, camera.View, camera.Projection);
            for (var i = 0; i < 6; i++)
            {
                var rot = Matrix.CreateRotationY((float)(i * Math.PI * 2 / 6));
                _connectorLargeModel.Draw(rot * Transform.WorldTransform, camera.View, camera.Projection);
                _connectorSmallModel.Draw(rot * Transform.WorldTransform, camera.View, camera.Projection);
            }
        }

        public void Tick(TickContext context)
        {
        }
    }
}
