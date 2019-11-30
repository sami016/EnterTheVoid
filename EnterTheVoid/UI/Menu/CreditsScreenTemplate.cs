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

    class CreditScreenTemplate : Template
    {
        private readonly SceneManager _sceneManager;

        public CreditScreenTemplate(SceneManager sceneManager)
        {
            _sceneManager = sceneManager;
        }

        public override IElement Evaluate() =>
            new Pane(
                new Pane(
                    new Text("Enter The Void by Sam Holder")
                    {
                        Position = new Rectangle(10, 10, 0, 0),
                        Font = "Title"
                    },
                    new Text($"Design, Programming, 3D Modelling - Sam Holder")
                    {
                        Position = new Rectangle(10, 50, 0, 0),
                        Font = "Default"
                    },
                    new Text($"3D Modelling - Drew Van Schoonhoven")
                    {
                        Position = new Rectangle(10, 80, 0, 0),
                        Font = "Default"
                    }
                )
                {
                    Position = new Rectangle(100, (int)(Vh * 50), GraphicsDevice.Viewport.Width - 200, (int)(Vh * 5)),
                },
                new MenuButton("Continue")
                {
                    Position = new Rectangle(100, (int)(Vh * 80), GraphicsDevice.Viewport.Width - 200, (int)(Vh * 18)),
                    Init = el => el.Events
                        .Subscribe<ClickUIEvent>(ClickContinue)
                },
                new Pane()
                {
                    Background = new ImageBackgroundStyling
                    {
                        ImageResource = "Logo",
                    },
                    Position = new Rectangle(GraphicsDevice.Viewport.Width / 2 - 250, 100, 500, 300)
                }
            )
            {
                Background = new ImageBackgroundStyling
                {
                    ImageResource = "Starfield"
                }
            };


        public void ClickContinue(ClickUIEvent ev)
        {
            _sceneManager.SetScene(new MenuScene());
        }
    }
}
