﻿using Forge.Core.Components;
using Forge.UI.Glass;
using IntoTheVoid.UI.Flight;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntoTheVoid.Phases.Open
{
    public class OpenPhase : Phase
    {
        private UIDispose _dispose;
        private OpenPhaseControl _openPhaseControl;

        [Inject] UserInterfaceManager UserInterfaceManager { get; set; }

        public OpenPhase()
        {
            Title = "Open Space";
            Description = "A safe stretch of open space. Clear as much distance as possible.";
            Duration = TimeSpan.FromSeconds(14);
        }

        public override void Start()
        {
            _dispose = UserInterfaceManager.Create(new OpenControls());
            _openPhaseControl = Entity.Create().Add(new OpenPhaseControl());
        }

        public override void Stop()
        {
            _openPhaseControl.Entity.Delete();
            _dispose();
        }
    }
}
