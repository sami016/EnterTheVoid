using Forge.UI.Glass.Elements;
using Forge.UI.Glass.Templates;
using GreatSpaceRace.Phases.Transmission;
using GreatSpaceRace.Upgrades;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.UI.Upgrades
{
    public class UpgradeMenu : Template
    {
        private readonly IList<UpgradeBase> _upgrades;
        private readonly Action<UpgradeBase> _onSelect;

        public UpgradeMenu(IList<UpgradeBase> upgrades, Action<UpgradeBase> onSelect)
        {
            _upgrades = upgrades;
            _onSelect = onSelect;
        }

        public override IElement Evaluate()
        {
            var height = Vh * 50;
            var width = Vw * 100 - 20;

            var elements = new List<IElement>();
            for (var i=0; i<3; i++)
            {
                elements.Add(new UpgradeMenuOption(_upgrades[i], _onSelect)
                {
                    Position = new Rectangle((int)(i * width/3) + 5, 5, (int)(width/3) - 10, (int)height - 10)
                });
            }

            return new Pane(
                new Text("Select an upgrade:")
                {
                    Position = new Rectangle(50, 0, 0, 0),
                    Font = "Title"
                },
                new Pane(elements.ToArray())
                {
                    Position = new Rectangle(10, (int)(Vh*50 - height / 2), (int)width, (int)height)
                }
            ); ;
        }
    }
}
