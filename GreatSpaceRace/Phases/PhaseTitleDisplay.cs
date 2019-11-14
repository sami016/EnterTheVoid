using Forge.Core.Components;
using Forge.Core.Rendering;
using Forge.Core.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Phases
{
    public class PhaseTitleDisplay : Component, IRenderable
    {
        [Inject] PhaseManager PhaseManager { get; set; }
        [Inject] ResourceManager<SpriteFont> Sprites { get; set; }
        [Inject] GraphicsDevice GraphicsDevice { get; set; }

        public uint RenderOrder { get; } = 140;

        public bool AutoRender { get; } = true;

        public void Render(RenderContext context)
        {
            if (PhaseManager.State == PhaseManagerState.Starting
                && PhaseManager.PhaseStartFraction > 0.1)
            {
                var titleFont = Sprites.Get("Title");
                var defaultFont = Sprites.Get("Default");
                var titleWidth = titleFont.MeasureString(PhaseManager.CurrentPhase.Title).X;
                var descriptionWidth = defaultFont.MeasureString(PhaseManager.CurrentPhase.Description).X;
                context.SpriteBatch.Begin();
                context.SpriteBatch.DrawString(
                    Sprites.Get("Title"),
                    PhaseManager.CurrentPhase.Title,
                    new Vector2(
                        (int)(GraphicsDevice.Viewport.Width / 2 - titleWidth / 2),
                        (int)(GraphicsDevice.Viewport.Height * 3 / 10)
                    ),
                    Color.White
                );
                context.SpriteBatch.DrawString(
                    Sprites.Get("Default"),
                    PhaseManager.CurrentPhase.Description,
                    new Vector2(
                        (int)(GraphicsDevice.Viewport.Width / 2 - descriptionWidth / 2),
                        (int)(GraphicsDevice.Viewport.Height * 3 / 10 + 30)
                    ),
                    Color.White
                );
                context.SpriteBatch.End();
            }
        }
    }
}
