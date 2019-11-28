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

    class WinScreenTemplate : Template
    {
        private readonly SceneManager _sceneManager;

        public WinScreenTemplate(SceneManager sceneManager)
        {
            _sceneManager = sceneManager;
        }

        public override IElement Evaluate() =>
            new Pane(
                new Pane(
                    new Text($"Congratulations. Throught great perseverence you have journeyed to the verge of the solar system. Thank you for playing!")
                    {
                        Position = new Rectangle(10, 10, 0, 0),
                        Font = "Default"
                    }
                )
                {
                    Position = new Rectangle(100, (int)(Vh * 50), GraphicsDevice.Viewport.Width - 200, (int)(Vh * 5)),
                },
                new MenuButton("Credits")//Campaign
                {
                    Position = new Rectangle(100, (int)(Vh * 60), GraphicsDevice.Viewport.Width - 200, (int)(Vh * 18)),
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


        public void ClickContinue(ClickUIEvent ev)
        {
            _sceneManager.SetScene(new MenuScene());
        }
    }
}
