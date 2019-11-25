using System;
using System.Collections.Generic;
using System.Text;

namespace IntoTheVoid.Ships.Modules
{
    class ResearchCenterModule : Module
    {
        public override string FullName { get; } = "Research Center";

        public override string ShortName { get; } = "Research Center Module";

        public override string DescriptionShort { get; } = "Provides an upgrade slot.";
    }
}
