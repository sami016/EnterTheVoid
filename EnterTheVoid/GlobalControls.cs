using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Interfaces;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntoTheVoid
{
    public class GlobalControls : Component, ITick
    {
        public void Tick(TickContext context)
        {
            var keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Escape))
            {
                System.Environment.Exit(0);
            }
        }
    }
}
