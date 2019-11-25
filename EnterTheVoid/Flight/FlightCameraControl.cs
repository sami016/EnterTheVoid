using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
using Forge.Core.Interfaces;
using Forge.Core.Rendering.Cameras;
using EnterTheVoid.Ships;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnterTheVoid.Flight
{
    public class FlightCameraControl : Component, IInit, ITick
    {
        private readonly Entity _shipEntity;
        private readonly Transform _shipTransform;
        private readonly FlightShip _flightShip;
        private float _activeCameraScale;
        public float CameraScale { get; set; }

        [Inject] Transform Transform { get; set; }
        [Inject] Camera Camera { get; set; }

        public FlightCameraControl(Entity shipEntity)
        {
            _shipEntity = shipEntity;
            _shipTransform = _shipEntity.Get<Transform>();
            _flightShip = shipEntity.Get<FlightShip>();
        }

        public void Initialise()
        {
            CameraScale = (Camera.CameraParameters as OrthographicCameraParameters).Width;
            _activeCameraScale = (Camera.CameraParameters as OrthographicCameraParameters).Width;
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
                if (_activeCameraScale != CameraScale)
                {
                    Console.WriteLine($"Active camera scale: {_activeCameraScale} Seeking: {CameraScale}");
                    _activeCameraScale = _activeCameraScale - (float)Math.Sign(_activeCameraScale - CameraScale) * 10f * context.DeltaTimeSeconds;
                    if (Math.Abs(_activeCameraScale - CameraScale) < 0.1f)
                    {
                        _activeCameraScale = CameraScale;
                    }
                    Camera.CameraParameters = new OrthographicCameraParameters(_activeCameraScale);
                }
                Transform.RotationCenter = averageLocation;
                Transform.Location = _shipTransform.Location + averageLocation + 20 * Vector3.Up + 2 * Vector3.Backward + forwardOffset;
                Camera.LookAt(_shipTransform.Location + averageLocation + forwardOffset);
                Camera.Recalculate();
                Camera.RecalculateParameters();
            });
        }

    }
}
