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
        private readonly Transform _shipTransform;

        [Inject] Transform Transform { get; set; }
        [Inject] Camera Camera { get; set; }

        public FlightCameraControl(Entity shipEntity)
        {
            _shipEntity = shipEntity;
            _shipTransform = _shipEntity.Get<Transform>();
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

            var forwardOffset = Vector3.Forward * 1;

            _shipEntity.Update(() =>
            {
                _shipEntity.Get<Transform>()
                    .RotationCenter = averageLocation;
            });
            this.Update(() =>
            {
                Transform.RotationCenter = averageLocation;
                Transform.Location = _shipTransform.Location + averageLocation + 20 * Vector3.Up + 2 * Vector3.Backward + forwardOffset;
                Camera.LookAt(_shipTransform.Location + averageLocation + forwardOffset);
                Camera.Recalculate();
                Camera.RecalculateParameters();
            });
        }
    }
}
