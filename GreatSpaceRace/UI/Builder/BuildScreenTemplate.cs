using Forge.Core;
using Forge.Core.Scenes;
using Forge.UI.Glass.Elements;
using Forge.UI.Glass.Interaction;
using Forge.UI.Glass.Stylings;
using Forge.UI.Glass.Templates;
using GreatSpaceRace.Builder;
using GreatSpaceRace.Scenes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreatSpaceRace.UI.Builder
{

    class BuildScreenTemplate : Template
    {
        private readonly BuildMode _gamemode;

        public BuildScreenTemplate(BuildMode gamemode)
        {
            _gamemode = gamemode;
        }

        public override void Tick(TickContext context)
        {
            base.Tick(context);

            Reevaluate();
        }

        private string GetTimeMessage()
        {
            if (_gamemode.Building)
            {
                return $"{(int)Math.Ceiling(_gamemode.BuildSecondsRemaining)} days remaining";
            }
            if (_gamemode.State == BuildModeState.CountIn)
            {
                return $"Get ready...";
            }
            return $"Prepare for lift off...";
        }

        public override IElement Evaluate() =>
            new Pane(
                // Top
                new Pane(
                    new Text($"Ship Construction")
                    {
                        Position = new Rectangle(15, 15, 0, 0)
                    },
                    new Text(GetTimeMessage())
                    {
                        Position = new Rectangle(15, 15 + 30, 0, 0)
                    }
                )
                {
                    Position = new Rectangle(0, 0, 400, 75),
                    Background = new ColourBackgroundStyling
                    {
                        Colour = Color.SlateGray
                    }
                },
                // Bottom
                EvaluateBottom()
            )
            {
                //Background = new ImageBackgroundStyling
                //{
                //    ImageResource = "Starfield"
                //}
            };

        private IElement EvaluateBottom()
        {
            IList<IElement> sections = new List<IElement>();
            for (var i = 0; i < 10; i++)
            {
                sections.Add(new Pane()
                {
                    Background = new ColourBackgroundStyling
                    {
                        Colour = Color.White
                    },
                    Position = new Rectangle(i * 100 + 5, 5, 90, 90)
                });
            }
            return new Pane(
                sections.ToArray()
            )
            {
                Position = new Rectangle(400, 950, 1000, 100),
                Background = new ColourBackgroundStyling
                {
                    Colour = Color.SlateGray
                },
            };
        }

    }
}
