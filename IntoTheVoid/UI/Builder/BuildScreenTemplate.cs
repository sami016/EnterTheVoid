using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Scenes;
using Forge.UI.Glass.Elements;
using Forge.UI.Glass.Interaction;
using Forge.UI.Glass.Stylings;
using Forge.UI.Glass.Templates;
using IntoTheVoid.Builder;
using IntoTheVoid.Scenes;
using IntoTheVoid.Ships;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forge.Core.Engine;
using IntoTheVoid.UI.Flight;

namespace IntoTheVoid.UI.Builder
{

    class BuildScreenTemplate : Template
    {
        private readonly ShipSectionRenderer _shipSectionRenderer;
        private readonly BuildMode _gamemode;
        private readonly ProductionLine _productionLine;
        private readonly BuildPlacer _buildPlacer;

        private int _refreshCount = 0;

        public BuildScreenTemplate(BuildMode gamemode, ProductionLine productionLine, BuildPlacer buildPlacer, ShipSectionRenderer shipSectionRenderer)
        {
            _shipSectionRenderer = shipSectionRenderer;
            _gamemode = gamemode;
            _productionLine = productionLine;
            _buildPlacer = buildPlacer;
        }

        public override void Tick(TickContext context)
        {
            base.Tick(context);
            _refreshCount++;
            if (_refreshCount % 1000 == 0)
            {
                _modelViews.Clear();
            }
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

        private IDictionary<Section, ModelView> _modelViews = new Dictionary<Section, ModelView>();

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
                        Colour = new Color(45, 45, 45)
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

        private IElement EvaluateProductionLineItem(Section section, Rectangle position)
        {
            if (!_modelViews.ContainsKey(section))
            {
                var ratio = position.Height / (float)position.Width;
                _modelViews[section] = new ModelView
                {
                    RenderFunc = ctx => _shipSectionRenderer.Render(ctx, Matrix.Identity, section),
                    Position = new Rectangle(0, 30, (int)(Vw * 20 - 10), (int)(12 * Vh - 30)),
                    View = Matrix.CreateLookAt(new Vector3(3f, 5f, 3f), Vector3.Zero, Vector3.Up),
                    Projection = Matrix.CreateOrthographicOffCenter(-2, 2, -2 * ratio, 2 * ratio, 0.001f, 10000f)
                };
            }
            return new Pane(
                    new Text(section.Module.ShortName)
                    {
                        Position = new Rectangle(10, 10, 0, 0),
                        Colour = new Color(230, 230, 230)
                    },
                    _modelViews[section]
                )
            {
                Background = new ColourBackgroundStyling
                {
                    Colour = new Color(45, 45, 45)
                },
                Init = el => el.Events.Subscribe<ClickUIEvent>(ev =>
                {
                    _buildPlacer.Update(() => _buildPlacer.StartPlacing(section));
                }),
                Position = position
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
                var sectionView = EvaluateProductionLineItem(section, new Rectangle(5, targetYPos, (int)(Vw * 20 - 10), height));
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
                    Colour = new Color(70, 70, 70)
                },
            };
        }

    }
}
