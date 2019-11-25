using EnterTheVoid.Ships;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.Flight
{
    public interface IShipCollider
    {
        void OnHit(FlightNode node, FlightShip ship, Point gridLocation, Vector3 nodeLocation, Section section);
    }
}
