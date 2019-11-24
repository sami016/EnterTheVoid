using Forge.Core.Components;
using Forge.Core.Rendering;
using Forge.Core.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntoTheVoid.Phases
{
    class PhaseDistanceTargetRenderable : Component, IRenderable
    {
        public uint RenderOrder { get; } = 100;

        public bool AutoRender { get; } = true;

        [Inject] PhaseDistanceTarget PhaseDistanceTarget { get; set; }
        [Inject] ResourceManager<SpriteFont> Sprites { get; set; }
        [Inject] GraphicsDevice GraphicsDevice { get; set; }

        public void Render(RenderContext context)
        {
            var font = Sprites.Get("Default");
            var text = $"Target distance: {(-PhaseDistanceTarget.Remaining).ToString("n0")} units";
            var measure = font.MeasureString(text);
            var pos = new Vector2((GraphicsDevice.Viewport.Width - measure.X) / 2, 10);
            context.SpriteBatch.Begin();
            context.SpriteBatch.DrawString(font, text, pos, Color.White);
            context.SpriteBatch.End();
        }
    }
}
