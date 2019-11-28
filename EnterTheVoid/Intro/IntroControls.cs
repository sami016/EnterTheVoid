using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Interfaces;
using Forge.Core.Scenes;
using Forge.Core.Utilities;
using EnterTheVoid.Scenes;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using EnterTheVoid.General;

namespace EnterTheVoid.Intro
{
    public class IntroControls : Component, ITick
    {
        private CompletionTimer _intoTimer = new CompletionTimer(TimeSpan.FromSeconds(30));
        [Inject] SceneManager SceneManager { get; set; }
        [Inject] MouseControls MouseControls { get; set; }
        [Inject] FadeTransition FadeTransition { get; set; }

        public void Tick(TickContext context)
        {
            _intoTimer.Tick(context.DeltaTime);
            var keyboard = Keyboard.GetState();
            if (keyboard.GetPressedKeys().Length > 0
                || MouseControls.LeftClicked
                || _intoTimer.Completed)
            {
                FadeTransition.StartTransition(() => SceneManager.SetScene(new MenuScene()));
            }
        }
    }
}
