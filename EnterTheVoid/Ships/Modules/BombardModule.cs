using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.Ships.Modules
{
    class BombardModule : Module
    {
        public override string FullName { get; } = "Bombard Launcher";

        public override string ShortName { get; } = "Bombard Launcher";

        public override string DescriptionShort { get; } = "A heavyweight single directional rocket launcher weapon module.";
    }
}
