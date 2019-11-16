using Forge.Core.Components;
using Forge.Core.Rendering.Cameras;
using Forge.Core.Scenes;
using Forge.UI.Glass;
using GreatSpaceRace.Builder;
using GreatSpaceRace.Flight;
using GreatSpaceRace.Phases;
using GreatSpaceRace.Phases.Asteroids;
using GreatSpaceRace.Ships;
using GreatSpaceRace.UI.Flight;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Scenes
{
    public class FlightScene : Scene
    {
        private readonly ShipTopology _shipTopology;

        [Inject] CameraManager CameraManager { get; set; }
        [Inject] UserInterfaceManager UserInterfaceManager { get; set; }
        [Inject] GraphicsDevice GraphicsDevice { get; set; }

        public FlightScene(ShipTopology shipTopology)
        {
            _shipTopology = shipTopology;
        }

        public override void Initialise()
        {
            AddSingleton(new BackgroundScoll());

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
            shipEnt.Add(new FlightShip(_shipTopology));
            shipEnt.Add(new RocketControls(_shipTopology));
            camera.Add(new FlightCameraControl(shipEnt));

            var phaseEnt = Create();
            phaseEnt.Add(new PhaseManager(
                new Phase[] {
                    //Create().Add(new OpenPhase()),
                    Create().Add(new AsteroidPhase())
                }
            ));
            phaseEnt.Add(new PhaseTitleDisplay());

            var radarEnt = Create();
            radarEnt.Add(new RadarRenderer());

            var cleanBuildUI = UserInterfaceManager.Create(new FlightScreenTemplate(GraphicsDevice));
            Disposal += () => cleanBuildUI();
        }
    }
}
