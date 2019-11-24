using Forge.Core.Components;
using Forge.Core.Rendering.Cameras;
using Forge.Core.Scenes;
using Forge.UI.Glass;
using IntoTheVoid.Builder;
using IntoTheVoid.Constants;
using IntoTheVoid.Flight;
using IntoTheVoid.Phases;
using IntoTheVoid.Phases.Asteroids;
using IntoTheVoid.Phases.Combat;
using IntoTheVoid.Phases.Open;
using IntoTheVoid.Phases.Satellite;
using IntoTheVoid.Phases.Transmission;
using IntoTheVoid.Ships;
using IntoTheVoid.UI.Flight;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntoTheVoid.Scenes
{
    public class FlightScene : Scene
    {
        private readonly ShipTopology _shipTopology;
        private readonly IEnumerable<Func<Phase>> _phaseFactories;

        [Inject] CameraManager CameraManager { get; set; }
        [Inject] UserInterfaceManager UserInterfaceManager { get; set; }
        [Inject] GraphicsDevice GraphicsDevice { get; set; }

        public FlightScene(ShipTopology shipTopology, IEnumerable<Func<Phase>> phaseFactories)
        {
            _shipTopology = shipTopology;
            _phaseFactories = phaseFactories;
        }

        public override void Initialise()
        {
            AddSingleton(new SpaceBackgroundScroll());

            var camera = Create();
            var cameraPos = camera.Add(new Transform()
            {
                Rotation = Quaternion.CreateFromYawPitchRoll(0, 0, (float)(Math.PI)),
            });
            //CameraManager.ActiveCamera = camera.Add(new Camera(new PerspectiveCameraParameters()));
            CameraManager.ActiveCamera = camera.Add(new Camera(new OrthographicCameraParameters(40)));
            CameraManager.ActiveCamera.Recalculate();

            AddSingleton(new FlightSpaces());

            var shipEnt = Create();
            shipEnt.Add(new Transform());
            var flightShip = shipEnt.AddShipBasics(_shipTopology);
            shipEnt.Add(new RocketControls());
            shipEnt.Add(new CombatControls());
            shipEnt.Add(new ShipVelocityLimiter());

            camera.Add(new FlightCameraControl(shipEnt));

            var phaseEnt = Create();
            var phases = _phaseFactories
                .Select(x => Create().Add(x()))
                .ToArray();
            phaseEnt.Add(new PhaseManager(
                phases
            ));
            phaseEnt.Add(new PhaseTitleDisplay());

            //var radarEnt = Create();
            //radarEnt.Add(new RadarRenderer());

            var cleanBuildUI = UserInterfaceManager.Create(new FlightScreenTemplate(GraphicsDevice, flightShip));
            Disposal += () => cleanBuildUI();
        }
    }
}
