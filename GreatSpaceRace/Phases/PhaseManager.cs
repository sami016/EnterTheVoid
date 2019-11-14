using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Interfaces;
using Forge.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreatSpaceRace.Phases
{
    public enum PhaseManagerState
    {
        Starting = 0,
        Running = 1,
        Ended = 2
    }

    /// <summary>
    /// This is the manager that runs the current phase set.
    /// </summary>
    public class PhaseManager : Component, ITick
    {
        private readonly IEnumerable<Phase> _phases;

        public PhaseManagerState State { get; private set; }
        public Phase CurrentPhase { get; private set; }

        private CompletionTimer _startTimer;
        private CompletionTimer _phaseTimer;

        public float PhaseStartFraction => _startTimer.CompletedFraction;
        public float PhaseFraction => _phaseTimer.CompletedFraction;
        public int PhaseIndex { get; set; }
        public int NumberOfPhases => _phases.Count();

        public PhaseManager(IEnumerable<Phase> phases)
        {
            _phases = phases;
            StartPhase(0);
        }

        private void StartPhase(int phaseIndex)
        {
            PhaseIndex = phaseIndex;
            State = PhaseManagerState.Starting;
            CurrentPhase = _phases.ElementAt(phaseIndex);
            _startTimer = new CompletionTimer(TimeSpan.FromSeconds(10));
            _phaseTimer = new CompletionTimer(CurrentPhase.Duration);
        }

        public void Tick(TickContext context)
        {
            if (State == PhaseManagerState.Starting)
            {
                _startTimer.Tick(context.DeltaTime);
                if (_startTimer.Completed)
                {
                    CurrentPhase.Start();
                    State = PhaseManagerState.Running;
                }
            }
            else if (State == PhaseManagerState.Running)
            {
                CurrentPhase.Tick(context);
                _phaseTimer.Tick(context.DeltaTime);
                if (_phaseTimer.Completed)
                {
                    State = PhaseManagerState.Ended;
                    //TODO: advanced.
                }
            }
        }
    }
}
