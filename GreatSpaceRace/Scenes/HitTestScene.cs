using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
using Forge.Core.Interfaces;
using Forge.Core.Rendering;
using Forge.Core.Rendering.Cameras;
using Forge.Core.Scenes;
using Forge.UI.Glass;
using GreatSpaceRace.Builder;
using GreatSpaceRace.Flight;
using GreatSpaceRace.Phases;
using GreatSpaceRace.Phases.Asteroids;
using GreatSpaceRace.Ships;
using GreatSpaceRace.Ships.Connections;
using GreatSpaceRace.Ships.Modules;
using GreatSpaceRace.UI.Flight;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Scenes
{
    public class HitTestScene : Scene
    {
        [Inject] CameraManager CameraManager { get; set; }
        [Inject] UserInterfaceManager UserInterfaceManager { get; set; }
        [Inject] GraphicsDevice GraphicsDevice { get; set; }

        public HitTestScene()
        {
        }

        public override void Initialise()
        {
            AddSingleton(new BackgroundScoll());

            var camera = Create();
            var cameraPos = camera.Add(new Transform()
            {
                Location = Vector3.Up*5+Vector3.Backward,
                Rotation = Quaternion.CreateFromYawPitchRoll(0, 0, (float)(Math.PI)),
            });
            //CameraManager.ActiveCamera = camera.Add(new Camera(new PerspectiveCameraParameters()));
            CameraManager.ActiveCamera = camera.Add(new Camera(new OrthographicCameraParameters(40)));

            CameraManager.ActiveCamera.LookAt(Vector3.Zero);
            //CameraManager.ActiveCamera.LookInDirection(Vector3.Forward);
            CameraManager.ActiveCamera.Recalculate();
            CameraManager.ActiveCamera.RecalculateParameters();

            AddSingleton(new FlightSpaces());

            //var shipEnt = Create();
            //shipEnt.Add(new Transform());
            //shipEnt.Add(new FlightShip(_shipTopology));
            //shipEnt.Add(new RocketControls(_shipTopology));

            var topology = new ShipTopology(6, 5);
            topology.Sections[2, 2] = new Section(
                new LifeSupportModule(),
                ConnectionLayouts.FullyConnected
            );

            this.EntityManager.Update(() =>
            {
                var asteroidEnt = Create();
                asteroidEnt.Add(new Transform()
                {
                    Location = new Vector3(0, 0, 0)
                });
                asteroidEnt.Add(new Asteroid());

                var shipEnt = Create();
                shipEnt.Add(new Transform());
                shipEnt.Add(new FlightShip(topology));
                shipEnt.Add(new RocketControls(topology));
                //camera.Add(new FlightCameraControl(shipEnt));
                // var asteroidEnt2 = Create();
                // asteroidEnt2.Add(new Transform());
                // asteroidEnt2.Add(new ShipSectionRenderer());
                // asteroidEnt2.Add(new TestShipSection());
                // asteroidEnt2.Add(new DebugCameraControl());
                // AddSingleton(new HitTest(asteroidEnt2, asteroidEnt));
            });

        }

    }

}
