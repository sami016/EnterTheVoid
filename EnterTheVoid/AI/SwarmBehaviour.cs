using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
using EnterTheVoid.Flight;
using EnterTheVoid.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Forge.Core.Utilities;

namespace EnterTheVoid.AI
{
    public class SwarmBehaviour
    {
        private readonly FlightShip _ship;
        private readonly Transform _transform;

        public Vector3 Target { get; set; }
        public float CatchupSpeed { get; set; } = 1f;

        public SwarmBehaviour(FlightShip ship, Transform transform, Vector3 target)
        {
            _ship = ship;
            _transform = transform;
            Target = target;
        }

        public void Tick(TickContext context)
        {
            var displacement = Target - _transform.Location;
            var length = (_transform.Location - Target).Length();
            if (length > 0.1f)
            {
                _ship.Update(() =>
                {
                    //_transform.Location = Target;
                    //displacement.Normalize();
                    _ship.Velocity = displacement * CatchupSpeed;
                });
            }
        }
    }
}
