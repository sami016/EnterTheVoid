using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Ships.Modules
{
    class FuelModule : Module
    {
        public override string FullName { get; } = "Fuel Storage";

        public override string ShortName { get; } = "Fuel Storage";

        public override string DescriptionShort { get; } = "Used to store rocket fuel, increasing the ship's fuel capacity.";
        public virtual int PassiveCapacity { get; set; } = 5;
    }
}
