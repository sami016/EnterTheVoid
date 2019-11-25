using Forge.Core.Components;
using Forge.Core.Scenes;
using EnterTheVoid.Constants;
using EnterTheVoid.Flight;
using EnterTheVoid.Orchestration;
using EnterTheVoid.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnterTheVoid.Phases.Satellite
{
    public class SatellitePhase : Phase
    {
        [Inject] Orchestrator Orchestrator { get; set; }

        public SatellitePhase(Planet location)
        {
            Title = $"{location.ToString().ToUpper()}: Satellite Safehaven";
            Description = "Your ship will be fully repaired. You will have 30 seconds to modify your ship.";
        }

        public override void Start()
        {
            Orchestrator.NextBuild();
        }
    }
}
