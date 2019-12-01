using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
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
using Microsoft.Xna.Framework.Input;
using EnterTheVoid.Ships;
using Forge.Core.Space.Bodies;
using Forge.Core.Space.Shapes;
using EnterTheVoid.Constants;
using Forge.Core.Rendering.VertexTypes;

namespace EnterTheVoid.Builder
{
    public class BuildNode : Component, IRenderable, IInit, ITick
    {
        private Model _floorModel;
        private Model _triModel;
        public Point GridLocation { get; }

        private readonly ShipTopology _shipTopology;
        private static Matrix Offset = Matrix.CreateTranslation(0, -0.12f, 0);
        private ShipSectionRenderer _shipRenderer;

        [Inject] ContentManager Content { get; set; }
        [Inject] CameraManager CameraManager { get; set; }
        [Inject] Transform Transform { get; set; }

        public uint RenderOrder { get; } = 100;
        public bool AutoRender { get; } = true;

        public BuildNode(Point gridPosition, ShipTopology shipTopology)
        {
            GridLocation = gridPosition;
            _shipTopology = shipTopology;
        }

        public void Initialise()
        {
            _triModel = Content.Load<Model>("Models/tri");
            _floorModel = Content.Load<Model>("Models/floor2");
            _floorModel.EnableDefaultLighting();
            _floorModel.SetDiffuseColour(Color.DarkSlateBlue);
            Entity.Add(new StaticBody(MeshShape.FromModel(_floorModel), (byte)HitLayers.BuildTile, Offset));
            //Entity.Add(new StaticBody(MeshShape.FromModel(_triModel), (byte)HitLayers.BuildTile));

            _shipRenderer = Entity.Add(new ShipSectionRenderer());
        }

        public void Render(RenderContext context)
        {
            context.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            var camera = CameraManager.ActiveCamera;
            if (_shipTopology.SectionAt(GridLocation) != null)
            {
                _shipRenderer.Render(context, Transform.WorldTransform, _shipTopology.SectionAt(GridLocation));
               //_floorModel.SetDiffuseColour(Color.Gray);
            } else
            {
                //_floorModel.SetDiffuseColour(Color.DarkSlateBlue);
            }
            _floorModel.Draw(Offset * Transform.WorldTransform, camera.View, camera.Projection);
        }

        public void Tick(TickContext context)
        {
            //var keyboard = Keyboard.GetState();
            //this.Update(() =>
            //{
            //    if (keyboard.IsKeyDown(Keys.T))
            //    {
            //        Transform.Rotation *= Quaternion.CreateFromYawPitchRoll(0, (float)(Math.PI * context.DeltaTimeSeconds), 0);
            //    }
            //    //Transform.Location += Vector3.Up * context.DeltaTimeSeconds * 0.1f;
            //});
        }
    }
}
