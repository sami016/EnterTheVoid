using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
using Forge.Core.Interfaces;
using Forge.Core.Rendering;
using Forge.Core.Rendering.Cameras;
using Forge.Core.Space.Shapes;
using Forge.Core.Utilities;
using GreatSpaceRace.Flight;
using GreatSpaceRace.Ships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Phases.Asteroids
{
    public class Asteroid : Component, IInit, ITick, IRenderable, IShipCollider
    {
        private static Random Random = new Random();
        private static Model _asteroid1;

        public uint RenderOrder { get; } = 10;

        public bool AutoRender { get; } = true;

        [Inject] ContentManager Content { get; set; }
        [Inject] Transform Transform { get; set; }
        [Inject] CameraManager CameraManager { get; set; }
        [Inject] FlightSpaces FlightSpaces { get; set; }

        private Vector3 _spin;
        public Vector3 Velocity { get; set; } = Vector3.Zero;

        public void Initialise()
        {
            _spin = new Vector3((float)Random.NextDouble(), (float)Random.NextDouble(), (float)Random.NextDouble());
            if (_asteroid1 == null)
            {
                _asteroid1 = Content.Load<Model>("Models/asteroid1");
                _asteroid1.EnableDefaultLighting();
                _asteroid1.SetDiffuseColour(Color.RosyBrown);
            }
            FlightSpaces.ObstacleSpace.Add(Entity);
        }

        public void Tick(TickContext context)
        {
            var spin = _spin * context.DeltaTimeSeconds;
            Transform.Update(() =>
            {
                Transform.Rotation *= Quaternion.CreateFromYawPitchRoll(spin.X, spin.Y, spin.Z);
                Transform.Location += Velocity * context.DeltaTimeSeconds;
            });
        }

        public void Render(RenderContext context)
        {
            context.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            var camera = CameraManager.ActiveCamera;
            _asteroid1.Draw(Transform.WorldTransform, camera.View, camera.Projection);
        }

        public override void Dispose()
        {
            FlightSpaces.ObstacleSpace.Remove(Entity);
        }

        public void OnHit(FlightNode node, Vector3 nodeLocation, Section section)
        {
            Entity.Delete();
        }
    }
}
