using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.Upgrades
{
    public class EfficientRocketry : UpgradeBase
    {
        public EfficientRocketry()
        {
            Name = "Efficient Rocketry";
            Description = "Each until of fuel yields an additional 10% rocket time.";
            TextureResource = "RocketFlight";
        }
    }
}
