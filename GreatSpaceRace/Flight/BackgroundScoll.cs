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

namespace GreatSpaceRace.Flight
{
    public class BackgroundScoll : Component, IInit, IRenderable, ITick
    {
        private Texture2D _starCluster;
        public const float scale = 2;

        public uint RenderOrder { get; } = 0;

        public bool AutoRender { get; } = true;

        [Inject] ContentManager Content { get; set; }
        [Inject] CameraManager CameraManager { get; set; }

        public void Initialise()
        {
            _starCluster = Content.Load<Texture2D>("Textures/Space_Background_3");
        }

        public void Render(RenderContext context)
        {
            var cameraPosition = CameraManager.ActiveCamera?.Entity?.Get<Transform>()?.Location ?? Vector3.Zero;
            var cameraX = (-cameraPosition.X * 25) % _starCluster.Width;
            var cameraY = (-cameraPosition.Z * 25) % _starCluster.Height;
            for (var i=-1; i<=1; i++)
            {
                for (var j = -1; j <= 1; j++)
                {
                    context.SpriteBatch.Begin();
                    context.SpriteBatch.Draw(_starCluster, new Rectangle(
                        (int)(cameraX + _starCluster.Width * i), 
                        (int)(cameraY + _starCluster.Height * j), 
                        (int)(_starCluster.Width * scale), 
                        (int)(_starCluster.Height * scale)), 
                        Color.White
                    );
                    context.SpriteBatch.End();
                }
            }

        }

        public void Tick(TickContext context)
        {
        }
    }
}
