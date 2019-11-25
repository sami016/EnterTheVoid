using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.Upgrades
{
    public class ProficientRepair : UpgradeBase
    {
        public ProficientRepair()
        {
            Name = "ProficientRepair";
            Description = "Doubles the rate of repair capabilities.";
            TextureResource = "LightningRepair";
        }
    }
}
