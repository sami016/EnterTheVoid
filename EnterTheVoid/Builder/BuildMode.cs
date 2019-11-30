using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Interfaces;
using Forge.Core.Scenes;
using Forge.Core.Utilities;
using EnterTheVoid.Orchestration;
using EnterTheVoid.Scenes;
using EnterTheVoid.Ships;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using EnterTheVoid.Constants;

namespace EnterTheVoid.Builder
{
    public enum BuildModeState
    {
        AwaitingBegin,
        CountIn,
        Building,
        Finished
    }

    public class BuildMode : Component, ITick
    {
        public bool Building => State == BuildModeState.Building;
        public float BuildSecondsRemaining => (float)_countDown.RemainingTime.TotalSeconds;

        private readonly ShipTopology _topology;
        private bool _completed = false;

        public BuildModeState State { get; private set; }
        private CompletionTimer _countIn;
        private CompletionTimer _countDown;
        private CompletionTimer _countOut;

        [Inject] Orchestrator Orchestrator { get; set; }

        public BuildMode(ShipTopology topology, Planet planet)
        {
            _topology = topology;
            State = BuildModeState.AwaitingBegin;
            _countIn = new CompletionTimer(TimeSpan.FromSeconds(3));
            _countDown = new CompletionTimer(TimeSpan.FromSeconds(planet == Planet.Earth ? 80 : 50));
            _countOut = new CompletionTimer(TimeSpan.FromSeconds(5));
        }

        public void Start()
        {
            if (State == BuildModeState.AwaitingBegin)
            {
                State = BuildModeState.CountIn;
            }
        }

        public void Tick(TickContext context)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                State = BuildModeState.Finished;
            }
            switch (State)
            {
                case BuildModeState.AwaitingBegin:
                    return;
                case BuildModeState.CountIn:
                    _countIn.Tick(context.DeltaTime);
                    if (_countIn.Completed)
                    {
                        State = BuildModeState.Building;
                    }
                    return;
                case BuildModeState.Building:
                    _countDown.Tick(context.DeltaTime);
                    if (_countDown.Completed)
                    {
                        State = BuildModeState.Finished;
                    }
                    return;
                case BuildModeState.Finished:
                    _countOut.Tick(context.DeltaTime);
                    if (_countOut.Completed)
                    {
                        if (!_completed)
                        {
                            _completed = true;
                            Orchestrator.NextFlight();
                        }
                    }
                    return;
            }
        }
    }
}
