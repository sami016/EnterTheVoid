using IntoTheVoid.Ships;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntoTheVoid.Flight
{
    public interface IShipCollider
    {
        void OnHit(FlightNode node, FlightShip ship, Point gridLocation, Vector3 nodeLocation, Section section);
    }
}
