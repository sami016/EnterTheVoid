using Forge.Core.Components;
using Forge.Core.Resources;
using Forge.Core.Scenes;
using Forge.UI.Glass;
using GreatSpaceRace.UI.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Scenes
{
    public class DebugMenuScene : Scene
    {
        private UIDispose _uiDispose;

        [Inject] UserInterfaceManager UserInterfaceManager { get; set; }
        [Inject] ResourceManager<SpriteFont> Fonts { get; set; }
        [Inject] ResourceManager<Texture2D> Textures { get; set; }
        [Inject] ContentManager Content { get; set; }
        [Inject] SceneManager SceneManager { get; set; }
        [Inject] GraphicsDeviceManager GraphicsDeviceManager { get; set; }

        public override void Initialise()
        {

            Fonts.Load("Default", Content.Load<SpriteFont>("Font/Default"));

            Textures.Load("StarCluster1", Content.Load<Texture2D>("Textures/StarCluster1"));
            Textures.Load("Starfield", Content.Load<Texture2D>("Textures/Starfield-7"));
            Textures.Load("Center", Content.Load<Texture2D>("Textures/center"));



            //Textures.Load("Rocket", Content.Load<Texture2D>("Icon/Rocket"));
            //Textures.Load("Settings", Content.Load<Texture2D>("Icon/Settings"));
            //Textures.Load("Globe", Content.Load<Texture2D>("Icon/Globe"));

            _uiDispose += UserInterfaceManager.Create(new DebugScreenTemplate(SceneManager));

            GraphicsDeviceManager.PreferredBackBufferWidth = GraphicsDeviceManager.GraphicsDevice.DisplayMode.Width;
            GraphicsDeviceManager.PreferredBackBufferHeight = GraphicsDeviceManager.GraphicsDevice.DisplayMode.Height;
            //GraphicsDeviceManager.IsFullScreen = true;
            GraphicsDeviceManager.ApplyChanges();
        }

        public override void Dispose()
        {
            base.Dispose();
            _uiDispose?.Invoke();
        }
    }
}
