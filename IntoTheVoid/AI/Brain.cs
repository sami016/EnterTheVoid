using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Interfaces;
using GreatSpaceRace.Flight;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.AI
{
    public abstract class Brain : Component, ITick, IInit
    {
        [Inject] public RocketCapability RocketCapability { get; set; }
        [Inject] public WeaponCapability WeaponCapability { get; set; }

        public virtual void Initialise()
        {

        }

        public abstract void Tick(TickContext context);
    }
}
