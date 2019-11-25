using Forge.UI.Glass.Elements;
using Forge.UI.Glass.Stylings;
using Forge.UI.Glass.Templates;
using Microsoft.Xna.Framework;

namespace EnterTheVoid.UI.Menu
{
    class MenuButton : Template
    {
        private readonly string _text;

        public MenuButton(string text)
        {
            _text = text;
        }

        public override IElement Evaluate() =>
            new Pane(
                    new Text(_text)
                    {
                        Position = new Rectangle(150 - 20, 75 - 7, 0, 0),
                    },
                    new Pane()
                    {
                        Position = new Rectangle(0, 0, 150, 150),
                        Background = new ImageBackgroundStyling
                        {
                            ImageResource = "Center"
                        }
                    }
                )
            {
                Background = new ColourBackgroundStyling
                {
                    Colour = new Color(30, 30, 30, 150)
                }
            };
    }
}
