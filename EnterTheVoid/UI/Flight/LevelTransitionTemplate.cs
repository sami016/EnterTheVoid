using EnterTheVoid.Constants;
using EnterTheVoid.Flight;
using EnterTheVoid.Phases;
using Forge.Core;
using Forge.Core.Rendering;
using Forge.UI.Glass.Elements;
using Forge.UI.Glass.Stylings;
using Forge.UI.Glass.Templates;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.UI.Flight
{
    public class LevelTransitionTemplate : Template
    {
        private readonly PhaseManager _phaseManager;
        private readonly ProgressTracker _progressTracker;
        private readonly Planet _planet;

        public LevelTransitionTemplate(PhaseManager phaseManager, Planet planet, ProgressTracker progressTracker)
        {
            _phaseManager = phaseManager;
            _progressTracker = progressTracker;
            _planet = planet;
        }

        public override void Tick(TickContext context)
        {
            base.Tick(context);
            Reevaluate();
        }

        public override IElement Evaluate()
        {
            return new Pane(
                _phaseManager.State == PhaseManagerState.LevelIntro ? new Pane(
                    new Text($"Level {(int)_planet} - {_planet} to {(Planet)(int)(_planet+1)}")
                    {
                        Font = "Title",
                        Position = new Rectangle((int)(Vw * 40), 50, 0, 0),
                        Center = true
                    },
                    new ModelView()
                    {
                        Renderable = _progressTracker,
                        Position = new Rectangle(0, 200, (int)(Vw * 60), 100),
                       // Position = new Rectangle((int)(Vw * 20), (int)(Vh * 100 - 100), (int)(Vw * 60), 100),
                        View = _progressTracker.View,
                        Projection = Matrix.CreateOrthographic(10f, 1f, 0.001f, 1000f)
                    }
                )
                {
                    Position = new Rectangle((int)(Vw * 20), (int)(Vh * 50) - 200, (int)(Vw * 60), 400),
                    Background = new ColourBackgroundStyling
                    {
                        Colour = new Color(45, 45, 45)
                    }
                } : new Pane()
                  
            );
        }
    }
}
