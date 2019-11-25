﻿using Forge.Core.Components;
using Forge.Core.Scenes;
using Forge.UI.Glass;
using EnterTheVoid.Flight;
using EnterTheVoid.Intro;
using EnterTheVoid.UI.Intro;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.Scenes
{
    public class IntroScene : Scene
    {
        [Inject] UserInterfaceManager UserInterfaceManager { get; set; }

        public override void Initialise()
        {
            AddSingleton(new IntroBackgroundScroll());
            AddSingleton(new IntroControls());
            UserInterfaceManager.AddSceneUI(this, new IntroScreenTemplate());
        }
    }
}