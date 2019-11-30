using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Interfaces;
using Forge.Core.Rendering;
using Forge.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.General
{
    public class FadeTransition : Component, ITick, IRenderable
    {
        private CompletionTimer _completionTimer = new CompletionTimer(TimeSpan.FromSeconds(1));
        private bool _transitioning = false;
        private bool _ranSwap = false;
        private Action _swapAction;

        [Inject] RenderResources RenderResources { get; set; }
        [Inject] GraphicsDevice GraphicsDevice { get; set; }

        public uint RenderOrder { get; } = 1000;

        public bool AutoRender { get; } = true;

        public void StartTransition(Action swapAction)
        {
            if (_transitioning)
            {
                return;
            }
            _completionTimer.Restart();
            _ranSwap = false;
            _transitioning = true;
            _swapAction = swapAction;
        }

        public void Render(RenderContext context)
        {
            if (_transitioning)
            {
                var alpha = 1f;
                if (_completionTimer.CompletedFraction < 0.5f)
                {
                    alpha = _completionTimer.CompletedFraction / 0.5f;
                }
                if (_completionTimer.CompletedFraction > 0.5f)
                {
                    alpha = 1.0f - (_completionTimer.CompletedFraction - 0.5f) / 0.5f;
                }

                context.SpriteBatch.Begin();
                context.SpriteBatch.Draw(RenderResources.BlackTexture, GraphicsDevice.Viewport.Bounds, new Color(0, 0, 0, alpha));
                context.SpriteBatch.End();
            }
        }

        public void Tick(TickContext context)
        {
            if (_transitioning)
            {
                _completionTimer.Tick(context.DeltaTime);
                if (!_ranSwap && _completionTimer.CompletedFraction >= 0.5)
                {
                    _ranSwap = true;
                    _swapAction?.Invoke();
                }
                if (_completionTimer.Completed)
                {
                    _transitioning = false;
                }
            }
        }
    }
}
