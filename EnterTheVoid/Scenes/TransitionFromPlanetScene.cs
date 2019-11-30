using EnterTheVoid.Constants;
using EnterTheVoid.Flight;
using EnterTheVoid.General;
using EnterTheVoid.Ships;
using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Interfaces;
using Forge.Core.Rendering.Cameras;
using Forge.Core.Scenes;
using Forge.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnterTheVoid.Scenes
{
    public class TransitionFromPlanetScene : Scene
    {
        private readonly ShipTopology _shipTopology;
        private readonly Planet _planet;
        private readonly Action _runAfter;
        private readonly bool _exiting;

        [Inject] CameraManager CameraManager { get; set; }
        [Inject] GraphicsDevice GraphicsDevice { get; set; }

        public TransitionFromPlanetScene(ShipTopology shipTopology, Planet planet, Action runAfter, bool exiting)
        {
            _shipTopology = shipTopology;
            _planet = planet;
            _runAfter = runAfter;
            _exiting = exiting;
        }

        public override void Initialise()
        {
            AddSingleton(new SpaceBackgroundScroll());

            AddSingleton(new FlightSpaces());

            var camera = Create();
            var cameraPos = camera.Add(new Transform()
            {
                Location = Vector3.Backward * 10 + Vector3.Up * 25
            });
            //CameraManager.ActiveCamera = camera.Add(new Camera(new OrthographicCameraParameters(10)));
            CameraManager.ActiveCamera = camera.Add(new Camera(new PerspectiveCameraParameters()
            {
                AspectRatio = GraphicsDevice.Viewport.AspectRatio
            }));
            CameraManager.ActiveCamera.LookAt(Vector3.Zero);
            CameraManager.ActiveCamera.Recalculate();
            var planetEnt = Create();
            planetEnt.Add(new Transform
            {
                Location = Vector3.Forward * 100
            });
            planetEnt.Add(new PlanetRenderer()
            {
                Planet = _planet,
                Scale = 80f
            });

            Create()
                .Add(new FlightSpaces());

            var shipEnt = Create();
            shipEnt.Add(new Transform()
            {
                Location = (_exiting ? Vector3.Forward * 60 : Vector3.Forward * 25)
            });
            var flightShip = shipEnt.AddShipBasics(_shipTopology);
            flightShip.Rotation = _exiting ? (float)Math.PI : 0f;
            flightShip.Velocity = _exiting ? (Vector3.Backward * 15) : (Vector3.Forward * 15 + Vector3.Right);

            Create().Add(new TransitionFromPlanet(_runAfter, flightShip, CameraManager.ActiveCamera));
        }

        public class TransitionFromPlanet : Component, ITick
        {
            private readonly FlightShip _flightShip;
            private readonly Action _runAfter;
            private readonly Camera _camera;
            private CompletionTimer _nextTimer = new CompletionTimer(TimeSpan.FromSeconds(4));
            private bool _finished = false;

            public TransitionFromPlanet(Action runAfter, FlightShip flightShip, Camera camera)
            {
                _flightShip = flightShip;
                _runAfter = runAfter;
                _camera = camera;
            }

            public void Tick(TickContext context)
            {
                _nextTimer.Tick(context.DeltaTime);
                if (!_finished && _nextTimer.Completed)
                {
                    _finished = true;
                    _runAfter?.Invoke();
                }

                _camera.LookAt(_flightShip.GetNodes().First().GlobalLocation);
                _camera.Recalculate();
            }
        }
    }
}
