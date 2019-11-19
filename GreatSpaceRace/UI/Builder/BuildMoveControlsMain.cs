using Forge.UI.Glass.Elements;
using Forge.UI.Glass.Stylings;
using Forge.UI.Glass.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.UI.Builder
{
    class BuildMoveControlsMain : Template
    {
        public override IElement Evaluate()
        {
            return new Pane(
                new Text("Move")
                {
                    Position = new Rectangle(20, -10, 0, 0),
                    Font = "Title"
                },
                new ButtonTemplate("W", Keys.W)
                {
                    Position = new Rectangle(50, 25, 40, 40)
                },
                new ButtonTemplate("A", Keys.A)
                {
                    Position = new Rectangle(5, 80, 40, 40)
                },
                new ButtonTemplate("S", Keys.S)
                {
                    Position = new Rectangle(50, 80, 40, 40)
                },
                new ButtonTemplate("D", Keys.D)
                {
                    Position = new Rectangle(100, 80, 40, 40)
                },
                new Pane()
                {
                    Position = new Rectangle(200, 50, 40, 40),
                    Background = new ImageBackgroundStyling
                    {
                        ImageResource = "LeftMouse"
                    }
                },
                new Text("Place")
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
                new Text("Cancel")
                {
                    Position = new Rectangle(400, 60, 0, 0),
                    Font = "Default"
                },
                new ButtonTemplate("R", Keys.R)
                {
                    Position = new Rectangle(550, 50, 40, 40)
                },
                new Text("Rotate")
                {
                    Position = new Rectangle(600, 60, 0, 0),
                    Font = "Default"
                },
                new ButtonTemplate("Q", Keys.Q)
                {
                    Position = new Rectangle(750, 50, 40, 40)
                },
                new Text("Shuffle")
                {
                    Position = new Rectangle(800, 60, 0, 0),
                    Font = "Default"
                }
            );
        }
    }
}
