using Forge.Core.Components;
using Forge.UI.Glass;
using GreatSpaceRace.UI.Flight;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Phases.Open
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
