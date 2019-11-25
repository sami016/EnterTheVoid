using Forge.Core;
using Forge.Core.Interfaces;
using Forge.UI.Glass.Elements;
using Forge.UI.Glass.Stylings;
using Forge.UI.Glass.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntoTheVoid.UI
{
    public class ButtonTemplate : Template
    {
        private readonly string _keyString;
        private readonly Keys _key;

        public ButtonTemplate(string keyString, Keys key)
        {
            _keyString = keyString;
            _key = key;
        }

        public override void Tick(TickContext context)
        {
            base.Tick(context);

            Reevaluate();
        }

        public override IElement Evaluate()
        {
            var down = Keyboard.GetState().IsKeyDown(_key);
            return new Pane(
                new Text(_keyString)
                {
                    Position = new Rectangle(14, 13, 0, 0),
                    Colour = Color.WhiteSmoke
                }
            )
            {
                Position = new Rectangle(0, 0, 40, 40),
                Background = new ImageBackgroundStyling
                {
                    ImageResource = down ? "Button" : "ButtonDown"
                }
            };
        }
    }
}
