using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Interfaces;
using Forge.Core.Utilities;
using GreatSpaceRace.Flight;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Phases.Open
{
    public class OpenPhaseControl : Component, ITick
    {
        private CompletionTimer _repairCompletionTimer = new CompletionTimer(TimeSpan.FromSeconds(2));
        
        [Inject] FlightShip FlightShip { get; set; }

        public void Tick(TickContext context)
        {
            var keys = Keyboard.GetState();

            // repair.
            if (keys.IsKeyDown(Keys.R))
            {
                _repairCompletionTimer.Tick(context.DeltaTime);
                if (_repairCompletionTimer.Completed)
                {
                    _repairCompletionTimer.Restart();
                    FlightShip.Repair(1);
                }
            }
        }
    }
}
