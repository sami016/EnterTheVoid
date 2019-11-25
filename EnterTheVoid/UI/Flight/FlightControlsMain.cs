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
                new Text("Thrust")
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
