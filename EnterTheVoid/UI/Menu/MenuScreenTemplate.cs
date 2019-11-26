using Forge.Core.Scenes;
using Forge.UI.Glass.Elements;
using Forge.UI.Glass.Interaction;
using Forge.UI.Glass.Stylings;
using Forge.UI.Glass.Templates;
using EnterTheVoid.Constants;
using EnterTheVoid.Orchestration;
using EnterTheVoid.Scenes;
using EnterTheVoid.Ships;
using EnterTheVoid.Ships.Connections;
using EnterTheVoid.Ships.Modules;
using EnterTheVoid.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using EnterTheVoid.General;
using EnterTheVoid.Upgrades;

namespace EnterTheVoid.UI.Menu
{

    class MenuScreenTemplate : Template
    {
        private readonly MenuScene _menuScene;
        private readonly string _tip;

        public MenuScreenTemplate(MenuScene menuScene)
        {
            _menuScene = menuScene;

            _tip = Tips.Sample();
        }

        public override IElement Evaluate() =>
            new Pane(
                //new Pane(
                //new Text($"Tip: {_tip}")
                //{
                //    Position = new Rectangle(10, 10, 0, 0),
                //    Font = "Default"
                //}
                //)
                //{
                //    Position = new Rectangle(100, (int)(Vh * 50), GraphicsDevice.Viewport.Width - 200, (int)(Vh * 5)),
                //},
                new MenuButton("Play")//Campaign
                {
                    Position = new Rectangle(100, (int)(Vh * 60), GraphicsDevice.Viewport.Width - 200, (int)(Vh * 18)),
                    Init = el => el.Events
                        .Subscribe<ClickUIEvent>(ClickBuild)
                },
                new MenuButton("Start with pre-built ship")//Multiplayer
                {
                    Position = new Rectangle(100, (int)(Vh * 80), GraphicsDevice.Viewport.Width - 200, (int)(Vh * 18)),
                    Init = el => el.Events
                        .Subscribe<ClickUIEvent>(ClickFlight)
                },
                new Pane()
                {
                    Background = new ImageBackgroundStyling
                    {
                        ImageResource = "Logo",
                    },
                    Position = new Rectangle(GraphicsDevice.Viewport.Width / 2 - 250, 100, 500, 300)
                }
                //new ModelView()
                //{
                //    RenderFunc = ctx => _planetRenderer.Render(ctx),
                //    Position = new Rectangle(0, 0, 100, 100),
                //    View = Matrix.CreateLookAt(Vector3.Backward * 4, Vector3.Zero, Vector3.Up),
                //    Projection = Matrix.CreatePerspective(100, 100, 0.001f, 10000f)
                //}
            )
            {
                Background = new ImageBackgroundStyling
                {
                    ImageResource = "Starfield"
                }
            };


        public void ClickBuild(ClickUIEvent ev)
        {
            var orchestrator = _menuScene.Create(false).Add(new Orchestrator());
            orchestrator.NextBuild();
        }

        public void ClickFlight(ClickUIEvent ev)
        {
            var topology = new ShipTopology(6, 5);

            //for (var i=0; i < 6; i++)
            //{
            //    for (var j=0; j<5; j++)
            //    {
            //        topology.SetSection(new Point(i, j] = new Section(
            //            new RocketModule(),
            //            ConnectionLayouts.FullyConnected,
            //            1
            //        );
            //    }
            //}
            topology.SetSection(new Point(2, 2), new Section(
                new ResearchCenterModule(),
                ConnectionLayouts.FullyConnected
            ));
            topology.SetSection(new Point(2, 1), new Section(
                new BlasterModule(),
                ConnectionLayouts.FullyConnected,
                4
            ));
            topology.SetSection(new Point(3, 1), new Section(
                new BombardModule(),
                ConnectionLayouts.FullyConnected,
                4
            ));
            topology.SetSection(new Point(2, 3), new Section(
                new RocketModule(),
                ConnectionLayouts.FullyConnected,
                1
            ));
            topology.SetSection(new Point(1, 3), new Section(
                new RocketModule(),
                ConnectionLayouts.FullyConnected,
                2
            ));
            topology.SetSection(new Point(3, 3), new Section(
                new RocketModule(),
                ConnectionLayouts.FullyConnected,
                OffDirection.SouthEast
            ));
            topology.SetSection(new Point(3, 2), new Section(
                new RotaryEngine(),
                ConnectionLayouts.FullyConnected,
                1
            ));
            topology.SetSection(new Point(4, 2), new Section(
                new RotaryEngine(),
                ConnectionLayouts.FullyConnected,
                1
            ));
            topology.SetSection(new Point(5, 2), new Section(
                new RotaryEngine(),
                ConnectionLayouts.FullyConnected,
                1
            ));
            topology.ApplyUpgrade(new BombardOverload());
            var orchestrator = _menuScene.Create(false).Add(new Orchestrator(topology));
            orchestrator.CurrentPlanet = Planet.Earth;
            orchestrator.NextFlight();

            //_menuScene.SetScene(new FlightScene(topology));
        }
    }
}
