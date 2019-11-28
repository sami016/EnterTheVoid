using Forge.Core.Engine;
using EnterTheVoid.Flight;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.Ships
{
    public static class ShipComponentHelper
    {
        public static FlightShip AddShipBasics(this Entity entity, ShipTopology topology)
        {
            var flightShip = entity.Add(new FlightShip(topology));
            entity.Add(new WeaponCapability());
            entity.Add(new RocketCapability());
            entity.Add(new ShipVelocityLimiter());
            return flightShip;
        }
    }
}
