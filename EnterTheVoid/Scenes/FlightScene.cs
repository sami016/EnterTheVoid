using Forge.Core.Components;
using Forge.Core.Rendering.Cameras;
using Forge.Core.Scenes;
using Forge.UI.Glass;
using EnterTheVoid.Builder;
using EnterTheVoid.Constants;
using EnterTheVoid.Flight;
using EnterTheVoid.Phases;
using EnterTheVoid.Phases.Asteroids;
using EnterTheVoid.Phases.Combat;
using EnterTheVoid.Phases.Open;
using EnterTheVoid.Phases.Satellite;
using EnterTheVoid.Phases.Transmission;
using EnterTheVoid.Ships;
using EnterTheVoid.UI.Flight;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnterTheVoid.General;
using EnterTheVoid.UI.Menu;

namespace EnterTheVoid.Scenes
{
    public class FlightScene : Scene
    {
        private readonly Planet _planet;
        private readonly ShipTopology _shipTopology;
        private readonly IEnumerable<Func<Phase>> _phaseFactories;

        [Inject] CameraManager CameraManager { get; set; }
        [Inject] UserInterfaceManager UserInterfaceManager { get; set; }
        [Inject] GraphicsDevice GraphicsDevice { get; set; }

        public FlightScene(Planet planet, ShipTopology shipTopology, IEnumerable<Func<Phase>> phaseFactories)
        {
            _planet = planet;
            _shipTopology = shipTopology;
            _phaseFactories = phaseFactories;
        }

        public override void Initialise()
        {
            var camera = Create();
            var cameraPos = camera.Add(new Transform()
            {
                Rotation = Quaternion.CreateFromYawPitchRoll(0, 0, (float)(Math.PI)),
            });
            //CameraManager.ActiveCamera = camera.Add(new Camera(new PerspectiveCameraParameters()));
            CameraManager.ActiveCamera = camera.Add(new Camera(new OrthographicCameraParameters(40)));
            CameraManager.ActiveCamera.Recalculate();


            AddSingleton(new SpaceBackgroundScroll(cameraPos));

            AddSingleton(new FlightSpaces());

            var shipEnt = Create();
            shipEnt.Add(new Transform());
            var flightShip = shipEnt.AddShipBasics(_shipTopology);
            shipEnt.Add(new RocketControls());
            shipEnt.Add(new CombatControls());
            shipEnt.Add(new ShipVelocityLimiter());
            shipEnt.Add(new PlayerDeathDetector());

            camera.Add(new FlightCameraControl(shipEnt));

            var phaseEnt = Create();
            var phases = _phaseFactories
                .Select(x => Create().Add(x()))
                .ToArray();
            var phaseManager = phaseEnt.Add(new PhaseManager(
                phases
            ));
            phaseEnt.Add(new PhaseTitleDisplay());

            var progressTracker = Create().Add(new ProgressTracker());
            UserInterfaceManager.AddSceneUI(this, new FlightScreenTemplate(GraphicsDevice, flightShip, progressTracker));
            UserInterfaceManager.AddSceneUI(this, new LevelTransitionTemplate(phaseManager, _planet, progressTracker));
        }
    }
}
