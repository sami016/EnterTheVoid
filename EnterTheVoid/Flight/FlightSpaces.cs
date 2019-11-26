using Forge.Core.Components;
using Forge.Core.Interfaces;
using Forge.Core.Space;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.Flight
{
    public class FlightSpaces : Component, IInit
    {
        public ISpace ObstacleSpace { get; set; }
        public ISpace ShipSpace { get; set; }
        public ISpace ProjectileSpace { get; set; }

        public void Initialise()
        {
            ObstacleSpace = Entity.Add(new Space(true));
            ShipSpace = Entity.Add(new Space(true));
            ProjectileSpace = Entity.Add(new Space(true));
        }
    }
}
