using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Interfaces;
using EnterTheVoid.Flight;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EnterTheVoid.Phases.Combat
{
    public class PhaseKillTarget : Component, ITick
    {
        private readonly Phase _phase;
        private readonly int _allowedRemaining;
        private readonly IEnumerable<FlightShip> _enemyShips;

        public PhaseKillTarget(Phase phase, IEnumerable<FlightShip> enemyShips, int allowedRemaining = 0)
        {
            _phase = phase;
            _allowedRemaining = allowedRemaining;
            _enemyShips = enemyShips;
        }

        public int Remaining
        {
            get
            {
                return _enemyShips.Where(x => !x.Entity.Deleted).Count();
            }
        }

        public void Tick(TickContext context)
        {
            if (Remaining <= _allowedRemaining)
            {
                _phase.Ended = true;
            }
        }
    }
}
