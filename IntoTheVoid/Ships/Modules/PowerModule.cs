using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Ships.Modules
{
    class EnergyModule : Module
    {
        public override string FullName { get; } = "Energy Storage";

        public override string ShortName { get; } = "Energy Storage";

        public override string DescriptionShort { get; } = "Provides energy storage and generation.";
        public virtual int PassiveCapacity { get; set; } = 10;
    }
}
