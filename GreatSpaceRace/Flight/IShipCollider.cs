using GreatSpaceRace.Ships;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Flight
{
    public interface IShipCollider
    {
        void OnHit(FlightNode node, Vector3 nodeLocation, Section section);
    }
}
