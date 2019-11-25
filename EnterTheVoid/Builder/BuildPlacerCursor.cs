using Forge.Core.Components;
using Forge.Core.Interfaces;
using Forge.Core.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntoTheVoid.Builder
{
    public class BuildPlacerCursor : Component, IInit, IRenderable
    {
        private Texture2D _hexTexture;

        [Inject] ContentManager Content { get; set; }
        [Inject] BuildPlacer BuildPlacer { get; set; }

        public uint RenderOrder { get; } = 200;

        public bool AutoRender { get; } = true;

        public void Initialise()
        {
            _hexTexture = Content.Load<Texture2D>("Textures/hex");
        }

        public void Render(RenderContext context)
        {
            if (BuildPlacer.PlacingSection != null)
            {
                var mouse = Mouse.GetState();   
                context.SpriteBatch.Begin(depthStencilState: DepthStencilState.None);
                context.SpriteBatch.Draw(_hexTexture, new Rectangle(mouse.X - 20, mouse.Y - 20, 40, 40), Color.White);
                context.SpriteBatch.End();
            }
        }
    }
}
