using Forge.Core.Components;
using Forge.Core.Interfaces;
using Forge.Core.Rendering;
using Forge.Core.Rendering.Cameras;
using GreatSpaceRace.Ships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Flight
{
    public class FlightNode : Component, IInit, IRenderable
    {

        private readonly ShipTopology _shipTopology;
        private ShipSectionRenderer _shipRenderer;

        public Point GridLocation { get; }

        public uint RenderOrder { get; } = 0;

        public bool AutoRender { get; } = true;

        [Inject] CameraManager CameraManager { get; set; }
        [Inject] Transform Transform { get; set; }

        public FlightNode(Point gridPosition, ShipTopology shipTopology)
        {
            GridLocation = gridPosition;
            _shipTopology = shipTopology;
        }

        public void Initialise()
        {
            _shipRenderer = Entity.Add(new ShipSectionRenderer());
        }

        public void Render(RenderContext context)
        {
            var parentTransform = Entity.Parent.Get<Transform>().WorldTransform;
            if (_shipTopology.Sections[GridLocation.X, GridLocation.Y] != null)
            {
                _shipRenderer.Render(context, parentTransform * Transform.WorldTransform, _shipTopology.Sections[GridLocation.X, GridLocation.Y]);
            }
        }
    }
}
