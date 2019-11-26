using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.Upgrades
{
    public class ShieldAmplification : UpgradeBase
    {
        public ShieldAmplification()
        {
            Name = "Shield Amplification";
            Description = "Increases forcefield bubble shield radius by 50%.";
            TextureResource = "Bubble";
        }
    }
}
