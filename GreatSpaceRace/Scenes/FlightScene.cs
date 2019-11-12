using Forge.Core.Components;
using Forge.Core.Rendering.Cameras;
using Forge.Core.Scenes;
using GreatSpaceRace.Builder;
using GreatSpaceRace.Flight;
using GreatSpaceRace.Ships;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Scenes
{
    public class FlightScene : Scene
    {
        private readonly ShipTopology _shipTopology;

        [Inject] CameraManager CameraManager { get; set; }

        public FlightScene(ShipTopology shipTopology)
        {
            _shipTopology = shipTopology;
        }

        public override void Initialise()
        {
            var camera = Create();
            var cameraPos = camera.Add(new Transform()
            {
                Rotation = Quaternion.CreateFromYawPitchRoll(0, 0, (float)(Math.PI)),
            });
            CameraManager.ActiveCamera = camera.Add(new Camera(new PerspectiveCameraParameters()));
            CameraManager.ActiveCamera.Recalculate();

            var shipEnt = Create();
            shipEnt.Add(new Transform());
            shipEnt.Add(new FlightShip(_shipTopology));

            camera.Add(new FlightCameraControl(shipEnt));
        }
    }
}
