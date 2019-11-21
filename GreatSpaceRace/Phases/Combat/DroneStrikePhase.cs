using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Phases.Combat
{
    public class DroneStrikePhase : Phase
    {
        public DroneStrikePhase()
        {
            Title = "Drone Zone";
            Description = "Combat warning. Fend off incoming attack drones.";
            CompleteMessage = "Combat completed.";
        }

        public override void Start()
        {
        }

        public override void Stop()
        {
        }
    }
}
