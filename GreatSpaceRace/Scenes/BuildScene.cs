﻿using Forge.Core.Components;
using Forge.Core.Rendering.Cameras;
using Forge.Core.Scenes;
using Forge.Core.Sound;
using Forge.UI.Glass;
using GreatSpaceRace.Builder;
using GreatSpaceRace.Ships;
using GreatSpaceRace.Ships.Connections;
using GreatSpaceRace.Ships.Modules;
using GreatSpaceRace.UI.Builder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Scenes
{
    public class BuildScene : Scene
    {

        private ShipTopology _shipTopology;

        [Inject] CameraManager CameraManager { get; set; }
        [Inject] UserInterfaceManager UserInterfaceManager { get; set; }
        [Inject] GraphicsDevice GraphicsDevice { get; set; }
        [Inject] MusicManager MusicManager { get; set; }

        public override void Initialise()
        {
            _shipTopology = new ShipTopology(6, 5);
            _shipTopology.Sections[2, 2] = new Section(
                new LifeSupportModule(),
                ConnectionLayouts.FullyConnected
            );

            var camera = Create();
            var cameraPos = camera.Add(new Transform()
            {
                Rotation = Quaternion.CreateFromYawPitchRoll(0, 0, (float)(Math.PI)),
            });
            //CameraManager.ActiveCamera = camera.Add(new Camera(new OrthographicCameraParameters(10)));
            CameraManager.ActiveCamera = camera.Add(new Camera(new PerspectiveCameraParameters()));
            cameraPos.Location = (Vector3.Backward + Vector3.Up) * 5;
            CameraManager.ActiveCamera.LookAt(Vector3.Zero);
            CameraManager.ActiveCamera.Recalculate();
            camera.Add(new DebugCameraControl());

            var floor = Create();
            floor.Add(new BuildFloor(_shipTopology));

            var productionLine = AddSingleton(new ProductionLine());
            var gameMode = AddSingleton(new BuildMode());

            var placerEnt = Create();
            var placer = placerEnt.Add(new BuildPlacer(CameraManager.ActiveCamera, _shipTopology, productionLine));
            placerEnt.Add(new BuildPlacerCursor());

            var cleanBuildUI = UserInterfaceManager.Create(new BuildScreenTemplate(gameMode, productionLine, placer));
            Disposal += () => cleanBuildUI();

            MusicManager.Start("Building");
        }
    }
}
