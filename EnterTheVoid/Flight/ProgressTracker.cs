using EnterTheVoid.Constants;
using EnterTheVoid.General;
using EnterTheVoid.Orchestration;
using Forge.Core.Components;
using Forge.Core.Interfaces;
using Forge.Core.Rendering;
using Forge.Core.Rendering.Cameras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Forge.Core.Utilities;

namespace EnterTheVoid.Flight
{
    class ProgressTracker : Component, IInit, IRenderable
    {
        private Camera _planetCamera;
        private IList<IRenderable> _planetRenderables = new List<IRenderable>();
        private Model _arrowModel;

        [Inject] Orchestrator Orchestrator { get; set; }
        [Inject] ContentManager Content { get; set; }

        public Matrix View => _planetCamera.View;
        public Matrix Projection => _planetCamera.Projection;

        public uint RenderOrder { get; } = 0;

        public bool AutoRender { get; } = false;

        public void Initialise()
        {
            _arrowModel = Content.Load<Model>("Models/Arrow");
            _arrowModel.SetDiffuseColour(Color.White);
            var planetCameraEnt = Entity.Create();
            planetCameraEnt.Add(new Transform()
            {
                Rotation = Quaternion.CreateFromYawPitchRoll(0, 0, (float)(Math.PI)),
                Location = (Vector3.Backward) * 10
            });
            _planetCamera = planetCameraEnt.Add(new Camera(new OrthographicCameraParameters()));
            _planetCamera.LookAt(Vector3.Zero);
            _planetCamera.Recalculate();

            int i = 0;
            for (var planet = 1; planet <= 8; planet++)
            {
                if (planet == 0) continue;
                var planetRendererEnt = Entity.Create();
                planetRendererEnt.Add(new Transform
                {
                    Location = new Vector3(1f * i - 4f, 0, 0)
                });
                _planetRenderables.Add(planetRendererEnt.Add(new PlanetRenderer()
                {
                    Planet = (Planet)i,
                    AutoRender = false
                }));
                i++;
            }
        }

        public void Render(RenderContext context)
        {
            foreach (var planet in _planetRenderables)
            {
                planet.Render(context);
            }

            var i = (int)Orchestrator.CurrentPlanet;
            _arrowModel.Draw(Matrix.CreateScale(0.05f) * Matrix.CreateRotationZ(-(float)Math.PI / 2) *  Matrix.CreateTranslation(1f * i - 4f + 0.5f, 0, 0), context.View.Value, context.Projection.Value);
        }
    }
}
