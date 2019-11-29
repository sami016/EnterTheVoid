using Forge.Core.Components;
using Forge.Core.Rendering.Cameras;
using Forge.Core.Scenes;
using Forge.Core.Sound;
using Forge.UI.Glass;
using EnterTheVoid.Builder;
using EnterTheVoid.Constants;
using EnterTheVoid.Ships;
using EnterTheVoid.Ships.Connections;
using EnterTheVoid.Ships.Modules;
using EnterTheVoid.UI.Builder;
using EnterTheVoid.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using EnterTheVoid.Flight;
using EnterTheVoid.General;
using EnterTheVoid.UI.Menu;
using Forge.Core.Engine;

namespace EnterTheVoid.Scenes
{
    public class BuildScene : Scene
    {

        private ShipTopology _shipTopology;
        private readonly Planet _planet;

        [Inject] CameraManager CameraManager { get; set; }
        [Inject] UserInterfaceManager UserInterfaceManager { get; set; }
        [Inject] GraphicsDevice GraphicsDevice { get; set; }
        [Inject] MusicManager MusicManager { get; set; }

        public BuildScene(ShipTopology shipTopology, Planet planet)
        {
            _shipTopology = shipTopology;
            _planet = planet;
        }

        public override void Initialise()
        {
            AddSingleton(new SpaceBackgroundScroll());

            var focusLocation = HexagonHelpers.GetGridWorldPosition(new Point(2, 2));

            var camera = Create();
            var cameraPos = camera.Add(new Transform()
            {
                Rotation = Quaternion.CreateFromYawPitchRoll(0, 0, (float)(Math.PI)),
                Location = focusLocation + (Vector3.Backward + Vector3.Up) * 10
            });
            //CameraManager.ActiveCamera = camera.Add(new Camera(new OrthographicCameraParameters(10)));
            CameraManager.ActiveCamera = camera.Add(new Camera(new PerspectiveCameraParameters()));
            CameraManager.ActiveCamera.LookAt(focusLocation);
            CameraManager.ActiveCamera.Recalculate();
            camera.Add(new BuildCameraControl());

            // Background planet.
            var planetEnt = Create();
            planetEnt.Add(new Transform()
            {
                Location = cameraPos.Location + CameraManager.ActiveCamera.Forwards * 100 + Vector3.Forward * 50
            });
            var planetRenderer = planetEnt.Add(new PlanetRenderer()
            {
                AutoRender = true,
                Planet = _planet,
                Scale = 80f,
                RenderOrder = 20
            });

            var floor = Create();
            floor.Add(new BuildFloor(_shipTopology));

            var productionLine = AddSingleton(new ProductionLine());
            var gameMode = AddSingleton(new BuildMode(_shipTopology));

            var placerEnt = Create();
            var placer = placerEnt.Add(new BuildPlacer(CameraManager.ActiveCamera, _shipTopology, productionLine));
            placerEnt.Add(new BuildPlacerCursor());

            var shipRenderer = AddSingleton(new ShipSectionRenderer());
            UserInterfaceManager.AddSceneUI(this, new BuildScreenTemplate(gameMode, _planet, productionLine, placer, shipRenderer));
            if (_planet == Planet.Earth)
            {
                UIDispose closeBeginScreen = null;
                closeBeginScreen = UserInterfaceManager.Create(new BeginScreenTemplate(() => {
                    gameMode.Start();
                    EntityManager.Update(() =>
                    {
                        closeBeginScreen?.Invoke();
                    });
                }));
            } else
            {
                gameMode.Start();
            }

            MusicManager.Start("Building");
        }
    }
}
