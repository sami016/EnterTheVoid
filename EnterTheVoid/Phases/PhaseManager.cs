﻿using EnterTheVoid.Orchestration;
using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
using Forge.Core.Interfaces;
using Forge.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnterTheVoid.Phases
{
    public enum PhaseManagerState
    {
        LevelIntro = 0,
        Starting = 1,
        Running = 2,
        Ended = 3
    }

    /// <summary>
    /// This is the manager that runs the current phase set.
    /// </summary>
    public class PhaseManager : Component, IInit, ITick
    {
        private readonly IEnumerable<Phase> _phases;

        public PhaseManagerState State { get; private set; }
        public Phase CurrentPhase { get; private set; }

        private CompletionTimer _introTimer;
        private CompletionTimer _startTimer;
        private CompletionTimer _phaseTimer;
        private CompletionTimer _endedTimer;

        public float PhaseStartFraction => _startTimer.CompletedFraction;
        public float PhaseFraction => _phaseTimer.CompletedFraction;
        public int PhaseIndex { get; set; } = -1;
        public int NumberOfPhases => _phases.Count();

        [Inject] Orchestrator Orchestrator { get; set; }

        public PhaseManager(IEnumerable<Phase> phases)
        {
            _phases = phases;
            _introTimer = new CompletionTimer(TimeSpan.FromSeconds(5));
        }

        public void Initialise()
        {
        }

        private void StartPhase(int phaseIndex)
        {
            if (phaseIndex >= NumberOfPhases)
            {
                Orchestrator.PhasesComplete();
                return;
            }
            this.Update(() =>
            {
                PhaseIndex = phaseIndex % NumberOfPhases;
                State = PhaseManagerState.Starting;
                CurrentPhase = _phases.ElementAt(PhaseIndex);
                _startTimer = new CompletionTimer(TimeSpan.FromSeconds(3));
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
                _endedTimer = new CompletionTimer(TimeSpan.FromSeconds(3));
            });
        }

        public override void Dispose()
        {
            base.Dispose();
            try
            {
                CurrentPhase.Stop();
            }
            catch { }
        }

        public void Tick(TickContext context)
        {
            if (PhaseIndex == -1)
            {
                if (State == PhaseManagerState.LevelIntro)
                {
                    _introTimer.Tick(context.DeltaTime);
                    if (_introTimer.Completed)
                    {
                        State = PhaseManagerState.Starting;
                        StartPhase(0);
                    }
                    return;
                }
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
