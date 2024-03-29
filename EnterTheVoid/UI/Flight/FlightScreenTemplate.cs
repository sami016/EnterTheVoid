﻿using Forge.UI.Glass.Elements;
using Forge.UI.Glass.Templates;
using EnterTheVoid.Flight;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.UI.Flight
{
    class FlightScreenTemplate : Template
    {
        private readonly GraphicsDevice _graphicsDevice;
        private readonly FlightShip _flightShip;
        private readonly ProgressTracker _progressTracker;

        public FlightScreenTemplate(GraphicsDevice graphicsDevice, FlightShip flightShip, ProgressTracker progressTracker)
        {
            _graphicsDevice = graphicsDevice;
            _flightShip = flightShip;
            _progressTracker = progressTracker;
        }

        public override IElement Evaluate() =>
            new Pane(
                new FlightControlsMain()
                {
                    Position = new Rectangle(0, _graphicsDevice.Viewport.Height - 160, 150, 150)
                },
                new ShipStats(_flightShip)
                {
                    Position = new Rectangle(_graphicsDevice.Viewport.Width - 200, _graphicsDevice.Viewport.Height - 300, 200, 300)
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
