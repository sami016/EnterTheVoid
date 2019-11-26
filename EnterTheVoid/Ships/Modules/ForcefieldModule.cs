using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.Ships.Modules
{
    class ForcefieldShieldModule : Module
    {
        public override string FullName { get; } = "Forcefield OmniShield bubble";

        public override string ShortName { get; } = "Forcefield shield acting in al all directional bubble";

        public override string DescriptionShort { get; } = "A bubble shield generator.";
    }
}
