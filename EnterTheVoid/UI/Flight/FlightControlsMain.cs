using Forge.UI.Glass.Elements;
using Forge.UI.Glass.Stylings;
using Forge.UI.Glass.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.UI.Flight
{
    class FlightControlsMain : Template
    {
        public override IElement Evaluate()
        {
            return new Pane(
                new Pane()
                {
                    Position = new Rectangle(200, 50, 40, 40),
                    Background = new ImageBackgroundStyling
                    {
                        ImageResource = "LeftMouse"
                    }
                },
                new Text("Light Fire")
                {
                    Position = new Rectangle(250, 60, 0, 0),
                    Font = "Default"
                },
                new Pane()
                {
                    Position = new Rectangle(350, 50, 40, 40),
                    Background = new ImageBackgroundStyling
                    {
                        ImageResource = "RightMouse"
                    }
                },
                new Text("Heavy Fire")
                {
                    Position = new Rectangle(400, 60, 0, 0),
                    Font = "Default"
                },

                new ButtonTemplate("1", Keys.D1)
                {
                    Position = new Rectangle(550, 50, 40, 40)
                },
                new Text("Shields")
                {
                    Position = new Rectangle(600, 60, 0, 0),
                    Font = "Default"
                },
                new ButtonTemplate("2", Keys.D2)
                {
                    Position = new Rectangle(750, 50, 40, 40)
                },
                new Text("Bombard")
                {
                    Position = new Rectangle(800, 60, 0, 0),
                    Font = "Default"
                },
                new ButtonTemplate("6", Keys.D6)
                {
                    Position = new Rectangle(950, 50, 40, 40)
                },
                new Text("Blast Rocket")
                {
                    Position = new Rectangle(1000, 60, 0, 0),
                    Font = "Default"
                },


                new Text("Controls")
                {
                    Position = new Rectangle(20, -10, 0, 0),
                    Font = "Title"
                },
                new ButtonTemplate("W", Keys.W)
                {
                    Position = new Rectangle(50, 55, 40, 40)
                },
                new ButtonTemplate("A", Keys.A)
                {
                    Position = new Rectangle(5, 100, 40, 40)
                },
                new ButtonTemplate("S", Keys.S)
                {
                    Position = new Rectangle(50, 100, 40, 40)
                },
                new ButtonTemplate("D", Keys.D)
                {
                    Position = new Rectangle(100, 100, 40, 40)
                },

                new ButtonTemplate("Q", Keys.Q)
                {
                    Position = new Rectangle(5, 30, 40, 40)
                },
                new ButtonTemplate("E", Keys.E)
                {
                    Position = new Rectangle(100, 30, 40, 40)
                }
            );
        }
    }
}
