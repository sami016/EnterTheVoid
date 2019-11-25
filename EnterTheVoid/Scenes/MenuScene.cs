using Forge.Core.Components;
using Forge.Core.Scenes;
using Forge.Core.Sound;
using Forge.UI.Glass;
using EnterTheVoid.UI.Menu;
using System;
using System.Collections.Generic;
using System.Text;
using EnterTheVoid.Menu;
using Microsoft.Xna.Framework;
using Forge.Core.Rendering.Cameras;
using EnterTheVoid.Constants;

namespace EnterTheVoid.Scenes
{
    public class MenuScene : Scene
    {

        [Inject] UserInterfaceManager UserInterfaceManager { get; set; }
        [Inject] MusicManager MusicManager { get; set; }
        [Inject] SceneManager SceneManager { get; set; }
        [Inject] CameraManager CameraManager { get; set; }

        public override void Initialise()
        {
            var camera = Create();
            var cameraPos = camera.Add(new Transform()
            {
                Rotation = Quaternion.CreateFromYawPitchRoll(0, 0, (float)(Math.PI)),
                Location = (Vector3.Backward) * 10
            });
            CameraManager.ActiveCamera = camera.Add(new Camera(new OrthographicCameraParameters(10)));
            //CameraManager.ActiveCamera = camera.Add(new Camera(new PerspectiveCameraParameters()));
            CameraManager.ActiveCamera.LookAt(Vector3.Zero);
            CameraManager.ActiveCamera.Recalculate();

            int i = 0;
            for (var planet = 1; planet <=8; planet++)
            {
                if (planet == 0) continue;
                var planetRendererEnt = Create();
                planetRendererEnt.Add(new Transform
                {
                    Location = new Vector3(1f * i - 4f, 0, 0)
                });
                planetRendererEnt.Add(new PlanetRenderer()
                {
                    Planet = (Planet)i
                });
                i++;
            }
            UserInterfaceManager.AddSceneUI(this, new MenuScreenTemplate(this));

            MusicManager.Start("Menu");
        }
    }
}
