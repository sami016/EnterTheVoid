using Forge.Core.Components;
using Forge.Core.Resources;
using Forge.Core.Scenes;
using Forge.Core.Sound;
using Forge.UI.Glass;
using GreatSpaceRace.UI.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Scenes
{
    public class StartupScene : Scene
    {
        [Inject] ResourceManager<SpriteFont> Fonts { get; set; }
        [Inject] ResourceManager<Texture2D> Textures { get; set; }
        [Inject] ResourceManager<Song> Songs { get; set; }
        [Inject] ResourceManager<SoundEffect> SoundEffects { get; set; }
        [Inject] ContentManager Content { get; set; }
        [Inject] SceneManager SceneManager { get; set; }
        [Inject] GraphicsDeviceManager GraphicsDeviceManager { get; set; }
        [Inject] GameWindow GameWindow { get; set; }

        public override void Initialise()
        {

            Fonts.Load("Default", Content.Load<SpriteFont>("Font/Default"));
            Fonts.Load("Title", Content.Load<SpriteFont>("Font/Title"));

            Textures.Load("StarCluster1", Content.Load<Texture2D>("Textures/StarCluster1"));
            Textures.Load("Starfield", Content.Load<Texture2D>("Textures/Starfield-7"));
            Textures.Load("Center", Content.Load<Texture2D>("Textures/center"));
            Textures.Load("Button", Content.Load<Texture2D>("Textures/button2"));
            Textures.Load("ButtonDown", Content.Load<Texture2D>("Textures/button"));
            Textures.Load("Logo", Content.Load<Texture2D>("Textures/logo"));
            Textures.Load("Power", Content.Load<Texture2D>("Textures/power"));
            Textures.Load("Fuel", Content.Load<Texture2D>("Textures/fuel"));
            Textures.Load("LeftMouse", Content.Load<Texture2D>("Textures/mouse_left"));
            Textures.Load("RightMouse", Content.Load<Texture2D>("Textures/mouse_right"));
            Textures.Load("RocketThruster", Content.Load<Texture2D>("Textures/rocket-thruster"));
            Textures.Load("RocketFlight", Content.Load<Texture2D>("Textures/rocket-flight"));
            Textures.Load("PlasmaBolt", Content.Load<Texture2D>("Textures/plasma-bolt"));
            Textures.Load("Cog", Content.Load<Texture2D>("Textures/cog"));

            Songs.Load("Menu", Content.Load<Song>("Music/Space Atmosphere"));
            Songs.Load("Building", Content.Load<Song>("Music/catinspace_hq"));
            Songs.Load("Ice", Content.Load<Song>("Music/Snow 02"));

            SoundEffects.Load("Hammer", Content.Load<SoundEffect>("Sounds/hammer"));
            SoundEffects.Load("Click", Content.Load<SoundEffect>("Sounds/click"));
            //Textures.Load("Rocket", Content.Load<Texture2D>("Icon/Rocket"));
            //Textures.Load("Settings", Content.Load<Texture2D>("Icon/Settings"));
            //Textures.Load("Globe", Content.Load<Texture2D>("Icon/Globe"));

            GraphicsDeviceManager.PreferredBackBufferWidth = GraphicsDeviceManager.GraphicsDevice.DisplayMode.Width;
            GraphicsDeviceManager.PreferredBackBufferHeight = GraphicsDeviceManager.GraphicsDevice.DisplayMode.Height;
            //GraphicsDeviceManager.IsFullScreen = true;
            //GraphicsDeviceManager.PreferredBackBufferWidth = 800;
            //GraphicsDeviceManager.PreferredBackBufferHeight = 600;
            GraphicsDeviceManager.ApplyChanges();

            GameWindow.Position = new Point(0, 0);

  //          SceneManager.SetScene(new MenuScene());
            SceneManager.SetScene(new IntroScene());
        }
    }
}
