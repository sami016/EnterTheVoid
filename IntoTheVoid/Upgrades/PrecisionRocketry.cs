using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Upgrades
{
    public class PrecisionRocketry : UpgradeBase
    {
        public PrecisionRocketry()
        {
            Name = "Precision Rocketry";
            Description = "Improved rocket control and deceleration.";
            TextureResource = "RocketThruster";
        }
    }
}
