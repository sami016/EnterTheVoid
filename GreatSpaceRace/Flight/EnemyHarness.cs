using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
using Forge.Core.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Flight
{
    /// <summary>
    /// Harnesses an enemy in position relative to the player's ship.
    /// </summary>
    public class EnemyHarness : Component, ITick
    {
        private readonly FlightShip _playerShip;

        public float? FixX { get; set; }
        public float FixZ { get; set; } = 10;

        [Inject] FlightShip Ship { get; set; }
        [Inject] Transform Transform { get; set; }

        public EnemyHarness(FlightShip playerShip)
        {
            _playerShip = playerShip;
        }

        public void Tick(TickContext context)
        {
            var playerLocation = _playerShip.Entity.Get<Transform>().Location;
            this.Update(() =>
            {
                Ship.Velocity = new Vector3(
                    FixX.HasValue ? 0 : Ship.Velocity.X,
                    Ship.Velocity.Y,
                    0f
                );
                Transform.Location = new Vector3(
                    FixX.HasValue ? (playerLocation.X + FixX.Value) : Transform.Location.X,
                    Transform.Location.Y,
                    playerLocation.Z + FixZ
                );
            });
        }
    }
}
