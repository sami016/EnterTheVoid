using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Interfaces;
using Forge.Core.Utilities;
using EnterTheVoid.Projectiles;
using EnterTheVoid.Ships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using EnterTheVoid.Ships.Modules;
using EnterTheVoid.Phases.Asteroids;
using Forge.Core.Rendering;
using EnterTheVoid.Utility;

namespace EnterTheVoid.Flight
{
    public class CombatControls : Component, ITick
    {
        [Inject] WeaponCapability WeaponCapability { get; set; }
        [Inject] MouseControls MouseControls { get; set; }

        public uint RenderOrder { get; } = 100;
        public bool AutoRender { get; } = true;


        public void Tick(TickContext context)
        {
            var keys = Keyboard.GetState();
            var mouse = Mouse.GetState();

            if (mouse.LeftButton == ButtonState.Pressed)
            {
                WeaponCapability?.StandardFire();
            }
            if (mouse.RightButton == ButtonState.Pressed)
            {
                WeaponCapability?.HeavyFire();
            }
        }

    }
}
