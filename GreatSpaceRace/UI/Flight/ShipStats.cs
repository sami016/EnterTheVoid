using Forge.Core;
using Forge.UI.Glass.Elements;
using Forge.UI.Glass.Stylings;
using Forge.UI.Glass.Templates;
using GreatSpaceRace.Flight;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.UI.Flight
{
    public class ShipStats : Template
    {
        private readonly FlightShip _flightShip;

        public ShipStats(FlightShip flightShip)
        {
            _flightShip = flightShip;
        }

        public override void Tick(TickContext context)
        {
            base.Tick(context);

            Reevaluate();
        }

        public override IElement Evaluate() => new Pane(
            new Text("Hull Integity")
            {
                Position = new Rectangle(0, 0, 0, 0)
            },
            new Pane()
            {
                Position = new Rectangle(0, 30, 100, 30),
                Background = new ColourBackgroundStyling()
                {
                    Colour = Color.Red
                }
            },
            new Pane()
            {
                Position = new Rectangle(0, 30, (int)(100 * _flightShip.Health / _flightShip.MaxHealth), 30),
                Background = new ColourBackgroundStyling()
                {
                    Colour = Color.Green
                }

            }
        );
    }
}
