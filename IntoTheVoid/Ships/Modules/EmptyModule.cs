using System;
using System.Collections.Generic;
using System.Text;

namespace IntoTheVoid.Ships.Modules
{
    class EmptyModule : Module
    {
        public override string FullName { get; } = "Empty Module";

        public override string ShortName { get; } = "Empty Module";

        public override string DescriptionShort { get; } = "An empty section with no functional improvement.";
    }
}
