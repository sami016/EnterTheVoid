using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
using Forge.Core.Interfaces;
using Forge.Core.Rendering.Cameras;
using GreatSpaceRace.Ships;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreatSpaceRace.Flight
{
    public class FlightCameraControl : Component, ITick
    {
        private readonly Entity _shipEntity;
        [Inject] Transform Transform { get; set; }
        [Inject] Camera Camera { get; set; }

        public FlightCameraControl(Entity shipEntity)
        {
            _shipEntity = shipEntity;
        }

        public void Tick(TickContext context)
        {
            var cellLocations = _shipEntity.Children
                .Select(x => x.Get<Transform>())
                .Where(x => x != null)
                .Select(x => x.Location)
                .ToArray();
            var total = Vector3.Zero;
            foreach (var cellLocation in cellLocations)
            {
                total += cellLocation;
            }
            var averageLocation = total / cellLocations.Length;

            this.Update(() =>
            {
                Transform.Location = averageLocation + 10 * Vector3.Up + 2 * Vector3.Backward;
                Camera.LookAt(averageLocation);
                Camera.Recalculate();
                Camera.RecalculateParameters();
            });
        }
    }
}
