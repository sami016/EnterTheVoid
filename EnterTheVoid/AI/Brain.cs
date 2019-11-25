using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Interfaces;
using EnterTheVoid.Flight;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.AI
{
    public abstract class Brain : Component, ITick, IInit
    {
        protected static readonly Random Random = new Random();
        [Inject] public RocketCapability RocketCapability { get; set; }
        [Inject] public WeaponCapability WeaponCapability { get; set; }
        [Inject] public Transform Transform { get; set; }
        [Inject] public FlightShip FlightShip { get; set; }

        public virtual void Initialise()
        {

        }

        public abstract void Tick(TickContext context);
    }
}
