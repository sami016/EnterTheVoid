using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Interfaces;
using Forge.Core.Rendering;
using Forge.Core.Rendering.Cameras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntoTheVoid.Intro
{
    public class IntroBackgroundScroll : Component, IInit, IRenderable, ITick
    {
        private Texture2D _introImage;
        public const float scale = 1;

        public uint RenderOrder { get; } = 0;

        public bool AutoRender { get; } = true;

        private Vector2 _animatePos = Vector2.Zero;

        [Inject] ContentManager Content { get; set; }
        [Inject] CameraManager CameraManager { get; set; }

        public void Initialise()
        {
            _introImage = Content.Load<Texture2D>("Textures/OGA-Background-1");
        }

        public void Render(RenderContext context)
        {
            var cameraPosition = CameraManager.ActiveCamera?.Entity?.Get<Transform>()?.Location ?? Vector3.Zero;
            var cameraX = (-_animatePos.X) % _introImage.Width;
            var cameraY = (-_animatePos.Y) % _introImage.Height;
            for (var i=-1; i<=1; i++)
            {
                for (var j = -1; j <= 1; j++)
                {
                    context.SpriteBatch.Begin();
                    context.SpriteBatch.Draw(_introImage, new Rectangle(
                        (int)(cameraX + _introImage.Width * i), 
                        (int)(cameraY + _introImage.Height * j), 
                        (int)(_introImage.Width * scale), 
                        (int)(_introImage.Height * scale)), 
                        Color.White
                    );
                    context.SpriteBatch.End();
                }
            }

        }

        public void Tick(TickContext context)
        {
            _animatePos += Vector2.One * 20 * context.DeltaTimeSeconds;
        }
    }
}
