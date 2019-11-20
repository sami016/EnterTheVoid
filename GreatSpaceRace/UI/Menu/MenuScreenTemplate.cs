using Forge.Core.Scenes;
using Forge.UI.Glass.Elements;
using Forge.UI.Glass.Interaction;
using Forge.UI.Glass.Stylings;
using Forge.UI.Glass.Templates;
using GreatSpaceRace.Constants;
using GreatSpaceRace.Scenes;
using GreatSpaceRace.Ships;
using GreatSpaceRace.Ships.Connections;
using GreatSpaceRace.Ships.Modules;
using GreatSpaceRace.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.UI.Menu
{

    class MenuScreenTemplate : Template
    {
        private readonly SceneManager _sceneManager;
        private readonly string _tip;

        public MenuScreenTemplate(SceneManager SceneManager)
        {
            _sceneManager = SceneManager;

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
                //new MenuButton("Hit Test")
                //{
                //    Position = new Rectangle(100, 500, 250, 150),
                //    Init = el => el.Events
                //        .Subscribe<ClickUIEvent>(ClickHitTest)
                //}
            )
            {
                Background = new ImageBackgroundStyling
                {
                    ImageResource = "Starfield"
                }
            };

        private void ClickHitTest(ClickUIEvent ev)
        {
            _sceneManager.SetScene(new HitTestScene());
        }

        public void ClickBuild(ClickUIEvent ev)
        {
            
            _sceneManager.SetScene(new BuildScene());
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
                new LifeSupportModule(),
                ConnectionLayouts.FullyConnected
            ));
            topology.SetSection(new Point(2, 1), new Section(
                new BlasterModule(),
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


            _sceneManager.SetScene(new FlightScene(topology));
        }
    }
}
