﻿using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
using Forge.Core.Interfaces;
using IntoTheVoid.Ships;
using IntoTheVoid.Ships.Modules;
using IntoTheVoid.Upgrades;
using IntoTheVoid.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntoTheVoid.Flight
{
    public class RocketControls : Component, ITick
    {
        [Inject] RocketCapability RocketCapabilities { get; set; }

        public RocketControls()
        {
        }

        public void Tick(TickContext context)
        {
            var keys = Keyboard.GetState();

            RocketCapabilities.Update(() =>
            {
                RocketCapabilities.RocketControl = new RocketControl
                {
                    Backwards = keys.IsKeyDown(Keys.S),
                    Forwards = keys.IsKeyDown(Keys.W),
                    Left = keys.IsKeyDown(Keys.A),
                    Right = keys.IsKeyDown(Keys.D),
                    RotatePort = keys.IsKeyDown(Keys.Q),
                    RotateStarboard = keys.IsKeyDown(Keys.E),
                };
            });

        }
    }
}
