using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Scenes;
using Forge.UI.Glass.Elements;
using Forge.UI.Glass.Interaction;
using Forge.UI.Glass.Stylings;
using Forge.UI.Glass.Templates;
using GreatSpaceRace.Builder;
using GreatSpaceRace.Scenes;
using GreatSpaceRace.Ships;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forge.Core.Engine;
using GreatSpaceRace.UI.Flight;

namespace GreatSpaceRace.UI.Builder
{

    class BuildScreenTemplate : Template
    {
        private readonly BuildMode _gamemode;
        private readonly ProductionLine _productionLine;
        private readonly BuildPlacer _buildPlacer;

        public BuildScreenTemplate(BuildMode gamemode, ProductionLine productionLine, BuildPlacer buildPlacer)
        {
            _gamemode = gamemode;
            _productionLine = productionLine;
            _buildPlacer = buildPlacer;
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
                new BuildMoveControlsMain()
                {
                    Position = new Rectangle(0, (int)(Vh*100 - 130), 100, 100)
                },
                // Bottom
                EvaluateProductionLine()
            )
            {
                //Background = new ImageBackgroundStyling
                //{
                //    ImageResource = "Starfield"
                //}
            };

        private IElement EvaluateProductionLineItem(Section section)
        {
            return new Pane(
                    new Text(section.Module.ShortName)
                    {
                        Position = new Rectangle(10, 10, 0, 0)
                    }
                    //new ModelView
                    //{
                    //    Renderable = new ShipSectionRenderable(new Point(0, 0))
                    //    {
                    //        Transform = new Transform()
                    //    },
                    //    Position = new Rectangle(0, 0, 240, 140)
                    //}
                )
            {
                Background = new ColourBackgroundStyling
                {
                    Colour = Color.DarkSlateBlue
                },
                Init = el => el.Events.Subscribe<ClickUIEvent>(ev =>
                {
                    _buildPlacer.Update(() => _buildPlacer.StartPlacing(section));
                })
            };
        }

        private IElement EvaluateProductionLine()
        {
            IList<IElement> sections = new List<IElement>();
            var i = 0;
            foreach (var section in _productionLine.Line)
            {
                var height = (int)(12 * Vh);
                var targetYPos = i * (height+10) + 5;
                //var smoothYPos = 0;
                //var fraction = _productionLine.SectionFractionalProgress;
                //if (i == _productionLine.Line.Count() - 1)
                //{
                //    smoothYPos = (int)(fraction * targetYPos + (1 - fraction) * 1800f);
                //} else
                //{
                //    Console.WriteLine(fraction);
                //    var targetYPosPrevious = (i+1) * 150 + 5;
                //    smoothYPos = (int)(fraction * targetYPos + (1 - fraction) * targetYPosPrevious);
                //}
                var sectionView = EvaluateProductionLineItem(section);
                sectionView.Position = new Rectangle(5, targetYPos, (int)(Vw * 20 - 10), height);
                sections.Add(sectionView);
                i++;
            }
            return new Pane(
                sections.ToArray()
            )
            {
                Position = new Rectangle((int)(Vw * 80), 0, (int)(Vw * 20), (int)(Vh*100)),
                Background = new ColourBackgroundStyling
                {
                    Colour = Color.SlateGray
                },
            };
        }

    }
}
