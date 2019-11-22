using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Upgrades
{
    public class BlastRocketry : UpgradeBase
    {
        public BlastRocketry()
        {
            Name = "Blast Rocketry";
            Description = "Enabled blast attack from rockets, a powerful but slow attack.";
            TextureResource = "PlasmaBolt";
        }
    }
}
