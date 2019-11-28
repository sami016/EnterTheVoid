using EnterTheVoid.General;
using EnterTheVoid.Scenes;
using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Interfaces;
using Forge.Core.Scenes;
using Forge.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.Flight
{
    public class PlayerDeathDetector : Component, ITick
    {
        [Inject] FlightShip FlightShip { get; set; }
        [Inject] SceneManager SceneManager { get; set; }
        [Inject] FadeTransition FadeTransition { get; set; }

        private readonly CompletionTimer _completionTimer = new CompletionTimer(TimeSpan.FromSeconds(5));

        public void Tick(TickContext context)
        {
            if (FlightShip.Health < 1)
            {
                _completionTimer.Tick(context.DeltaTime);
                if (_completionTimer.Completed)
                {
                    FadeTransition.StartTransition(() => SceneManager.SetScene(new DeathScene()));
                }
            }
        }
    }
}
