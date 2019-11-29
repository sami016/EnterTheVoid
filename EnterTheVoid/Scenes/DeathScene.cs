using Forge.Core.Components;
using Forge.Core.Scenes;
using Forge.Core.Sound;
using Forge.UI.Glass;
using EnterTheVoid.UI.Menu;
using System;
using System.Collections.Generic;
using System.Text;
using EnterTheVoid.General;
using Microsoft.Xna.Framework;
using Forge.Core.Rendering.Cameras;
using EnterTheVoid.Constants;
using EnterTheVoid.Flight;

namespace EnterTheVoid.Scenes
{
    public class DeathScene : Scene
    {
        [Inject] UserInterfaceManager UserInterfaceManager { get; set; }
        [Inject] MusicManager MusicManager { get; set; }
        [Inject] SceneManager SceneManager { get; set; }

        public override void Initialise()
        {
            UserInterfaceManager.AddSceneUI(this, new DeathScreenTemplate(SceneManager));
            
            MusicManager.Start("Menu");
        }
    }
}
