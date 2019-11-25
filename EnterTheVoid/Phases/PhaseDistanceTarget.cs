using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Interfaces;
using EnterTheVoid.Flight;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.Phases
{
    public class PhaseDistanceTarget : Component, ITick
    {
        private readonly Phase _phase;
        private readonly FlightShip _ship;
        private readonly Transform _shipTransform;
        private readonly float _targetZ;

        public PhaseDistanceTarget(Phase phase, FlightShip ship, float distanceTarget)
        {
            _phase = phase;
            _ship = ship;
            _shipTransform = ship.Entity.Get<Transform>();
            _targetZ = _shipTransform.Location.Z - distanceTarget;
        }

        public float Remaining => _targetZ - _shipTransform.Location.Z;

        public void Tick(TickContext context)
        {
            if (Remaining > 0)
            {
                _phase.Ended = true;
            }
        }
    }
}
