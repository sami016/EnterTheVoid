using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Ships.Modules
{
    class LifeSupportModule : Module
    {
        public override string FullName { get; } = "Life support pod";

        public override string ShortName { get; } = "Life support pod";

        public override string DescriptionShort { get; } = "Life support pod designed to provide critical requirements for 10 crew members.";
    }
}
