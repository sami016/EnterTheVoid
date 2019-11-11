using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Interfaces;
using Forge.Core.Scenes;
using Forge.Core.Utilities;
using GreatSpaceRace.Scenes;
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

        public BuildModeState State { get; private set; }
        private CompletionTimer _countIn;
        private CompletionTimer _countDown;
        private CompletionTimer _countOut;

        [Inject] SceneManager SceneManager { get; set; }

        public BuildMode()
        {
            State = BuildModeState.CountIn;
            _countIn = new CompletionTimer(TimeSpan.FromSeconds(10));
            _countDown = new CompletionTimer(TimeSpan.FromSeconds(90));
            _countOut = new CompletionTimer(TimeSpan.FromSeconds(5));
        }

        public void Tick(TickContext context)
        {
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
                        //todo
                        //SceneManager.SetScene(new BuildScene());
                    }
                    return;
            }
        }
    }
}
