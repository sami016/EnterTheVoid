using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.Upgrades
{
    public class OreProcessing : UpgradeBase
    {
        public OreProcessing()
        {
            Name = "High Yield Ore Processing";
            Description = "Doubles energy yield from processing ore.";
            TextureResource = "OilDrum";
        }
    }
}
