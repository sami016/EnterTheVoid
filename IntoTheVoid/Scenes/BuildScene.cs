﻿using Forge.Core.Components;
using Forge.Core.Rendering.Cameras;
using Forge.Core.Scenes;
using Forge.Core.Sound;
using Forge.UI.Glass;
using IntoTheVoid.Builder;
using IntoTheVoid.Constants;
using IntoTheVoid.Ships;
using IntoTheVoid.Ships.Connections;
using IntoTheVoid.Ships.Modules;
using IntoTheVoid.UI.Builder;
using IntoTheVoid.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntoTheVoid.Scenes
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

            var floor = Create();
            floor.Add(new BuildFloor(_shipTopology));

            var productionLine = AddSingleton(new ProductionLine());
            var gameMode = AddSingleton(new BuildMode(_shipTopology));

            var placerEnt = Create();
            var placer = placerEnt.Add(new BuildPlacer(CameraManager.ActiveCamera, _shipTopology, productionLine));
            placerEnt.Add(new BuildPlacerCursor());

            var shipRenderer = AddSingleton(new ShipSectionRenderer());
            UserInterfaceManager.AddSceneUI(this, new BuildScreenTemplate(gameMode, _planet, productionLine, placer, shipRenderer));

            MusicManager.Start("Building");
        }
    }
}
