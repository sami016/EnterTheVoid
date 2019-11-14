using Forge.Core.Scenes;
using Forge.UI.Glass.Elements;
using Forge.UI.Glass.Interaction;
using Forge.UI.Glass.Stylings;
using Forge.UI.Glass.Templates;
using GreatSpaceRace.Scenes;
using GreatSpaceRace.Ships;
using GreatSpaceRace.Ships.Connections;
using GreatSpaceRace.Ships.Modules;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.UI.Debug
{

    class DebugScreenTemplate : Template
    {
        private readonly SceneManager _sceneManager;

        public DebugScreenTemplate(SceneManager SceneManager)
        {
            _sceneManager = SceneManager;
        }

        public override IElement Evaluate() =>
            new Pane(
                new Text("The Great Space Race")
                {
                    Position = new Rectangle(25, 25, 0, 0),
                    Font = "Title"
                },
                new MenuButton("Build Mode")
                {
                    Position = new Rectangle(100, 100, 250, 150),
                    Init = el => el.Events
                        .Subscribe<ClickUIEvent>(ClickBuild)
                },
                new MenuButton("Flight Mode")
                {
                    Position = new Rectangle(200, 300, 250, 150),
                    Init = el => el.Events
                        .Subscribe<ClickUIEvent>(ClickFlight)
                }
            )
            {
                Background = new ImageBackgroundStyling
                {
                    ImageResource = "Starfield"
                }
            };

        public void ClickBuild(ClickUIEvent ev)
        {
            System.Diagnostics.Debug.WriteLine("Build clicked");
            
            _sceneManager.SetScene(new BuildScene());
        }

        public void ClickFlight(ClickUIEvent ev)
        {
            System.Diagnostics.Debug.WriteLine("Flight clicked");

            var topology = new ShipTopology(6, 5);
            topology.Sections[2, 2] = new Section(
                new LifeSupportModule(),
                ConnectionLayouts.FullyConnected
            );
            topology.Sections[2, 1] = new Section(
                new BlasterModule(),
                ConnectionLayouts.FullyConnected,
                4
            );
            topology.Sections[2, 3] = new Section(
                new RocketModule(),
                ConnectionLayouts.FullyConnected,
                1
            );
            //topology.Sections[3, 2] = new Section(
            //    new RotaryEngine(),
            //    ConnectionLayouts.FullyConnected,
            //    1
            //);
            //topology.Sections[4, 2] = new Section(
            //    new RotaryEngine(),
            //    ConnectionLayouts.FullyConnected,
            //    1
            //);
            //topology.Sections[5, 2] = new Section(
            //    new RotaryEngine(),
            //    ConnectionLayouts.FullyConnected,
            //    1
            //);


            _sceneManager.SetScene(new FlightScene(topology));
        }
    }
}
