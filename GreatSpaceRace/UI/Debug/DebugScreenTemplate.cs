using Forge.Core.Scenes;
using Forge.UI.Glass.Elements;
using Forge.UI.Glass.Interaction;
using Forge.UI.Glass.Stylings;
using Forge.UI.Glass.Templates;
using GreatSpaceRace.Scenes;
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
                //new Text("The Great Space Race"),
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
        }
    }
}
