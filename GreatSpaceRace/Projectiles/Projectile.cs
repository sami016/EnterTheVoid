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

namespace GreatSpaceRace.Projectiles
{
    public class Projectile : Component, IInit, ITick, IRenderable, IShipCollider
    {
        private static Random Random = new Random();
        private static Model _projectileModel;

        public uint RenderOrder { get; } = 10;

        public bool AutoRender { get; } = true;

        [Inject] ContentManager Content { get; set; }
        [Inject] Transform Transform { get; set; }
        [Inject] CameraManager CameraManager { get; set; }
        [Inject] FlightSpaces FlightSpaces { get; set; }

        public Vector3 Velocity { get; set; } = Vector3.Zero;

        private readonly CompletionTimer _despawnTimer;

        public Projectile(Vector3 direction, float speed, TimeSpan? despawnDuration = null)
        {
            direction.Normalize();
            Velocity = direction * speed;
            _despawnTimer = new CompletionTimer(despawnDuration ?? TimeSpan.FromSeconds(10));
        }

        public void Initialise()
        {
            if (_projectileModel == null)
            {
                _projectileModel = Content.Load<Model>("Models/asteroid1");
                _projectileModel.EnableDefaultLighting();
                _projectileModel.SetDiffuseColour(Color.White);
            }
            FlightSpaces.ObstacleSpace.Add(Entity);
        }

        public void Tick(TickContext context)
        {
            var location = Transform.Location;
            // Get every obstacle within 5 units of the node.
            var obstacle = FlightSpaces.ObstacleSpace.GetNearby(location, 5f);
            foreach (var entity in obstacle)
            {
                var pos = entity.Get<Transform>().Location;
                var distance = (location - pos).Length();
                if (distance < 1.1f)
                {
                    if (entity.Has<IProjectileCollider>())
                    {
                        entity.Get<IProjectileCollider>().OnHit(Entity, this);
                    }
                }
            }

            //var spin = _spin * context.DeltaTimeSeconds;
            Transform.Update(() =>
            {
                //Transform.Rotation *= Quaternion.CreateFromYawPitchRoll(spin.X, spin.Y, spin.Z);
                Transform.Location += Velocity * context.DeltaTimeSeconds;
            });
        }

        public void Render(RenderContext context)
        {
            context.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            var camera = CameraManager.ActiveCamera;
            _projectileModel.Draw(Matrix.CreateScale(0.1f) * Transform.WorldTransform, camera.View, camera.Projection);
        }

        public override void Dispose()
        {
            FlightSpaces.ObstacleSpace.Remove(Entity);
        }

        public void OnHit(FlightNode node, FlightShip ship, Point gridLocation, Vector3 nodeLocation, Section section)
        {
        }
    }
}
