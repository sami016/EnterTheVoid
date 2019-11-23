using Forge.UI.Glass.Elements;
using Forge.UI.Glass.Stylings;
using Forge.UI.Glass.Templates;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntoTheVoid.UI.Intro
{
    class IntroScreenTemplate : Template
    {
        public override IElement Evaluate()
        {
            return new Pane(
                new Pane(
                    new AnimatedText("Into The Void")
                    {
                        Font = "Title",
                    },

                    new AnimatedText("The year is 2084...", TimeSpan.FromMilliseconds(50), TimeSpan.FromSeconds(2))
                    {
                        Position = new Rectangle(0, 60, 0, 0)
                    },
                    new AnimatedText("Recent technological advances have opened up a new frontier of exploration for the human race...", TimeSpan.FromMilliseconds(50), TimeSpan.FromSeconds(4))
                    {
                        Position = new Rectangle(0, 100, 0, 0)
                    },
                    new AnimatedText("The doorways to new worlds and ever-expanding opportunities are opening... and with it a fierce competition.", TimeSpan.FromMilliseconds(50), TimeSpan.FromSeconds(10))
                    {
                        Position = new Rectangle(0, 140, 0, 0)
                    },
                    new AnimatedText("The race is underway to determine who has what it takes to traverse the unforgiving places beyond our atmosphere.", TimeSpan.FromMilliseconds(50), TimeSpan.FromSeconds(17))
                    {
                        Position = new Rectangle(0, 180, 0, 0)
                    },
                    new AnimatedText("This is the story of the race between pioneers. Who will have what it takes to claim this brave new domain?", TimeSpan.FromMilliseconds(50), TimeSpan.FromSeconds(24))
                    {
                        Position = new Rectangle(0, 220, 0, 0)
                    },
                    //Many nations, corporations and talented individuals 
                    new Text("Press any button to continue...")
                    {
                        Position = new Rectangle(GraphicsDevice.Viewport.Width / 2 - 100, GraphicsDevice.Viewport.Height - 200, 0, 0)
                    }
                )
                {
                    Position = new Rectangle(50, 50, GraphicsDevice.Viewport.Width - 100, GraphicsDevice.Viewport.Height - 100),
                    Background = new ColourBackgroundStyling
                    {
                        Colour = new Color(Color.Black, 0.3f)
                    }
                }
            )
            {
                Background = new ColourBackgroundStyling
                {
                    Colour = new Color(Color.Black, 0.3f)
                }
            };
        }
    }
}
