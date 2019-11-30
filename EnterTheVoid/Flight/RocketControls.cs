using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
using Forge.Core.Interfaces;
using EnterTheVoid.Ships;
using EnterTheVoid.Ships.Modules;
using EnterTheVoid.Upgrades;
using EnterTheVoid.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forge.Core.Utilities;

namespace EnterTheVoid.Flight
{
    public class RocketControls : Component, ITick
    {
        [Inject] RocketCapability RocketCapabilities { get; set; }
        [Inject] MouseControls MouseControls { get; set; }

        public RocketControls()
        {
        }

        public void Tick(TickContext context)
        {
            var keys = Keyboard.GetState();
            var mouse = Mouse.GetState();

            RocketCapabilities.Update(() =>
            {
                RocketCapabilities.RocketControl = new RocketControl
                {
                    Backwards = keys.IsKeyDown(Keys.S) || keys.IsKeyDown(Keys.Down),
                    Forwards = keys.IsKeyDown(Keys.W) || keys.IsKeyDown(Keys.Up),
                    Left = keys.IsKeyDown(Keys.A) || keys.IsKeyDown(Keys.Left),
                    Right = keys.IsKeyDown(Keys.D) || keys.IsKeyDown(Keys.Right),
                    RotatePort = keys.IsKeyDown(Keys.Q) || MouseControls.ScrollWheelValueDelta > 0,
                    RotateStarboard = keys.IsKeyDown(Keys.E) || MouseControls.ScrollWheelValueDelta < 0,
                };
            });

        }
    }
}
