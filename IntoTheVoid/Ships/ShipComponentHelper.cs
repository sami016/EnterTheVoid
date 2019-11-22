using Forge.Core.Engine;
using GreatSpaceRace.Flight;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Ships
{
    public static class ShipComponentHelper
    {
        public static FlightShip AddShipBasics(this Entity entity, ShipTopology topology)
        {
            var flightShip = entity.Add(new FlightShip(topology));
            entity.Add(new WeaponCapability());
            entity.Add(new RocketCapability());
            return flightShip;
        }
    }
}
