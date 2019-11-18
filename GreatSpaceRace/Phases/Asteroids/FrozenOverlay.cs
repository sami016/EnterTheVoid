using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Interfaces;
using Forge.Core.Rendering;
using Forge.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Phases.Asteroids
{
    public class FrozenOverlay : Component, IInit, IRenderable, ITick
    {
        private Texture2D _frozenTexture;
        private bool _fading = false;
        private CompletionTimer _fadeTimer = new CompletionTimer(TimeSpan.FromSeconds(10));

        [Inject] ContentManager ContentManager { get; set; }
        
        [Inject] GraphicsDevice GraphicsDevice { get; set; }
        public uint RenderOrder { get; } = 80;

        public bool AutoRender { get; } = true;

        public void Initialise()
        {
            _frozenTexture = ContentManager.Load<Texture2D>("Textures/frozen");
        }

        public void Fade()
        {
            _fading = true;
            _fadeTimer.Restart();
        }

        public void Render(RenderContext context)
        {
            context.GraphicsDevice.BlendState = BlendState.Additive;
            var fraction = _fading ? (1f - _fadeTimer.CompletedFraction) : _fadeTimer.CompletedFraction;
            context.SpriteBatch.Begin();
            context.SpriteBatch.Draw(_frozenTexture, GraphicsDevice.Viewport.Bounds, Color.White * 0.3f * fraction);
            context.SpriteBatch.End();
        }

        public void Tick(TickContext context)
        {
            _fadeTimer.Tick(context.DeltaTime);
            if (_fading && _fadeTimer.Completed)
            {
                Entity.Delete();
            }
        }
    }
}
