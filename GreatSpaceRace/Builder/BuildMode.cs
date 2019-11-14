using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Interfaces;
using Forge.Core.Scenes;
using Forge.Core.Utilities;
using GreatSpaceRace.Scenes;
using GreatSpaceRace.Ships;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Builder
{
    public enum BuildModeState
    {
        CountIn,
        Building,
        Finished
    }

    public class BuildMode : Component, ITick
    {
        public bool Building => State == BuildModeState.Building;
        public float BuildSecondsRemaining => (float)_countDown.RemainingTime.TotalSeconds;

        private readonly ShipTopology _topology;

        public BuildModeState State { get; private set; }
        private CompletionTimer _countIn;
        private CompletionTimer _countDown;
        private CompletionTimer _countOut;

        [Inject] SceneManager SceneManager { get; set; }

        public BuildMode(ShipTopology topology)
        {
            _topology = topology;
            State = BuildModeState.CountIn;
            _countIn = new CompletionTimer(TimeSpan.FromSeconds(10));
            _countDown = new CompletionTimer(TimeSpan.FromSeconds(90));
            _countOut = new CompletionTimer(TimeSpan.FromSeconds(5));
        }

        public void Tick(TickContext context)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                State = BuildModeState.Finished;
            }
            switch (State)
            {
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
                        SceneManager.SetScene(new FlightScene(_topology));
                    }
                    return;
            }
        }
    }
}
