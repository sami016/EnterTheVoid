using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Interfaces;
using Forge.Core.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntoTheVoid.Flight
{
    public class RadarRenderer : Component, IInit, ITick, IRenderable
    {
        private Texture2D _radarTexture;
        private Color _colour;

        public uint RenderOrder { get; } = 100;

        public bool AutoRender { get; } = true;

        [Inject] ContentManager Content { get; set; }
        [Inject] GraphicsDevice GraphicsDevice { get; set; }

        public void Initialise()
        {
            _radarTexture = Content.Load<Texture2D>("Textures/radar");
            _colour = new Color(255, 255, 255, 10);
        }

        public void Render(RenderContext context)
        {
            context.SpriteBatch.Begin();
            context.SpriteBatch.Draw(_radarTexture, new Rectangle(GraphicsDevice.Viewport.Width - 300, GraphicsDevice.Viewport.Height - 200, 300, 200), _colour);
            context.SpriteBatch.End();
        }

        public void Tick(TickContext context)
        {
        }
    }
}
