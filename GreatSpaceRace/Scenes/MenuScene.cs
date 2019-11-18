﻿using Forge.Core.Components;
using Forge.Core.Scenes;
using Forge.Core.Sound;
using Forge.UI.Glass;
using GreatSpaceRace.UI.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Scenes
{
    public class MenuScene : Scene
    {

        [Inject] UserInterfaceManager UserInterfaceManager { get; set; }
        [Inject] MusicManager MusicManager { get; set; }
        [Inject] SceneManager SceneManager { get; set; }

        public override void Initialise()
        {
            UserInterfaceManager.AddSceneUI(this, new MenuScreenTemplate(SceneManager));

            MusicManager.Start("Menu");
        }
    }
}