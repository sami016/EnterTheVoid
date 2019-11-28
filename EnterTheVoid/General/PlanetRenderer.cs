using EnterTheVoid.Constants;
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
    public class PlanetRenderer : Component, IInit, IRenderable, ITick
    {
        private Effect _effect;
        private Model _earthModel;
        private Model _marsModel;
        private Model _juptierModel;
        private Model _saturnModel;
        private Model _uranusModel;
        private Model _neptuneModel;
        private Model _plutoModel;

        public uint RenderOrder { get; set; } = 200;

        public bool AutoRender { get; set; } = true;

        [Inject] ContentManager Content { get; set; }
        [Inject] CameraManager CameraManager { get; set; }
        [Inject] Transform Transform { get; set; }
        public Planet Planet { get; set; } = Planet.Earth;
        public Camera Camera { get; set; }

        private Matrix _transform = Matrix.CreateScale(0.2f) * Matrix.CreateRotationZ(0.5f);

        public float Scale { get; set; } = 1f;
        public void Initialise()
        {
            //_effect = Content.Load<Effect>("Smooth");

            _earthModel = Content.Load<Model>("Models/earth");
            _earthModel.EnableDefaultLighting();
            _marsModel = Content.Load<Model>("Models/mars");
            _marsModel.EnableDefaultLighting();
            _juptierModel = Content.Load<Model>("Models/jupiter");
            _juptierModel.EnableDefaultLighting();
            _saturnModel = Content.Load<Model>("Models/saturn");
            _saturnModel.EnableDefaultLighting();
            _uranusModel = Content.Load<Model>("Models/uranus");
            _uranusModel.EnableDefaultLighting();
            _neptuneModel = Content.Load<Model>("Models/neptune");
            _neptuneModel.EnableDefaultLighting();
            _plutoModel = Content.Load<Model>("Models/pluto");
            _plutoModel.EnableDefaultLighting();
        }

        public void Render(RenderContext context)
        {
            Model model = null;
            switch (Planet)
            {
                case Planet.Earth:
                    model = _earthModel;
                    break;
                case Planet.Mars:
                    model = _marsModel;
                    break;
                case Planet.Jupiter:
                    model = _juptierModel;
                    break;
                case Planet.Saturn:
                    model = _saturnModel;
                    break;
                case Planet.Uranus:
                    model = _uranusModel;
                    break;
                case Planet.Neptune:
                    model = _neptuneModel;
                    break;
                case Planet.Pluto:
                    model = _plutoModel;
                    break;
            }
            if (model != null)
            {
                var transform = _transform;
                if (Transform != null)
                {
                    transform *= Transform.WorldTransform;
                }
                context.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
                var view = context.View ?? (Camera ?? CameraManager.ActiveCamera).View;
                var projection = context.Projection ?? (Camera ?? CameraManager.ActiveCamera).Projection;
                model.Draw(Matrix.CreateScale(Scale) * transform, view, projection);
            }
        }

        public void Tick(TickContext context)
        {
            _transform *= Matrix.CreateRotationY(context.DeltaTimeSeconds * 0.1f);
        }
    }
}
