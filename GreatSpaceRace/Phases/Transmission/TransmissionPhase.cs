using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Phases.Transmission
{
    class TransmissionPhase : Phase
    {
        public TransmissionPhase()
        {
            Title = "Incoming Transmission - Origin Unknown";
            Description = "Upgrade blueprints partially decryped. Select upgrade ";
        }

        public override void Start()
        {
        }

        public override void Stop()
        {
        }
    }
}
