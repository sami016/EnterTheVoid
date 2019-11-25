using System;
using System.Collections.Generic;
using System.Text;

namespace IntoTheVoid.Ships.Modules
{
    class BlasterModule : Module
    {
        public override string FullName { get; } = "Blaster Gun";

        public override string ShortName { get; } = "Blaster Gun";

        public override string DescriptionShort { get; } = "A lightweight single directional weapon module.";
    }
}
