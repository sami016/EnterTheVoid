using Forge.UI.Glass.Elements;
using Forge.UI.Glass.Stylings;
using Forge.UI.Glass.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.UI.Flight
{
    class OpenControls : Template
    {
        public override IElement Evaluate()
        {
            return new Pane(
                new Pane(
                    new Text("Open Space")
                    {
                        Position = new Rectangle(200, -10, 0, 0),
                        Font = "Title"
                    },
                    new Text("Hold to repair")
                    {
                        Position = new Rectangle(20, 20, 0, 0),
                        Font = "Default"
                    },
                    new ButtonTemplate("R", Keys.R)
                    {
                        Position = new Rectangle(50, 55, 40, 40)
                    }
                    //,
                    //new Text("Hold to leap")
                    //{
                    //    Position = new Rectangle(420, 20, 0, 0),
                    //    Font = "Default"
                    //},
                    //new ButtonTemplate("L", Keys.L)
                    //{
                    //    Position = new Rectangle(450, 55, 40, 40)
                    //}
                )
                {
                    Position = new Rectangle(GraphicsDevice.Viewport.Width/2-300, 300, 0, 0)
                }
            );
        }
    }
}
