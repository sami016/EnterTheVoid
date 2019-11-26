using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.Upgrades
{
    public class BombardOverload : UpgradeBase
    {
        public BombardOverload()
        {
            Name = "Bombard Overload";
            Description = "Doubles the number of rockets deployed, but reduces accuracy.";
            TextureResource = "Rocket";
        }
    }
}
