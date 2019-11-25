using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Interfaces;
using Forge.Core.Utilities;
using IntoTheVoid.Projectiles;
using IntoTheVoid.Ships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using IntoTheVoid.Ships.Modules;
using IntoTheVoid.Phases.Asteroids;
using Forge.Core.Rendering;
using IntoTheVoid.Utility;

namespace IntoTheVoid.Flight
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
