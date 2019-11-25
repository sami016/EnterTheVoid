using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.Upgrades
{
    public class HullReinforcement : UpgradeBase
    {
        public HullReinforcement()
        {
            Name = "Hull Reinforcement";
            Description = "Applies 10% damage reduction across the ship.";
            TextureResource = "ShieldComb";
        }
    }
}
