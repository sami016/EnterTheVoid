﻿using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
using Forge.Core.Interfaces;
using Forge.Core.Rendering;
using Forge.Core.Rendering.Cameras;
using Forge.Core.Space.Bodies;
using Forge.Core.Space.Shapes;
using GreatSpaceRace.Phases.Asteroids;
using GreatSpaceRace.Ships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreatSpaceRace.Flight
{
    public class FlightNode : Component, IInit, ITick, IRenderable
    {

        private readonly ShipTopology _shipTopology;
        private ShipSectionRenderer _shipRenderer;

        public Point GridLocation { get; }

        public uint RenderOrder { get; } = 100;

        public bool AutoRender { get; } = true;

        [Inject] CameraManager CameraManager { get; set; }
        [Inject] Transform Transform { get; set; }
        [Inject] StaticBody Body { get; set; }
        [Inject] FlightSpaces FlightSpaces { get; set; }

        public FlightNode(Point gridPosition, ShipTopology shipTopology)
        {
            GridLocation = gridPosition;
            _shipTopology = shipTopology;
        }

        public void Initialise()
        {
            _shipRenderer = Entity.Add(new ShipSectionRenderer());
            Entity.Add(new StaticBody(new SphereShape(1f)));
        }

        public void Tick(TickContext context)
        {
            if (_shipTopology.Sections[GridLocation.X, GridLocation.Y] == null)
            {
                return;
            }
            
            // Get location of node.
            var parentTransform = Entity.Parent.Get<Transform>().WorldTransform;
            var transform = Transform.WorldTransform * parentTransform;
            var location = Vector3.Transform(Vector3.Zero, transform);

            // Get every obstacle within 5 units of the node.
            var obstacle = FlightSpaces.ObstacleSpace.GetNearby(location, 5f);
            foreach (var entity in obstacle)
            {
                var pos = entity.Get<Transform>().Location;
                var distance = (location - pos).Length();
                if (distance < 2f)
                {
                    if (entity.Has<IShipCollider>())
                    {
                        entity.Get<IShipCollider>().OnHit(this, location, _shipTopology.Sections[GridLocation.X, GridLocation.Y]);
                    }
                }
            }
        }

        private void HandleHit(Entity ent)
        {
            Console.WriteLine("Hit obstacle");
            if (ent.Has<Asteroid>())
            {
                ent.Delete();
            }
        }

        public void Render(RenderContext context)
        {
            var parentTransform = Entity.Parent.Get<Transform>().WorldTransform;
            if (_shipTopology.Sections[GridLocation.X, GridLocation.Y] != null)
            {
                _shipRenderer.Render(context, Transform.WorldTransform * parentTransform, _shipTopology.Sections[GridLocation.X, GridLocation.Y]);
            }
        }
    }
}
