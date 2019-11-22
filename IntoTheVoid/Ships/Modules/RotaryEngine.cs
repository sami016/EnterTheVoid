using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Ships.Modules
{
    class RotaryEngine : Module
    {
        public override string FullName { get; } = "Rotary Engine";

        public override string ShortName { get; } = "Rotary Engine";

        public override string DescriptionShort { get; } = "Provides passive boost to ship tilt capabilities.";
    }
}
