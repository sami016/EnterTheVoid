using Forge.Core.Components;
using Forge.Core.Rendering.Cameras;
using Forge.Core.Scenes;
using Forge.UI.Glass;
using GreatSpaceRace.Builder;
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
        [Inject] CameraManager CameraManager { get; set; }
        [Inject] UserInterfaceManager UserInterfaceManager { get; set; }
        [Inject] GraphicsDevice GraphicsDevice { get; set; }

        public override void Initialise()
        {
            var camera = Create();
            var cameraPos = camera.Add(new Transform()
            {
                Rotation = Quaternion.CreateFromYawPitchRoll(0, 0, (float)(Math.PI)),
            });
            CameraManager.ActiveCamera = camera.Add(new Camera(new OrthographicCameraParameters(10)));
            //CameraManager.ActiveCamera = camera.Add(new Camera(new PerspectiveCameraParameters()));
            cameraPos.Location = (Vector3.Backward + Vector3.Up) * 50;
            CameraManager.ActiveCamera.LookAt(Vector3.Zero);
            CameraManager.ActiveCamera.Recalculate();
            camera.Add(new DebugCameraControl());

            var floor = Create();
            floor.Add(new BuildFloor());

            var gameMode = AddSingleton(new BuildMode());

            var cleanBuildUI = UserInterfaceManager.Create(new BuildScreenTemplate(gameMode));
            Disposal += () => cleanBuildUI();
        }
    }
}
