using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
using Forge.Core.Interfaces;
using Forge.Core.Rendering;
using Forge.Core.Rendering.Cameras;
using Forge.Core.Space.Bodies;
using Forge.Core.Space.Shapes;
using EnterTheVoid.Obstacles;
using EnterTheVoid.Phases.Asteroids;
using EnterTheVoid.Projectiles;
using EnterTheVoid.Ships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnterTheVoid.Flight
{
    public class FlightNode : Component, IInit, ITick, IRenderable, IShipCollider, IProjectileCollider
    {
        private static readonly Random Random = new Random();
        private readonly ShipTopology _shipTopology;
        private ShipSectionRenderer _shipRenderer;
        private readonly FlightShip _ship;

        public Guid ShipGuid => _ship.ShipGuid;
        public Point GridLocation { get; }

        public uint RenderOrder { get; } = 100;

        public bool AutoRender { get; } = true;

        [Inject] CameraManager CameraManager { get; set; }
        [Inject] Transform Transform { get; set; }
        [Inject] FlightSpaces FlightSpaces { get; set; }

        public Vector3 GloalLocation => Transform.GlobalLocation;

        public FlightNode(FlightShip ship, Point gridPosition, ShipTopology shipTopology)
        {
            _ship = ship;
            GridLocation = gridPosition;
            _shipTopology = shipTopology;
        }

        public void Initialise()
        {
            _shipRenderer = Entity.Add(new ShipSectionRenderer());
            FlightSpaces.ShipSpace.Add(Entity);
        }

        public bool Active => _shipTopology.SectionAt(GridLocation) != null;

        public void Tick(TickContext context)
        {
            if (!Active)
            {
                return;
            }
            
            // Get location of node.
            var location = GlobalLocation;

            // Get every obstacle within 5 units of the node.
            foreach (var entity in FlightSpaces.ObstacleSpace.GetNearby(location, 5f))
            {
                var pos = entity.Get<Transform>().Location;
                var radius = entity.Get<IObstacle>()?.Radius ?? 1f;
                var distance = (location - pos).Length();
                if (distance < 1f + radius)
                {
                    if (entity.Has<IShipCollider>())
                    {
                        entity.Get<IShipCollider>().OnHit(this, _ship, GridLocation, location, _shipTopology.SectionAt(GridLocation));
                    }
                }
            }

            foreach (var entity in FlightSpaces.ShipSpace.GetNearby(location, 5f))
            {
                var otherNode = entity.Get<FlightNode>();
                if (!otherNode.Active || otherNode.ShipGuid == ShipGuid)
                {
                    continue;
                }
                var pos = otherNode.GlobalLocation;
                var distance = (location - pos).Length();
                if (distance < 2f)
                {
                    if (entity.Has<IShipCollider>())
                    {
                        entity.Get<IShipCollider>().OnHit(this, _ship, GridLocation, location, _shipTopology.SectionAt(GridLocation));
                    }
                }
            }
        }

        public Vector3 GlobalLocation
        {
            get
            {
                var parentTransform = Entity.Parent.Get<Transform>().WorldTransform;
                var transform = Transform.WorldTransform * parentTransform;
                return Vector3.Transform(Vector3.Zero, transform);
            }
        }

        public override void Dispose()
        {
            FlightSpaces.ShipSpace.Remove(Entity);
        }

        public void Render(RenderContext context)
        {
            var parentTransform = Entity.Parent.Get<Transform>().WorldTransform;
            if (_shipTopology.SectionAt(GridLocation) != null)
            {
                _shipRenderer.Render(context, Transform.WorldTransform * parentTransform, _shipTopology.SectionAt(GridLocation));
            }
        }

        public void OnHit(FlightNode node, FlightShip ship, Point gridLocation, Vector3 nodeLocation, Section section)
        {
            var otherLocation = node.Entity.Get<Transform>().Location;
            var direction = (otherLocation - Transform.Location);
            // Handle identical location with random impulse.
            if (direction.LengthSquared() == 0)
            {
                direction = new Vector3((float)Random.NextDouble() - 0.5f, (float)Random.NextDouble() - 0.5f, (float)Random.NextDouble() - 0.5f);
            }
            var shipDirection = (ship.GetCenterGlobalLocation() - _ship.GetCenterGlobalLocation());
            shipDirection.Normalize();
            direction.Normalize();
            _ship.Damage(GridLocation, 2);
            _ship.Velocity = (direction + shipDirection) * 5f;
            //_ship.Push(direction, 10f);
        }

        public void OnHit(Entity projectileEntity, ProjectileBase projectile)
        {
            _ship.Damage(GridLocation, (int)Math.Floor(projectile.GetDamage(Entity, this)));
        }
    }
}
