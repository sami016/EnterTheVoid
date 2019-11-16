using Forge.Core.Components;
using Forge.Core.Interfaces;
using Forge.Core.Space;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Flight
{
    public class FlightSpaces : Component, IInit
    {
        public ISpace ObstacleSpace { get; set; }

        public void Initialise()
        {
            ObstacleSpace = Entity.Add(new Space(true));
        }
    }
}
