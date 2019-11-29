using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.Ships.Modules
{
    class ArmourModule : Module
    {
        public override string FullName { get; } = "Armour Module";

        public override string ShortName { get; } = "Armour Module";

        public override string DescriptionShort { get; } = "A reinforced module provide armour in all directions.";

        public ArmourModule()
        {
            MaxHealth = 220;
        }
    }
}
