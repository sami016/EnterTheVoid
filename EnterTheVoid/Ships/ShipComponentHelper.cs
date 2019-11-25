using Forge.Core.Engine;
using IntoTheVoid.Flight;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntoTheVoid.Ships
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
