using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.Ships.Modules
{
    class ShieldBubbleGeneratorModule : Module
    {
        public override string FullName { get; } = "Shield Bubble Generator";

        public override string ShortName { get; } = "Shield Bubble Generator";

        public override string DescriptionShort { get; } = "A bubble shield generator.";

        public ShieldBubbleGeneratorModule()
        {
            MaxHealth = 70;
        }
    }
}
