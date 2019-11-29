using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
using Forge.Core.Interfaces;
using Forge.Core.Rendering;
using Forge.Core.Rendering.Cameras;
using Forge.Core.Space.Shapes;
using Forge.Core.Utilities;
using EnterTheVoid.Flight;
using EnterTheVoid.Obstacles;
using EnterTheVoid.Projectiles;
using EnterTheVoid.Ships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using EnterTheVoid.Upgrades;

namespace EnterTheVoid.Phases.Asteroids
{
    public class Ore : Component, IInit, ITick, IRenderable, IShipCollider, IProjectileCollider, IVelocity, IObstacle
    {
        private static Random Random = new Random();
        private static Model _fuelItemModel;

        public uint RenderOrder { get; } = 10;

        public bool AutoRender { get; } = true;

        [Inject] ContentManager Content { get; set; }
        [Inject] Transform Transform { get; set; }
        [Inject] CameraManager CameraManager { get; set; }
        [Inject] FlightSpaces FlightSpaces { get; set; }

        private Vector3 _spin;
        private bool _claimed = false;
        public Vector3 Velocity { get; set; } = Vector3.Zero;

        public float Radius { get; protected set; } = 0.3f;

        public void Initialise()
        {
            _spin = new Vector3((float)Random.NextDouble(), (float)Random.NextDouble(), (float)Random.NextDouble());
            if (_fuelItemModel == null)
            {
                _fuelItemModel = Content.Load<Model>("Models/fuelItem");
                //_fuelItemModel.EnableDefaultLighting();
                _fuelItemModel.SetDiffuseColour(Color.LightGoldenrodYellow);
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
            _fuelItemModel.Draw(Matrix.CreateScale(0.3f) * Transform.WorldTransform, camera.View, camera.Projection);
        }

        public override void Dispose()
        {
            FlightSpaces.ObstacleSpace.Remove(Entity);
        }

        public void OnHit(FlightNode node, FlightShip ship, Point gridLocation, Vector3 nodeLocation, Section section)
        {
            if (!_claimed)
            {
                if (ship.HasUpgrade<OreProcessing>())
                {
                    ship.AddEnergy(2);
                }
                else
                {
                    ship.AddEnergy(1);
                }
                _claimed = true;
            }
            Entity.Delete();
        }

        public virtual void OnHit(Entity projectileEntity, ProjectileBase projectile)
        {
            Entity.Delete();
        }
    }
}
