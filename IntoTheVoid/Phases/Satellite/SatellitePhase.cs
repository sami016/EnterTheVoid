using Forge.Core.Components;
using Forge.Core.Scenes;
using IntoTheVoid.Constants;
using IntoTheVoid.Flight;
using IntoTheVoid.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntoTheVoid.Phases.Satellite
{
    public class SatellitePhase : Phase
    {
        [Inject] SceneManager SceneManager { get; set; }

        public SatellitePhase(Planet location)
        {
            Title = $"{location.ToString().ToUpper()}: Satellite Safehaven";
            Description = "Your ship will be fully repaired. You will have 30 seconds to modify your ship.";
        }

        public override void Start()
        {
            var ship = Entity.EntityManager.GetAll<FlightShip>().First();
            SceneManager.SetScene(new BuildScene(ship.Topology));
        }
    }
}
