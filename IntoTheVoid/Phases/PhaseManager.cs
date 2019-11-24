using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
using Forge.Core.Interfaces;
using Forge.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntoTheVoid.Phases
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
    public class PhaseManager : Component, IInit, ITick
    {
        private readonly IEnumerable<Phase> _phases;

        public PhaseManagerState State { get; private set; }
        public Phase CurrentPhase { get; private set; }

        private CompletionTimer _startTimer;
        private CompletionTimer _phaseTimer;
        private CompletionTimer _endedTimer;

        public float PhaseStartFraction => _startTimer.CompletedFraction;
        public float PhaseFraction => _phaseTimer.CompletedFraction;
        public int PhaseIndex { get; set; } = -1;
        public int NumberOfPhases => _phases.Count();

        public PhaseManager(IEnumerable<Phase> phases)
        {
            _phases = phases;
        }

        public void Initialise()
        {
            StartPhase(0);
        }

        private void StartPhase(int phaseIndex)
        {
            this.Update(() =>
            {
                PhaseIndex = phaseIndex % NumberOfPhases;
                State = PhaseManagerState.Starting;
                CurrentPhase = _phases.ElementAt(PhaseIndex);
                _startTimer = new CompletionTimer(TimeSpan.FromSeconds(6));
#if DEBUG
                _startTimer = new CompletionTimer(TimeSpan.FromSeconds(1));
#endif
                if (CurrentPhase.Duration.HasValue)
                {
                    _phaseTimer = new CompletionTimer(CurrentPhase.Duration.Value);
                } else
                {
                    _phaseTimer = null;
                }
                _endedTimer = new CompletionTimer(TimeSpan.FromSeconds(6));
            });
        }

        public void Tick(TickContext context)
        {
            if (PhaseIndex == -1)
            {
                return;
            }
            if (State == PhaseManagerState.Starting)
            {
                _startTimer.Tick(context.DeltaTime);
                if (_startTimer.Completed)
                {
                    CurrentPhase.Start();
                    _phaseTimer?.Restart();
                    State = PhaseManagerState.Running;
                }
            }
            else if (State == PhaseManagerState.Running)
            {
                CurrentPhase.Tick(context);
                _phaseTimer?.Tick(context.DeltaTime);
                if (_phaseTimer?.Completed == true || CurrentPhase.Ended)
                {
                    CurrentPhase.Stop();
                    _endedTimer.Restart();
                    State = PhaseManagerState.Ended;
                    //TODO: advanced.
                }
            }
            else if (State == PhaseManagerState.Ended)
            {
                _endedTimer.Tick(context.DeltaTime);
                if (_endedTimer.Completed)
                {
                    CurrentPhase.Dispose();
                    _startTimer.Restart();
                    StartPhase(PhaseIndex+1);
                }
            }
        }
    }
}
