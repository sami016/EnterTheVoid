﻿using Forge.Core;
using Forge.Core.Engine;
using Forge.Core.Components;
using Forge.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntoTheVoid.Flight
{
    public class ShipVelocityLimiter : Component, ITick
    {
        [Inject] FlightShip Ship { get; set; }

        public float Limit { get; set; } = 10f;

        public void Tick(TickContext context)
        {
            var mag = Ship.Velocity.Length();
            if (mag > Limit)
            {
                Ship.Update(() =>
                {
                    Ship.Velocity *= 0.98f;
                });
            }
        }
    }
}
