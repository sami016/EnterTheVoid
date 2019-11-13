using Forge.UI.Glass.Elements;
using Forge.UI.Glass.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.UI.Flight
{
    class FlightScreenTemplate : Template
    {
        private readonly GraphicsDevice _graphicsDevice;

        public FlightScreenTemplate(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        public override IElement Evaluate() =>
            new Pane(
                new FlightControlsMain()
                {
                    Position = new Rectangle(0, _graphicsDevice.Viewport.Height - 160, 150, 150)
                }
            )
            {
                //Background = new ImageBackgroundStyling
                //{
                //    ImageResource = "Starfield"
                //}
            };
    }
}
