using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Interfaces;
using Forge.Core.Rendering;
using Forge.Core.Rendering.Cameras;
using Forge.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.General
{
    public class ClusterExplosionEffect : Component, IInit, IRenderable, ITick
    {
        private static IList<Vector3> _componentDirections = new List<Vector3>();

        static ClusterExplosionEffect()
        {
            var random = new Random();
            for (var i = 0; i < 16; i++)
            {
                _componentDirections.Add(new Vector3(2 * (float)random.NextDouble() - 1, 2 * (float)random.NextDouble() - 1, 2 * (float)random.NextDouble() - 1));
            }
        }

        public uint RenderOrder { get; } = 120;
        public bool AutoRender { get; } = true;

        public float ScaleFactor { get; set; } = 1f;
        public float DistanceScaleFactor { get; set; } = 3f;
        public int Components { get; set; } = 3;


        private float _scale = 0.01f;
        private CompletionTimer _timer = new CompletionTimer(TimeSpan.FromMilliseconds(300));
        private Model _explosionModel;

        [Inject] CameraManager CameraManager { get; set; }
        [Inject] ContentManager Content { get; set; }
        [Inject] Transform Transform { get; set; }

        public void Render(RenderContext context)
        {
            context.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            var camera = CameraManager.ActiveCamera;
            var moveScale = _timer.CompletedFraction * DistanceScaleFactor;
            for (var i = 0; i < Components; i++)
            {
                _explosionModel.Draw(Matrix.CreateScale(_scale * ScaleFactor) * Matrix.CreateTranslation(_componentDirections[i] * moveScale) * Transform.WorldTransform, camera.View, camera.Projection);
            }
        }

        public void Tick(TickContext context)
        {
            _timer.Tick(context.DeltaTime);
            var fraction = _timer.CompletedFraction;
            if (fraction <= 0.3)
            {
                _scale = fraction / 0.3f;
            }
            else
            {
                _scale = (float)Math.Sin(((fraction - 0.3f) / 0.7f * (float)Math.PI / 2) + (float)Math.PI / 2);
            }
            if (_timer.Completed)
            {
                Entity.Delete();
            }
        }

        public void Initialise()
        {
            _explosionModel = Content.Load<Model>("Models/explosion");
            _explosionModel.EnableDefaultLighting();
            _explosionModel.SetDiffuseColour(Color.White);
            _explosionModel.ConfigureBasicEffects(x =>
            {
                x.Alpha = 0.5f;
            });
        }
    }
}
