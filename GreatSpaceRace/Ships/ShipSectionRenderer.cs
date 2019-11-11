using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Interfaces;
using Forge.Core.Rendering;
using Forge.Core.Rendering.Cameras;
using Forge.Core.Resources;
using Forge.Core.Utilities;
using GreatSpaceRace.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;

namespace GreatSpaceRace.Ships
{
    /// <summary>
    /// Not a renderable, but a render utility for drawing parts of ships.
    /// </summary>
    public class ShipSectionRenderer : Component, IInit, ITick
    {
        private Model _cellModel;
        private Model _connectorLargeModel;
        private Model _connectorSmallModel;
        private SpriteFont _d;

        [Inject] ContentManager Content { get; set; }
        [Inject] CameraManager CameraManager { get; set; }
        [Inject] ResourceManager<SpriteFont> FontManager { get; set; }
        [Inject] GraphicsDevice GraphicsDevice { get; set; }

        public void Initialise()
        {
            _cellModel = Content.Load<Model>("Models/cell");
            _cellModel.EnableDefaultLighting();
            _connectorLargeModel = Content.Load<Model>("Models/connector1");
            _connectorLargeModel.EnableDefaultLighting();
            _connectorSmallModel = Content.Load<Model>("Models/connector2");
            _connectorSmallModel.EnableDefaultLighting();

            _d = FontManager.Get("Default");
        }

        public void Render(RenderContext context, Transform transform, Section section)
        {
            context.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            var view = context.View;
            var projection = context.Projection;
            if (view == null)
            {
                view = CameraManager.ActiveCamera.View;
            }
            if (projection == null)
            {
                projection = CameraManager.ActiveCamera.Projection;
            }
            _cellModel.Draw(transform.WorldTransform, view.Value, projection.Value);
            for (var i = 0; i < 6; i++)
            {
                var rotatedDirection = (i+section.Rotation) % 6;
                var rot = Matrix.CreateRotationY((float)(-rotatedDirection * Math.PI * 2 / 6));

                if (section.ConnectionLayout.LargeConnectors.Contains(i))
                {
                    _connectorLargeModel.Draw(rot * transform.WorldTransform, view.Value, projection.Value);
                }
                if (section.ConnectionLayout.SmallConectors.Contains(i))
                { 
                    _connectorSmallModel.Draw(rot * transform.WorldTransform, view.Value, projection.Value);
                }
            }

            // debug string
            //var screenPos = Vector3.Transform(Vector3.Transform(Vector3.Transform(Vector3.Zero, transform.WorldTransform), view.Value), projection.Value);
            //context.SpriteBatch.Begin(depthStencilState: DepthStencilState.Default);
            //context.SpriteBatch.DrawString(_d, HexagonHelpers.GetWorldGridPosition(transform.Location).ToString(), new Vector2(screenPos.X * GraphicsDevice.Viewport.Width / 2, screenPos.Y * GraphicsDevice.Viewport.Width / 2), Color.White);
            //context.SpriteBatch.End();
        }

        public void Tick(TickContext context)
        {
        }
    }
}
