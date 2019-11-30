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

    class BeginScreenTemplate : Template
    {
        private readonly Action _actionStart;

        public BeginScreenTemplate(Action actionStart)
        {
            _actionStart = actionStart;
        }

        public override IElement Evaluate() =>
            new Pane(
                new Pane(
                        new Text($"You will construct your spaceship upon the hexagonal grid.")
                        {
                            Position = new Rectangle(10, 10, 0, 0),
                            Font = "Default"
                        },
                        new Text($"A production line of modules will appear on screen. Sections can be joined onto the existing ship by matching connectors.")
                        {
                            Position = new Rectangle(10, 60, 0, 0),
                            Font = "Default"
                        },
                        new Text($"Try to prioritise rockets for propulsion and guns for offense, before moving onto advaced modules such as shields and energy storage.")
                        {
                            Position = new Rectangle(10, 110, 0, 0),
                            Font = "Default"
                        },
                        new Text($"You will have just 80 seconds to construct your ship. Good luck!")
                        {
                            Position = new Rectangle(10, 160, 0, 0),
                            Font = "Default"
                        },
                        new MenuButton("Begin")
                        {
                            Position = new Rectangle(50, 250, (int)(Vw * 80) - 100, 160),
                            Init = el => el.Events
                                .Subscribe<ClickUIEvent>(ClickContinue)
                        }
                    )
                    {
                        Background = new ColourBackgroundStyling { Colour = new Color(45, 45, 45) },
                        Position = new Rectangle((int)(Vw * 10), 450, (int)(Vw * 80), 450)
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
                    //ImageResource = "Starfield"
                }
            };


        public void ClickContinue(ClickUIEvent ev)
        {
            _actionStart?.Invoke();
        }
    }
}
