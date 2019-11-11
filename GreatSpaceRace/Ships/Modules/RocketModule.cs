using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Ships.Modules
{
    class RocketModule : Module
    {
        public override string FullName { get; } = "Ion Rocket";

        public override string ShortName { get; } = "Ion Rocket";

        public override string DescriptionShort { get; } = "A lightweight rocket module.";
    }
}
