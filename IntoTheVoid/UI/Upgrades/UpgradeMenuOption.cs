using Forge.UI.Glass.Elements;
using Forge.UI.Glass.Interaction;
using Forge.UI.Glass.Stylings;
using Forge.UI.Glass.Templates;
using GreatSpaceRace.Phases.Transmission;
using GreatSpaceRace.Upgrades;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.UI.Upgrades
{
    public class UpgradeMenuOption : Template
    {
        private readonly UpgradeBase _upgrade;
        private readonly Action<UpgradeBase> _onSelect;

        public UpgradeMenuOption(UpgradeBase upgrade, Action<UpgradeBase> onSelect)
        {
            _upgrade = upgrade;
            _onSelect = onSelect;
        }

        public override IElement Evaluate()
        {
            var height = Vh * 50;
            var width = (Vw * 100 - 20) / 3 - 10;
            return new Pane(
                new Text(_upgrade.Name)
                {
                    Font = "Title",
                    Position = new Rectangle(15, 10, 0, 0)
                },
                new Pane()
                {
                    Position = new Rectangle((int)(width / 2) - 150, 50, 330, 300),
                    Background = new ImageBackgroundStyling {
                        ImageResource = _upgrade.TextureResource
                    }
                },
                new Text(_upgrade.Description)
                {
                    Font = "Default",
                    Position = new Rectangle(50, 400, 0, 0)
                }
            )
            {
                Background = new ColourBackgroundStyling
                {
                    Colour = Color.DarkGray
                },
                Init = el => el.Events.Subscribe<ClickUIEvent>(e => _onSelect(_upgrade))
            };
        }
    }
}
