using Forge.Core.Components;
using Forge.Core.Interfaces;
using Forge.Core.Rendering;
using Forge.Core.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.Phases
{
    class PhaseDistanceTargetRenderable : Component, IInit, IRenderable
    {
        private Texture2D _arrowTexture;

        public uint RenderOrder { get; } = 100;

        public bool AutoRender { get; } = true;

        [Inject] PhaseDistanceTarget PhaseDistanceTarget { get; set; }
        [Inject] ResourceManager<SpriteFont> Sprites { get; set; }
        [Inject] GraphicsDevice GraphicsDevice { get; set; }
        [Inject] ContentManager ContentManager { get; set; }

        public void Initialise()
        {
            _arrowTexture = ContentManager.Load<Texture2D>("Textures/arrow");
        }

        public void Render(RenderContext context)
        {
            var font = Sprites.Get("Default");
            var text = $"Target distance: {(-PhaseDistanceTarget.Remaining).ToString("n0")} units";
            var measure = font.MeasureString(text);
            var pos = new Vector2((GraphicsDevice.Viewport.Width - measure.X) / 2, 10);
            context.SpriteBatch.Begin();
            context.SpriteBatch.DrawString(font, text, pos, Color.White);
            context.SpriteBatch.Draw(_arrowTexture, new Rectangle(GraphicsDevice.Viewport.Width / 2 - 20, 40, 40, 40), Color.White);
            context.SpriteBatch.End();
        }
    }
}
