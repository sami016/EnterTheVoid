using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Upgrades
{
    public class EnhancedRotation : UpgradeBase
    {
        public EnhancedRotation()
        {
            Name = "Enhanced Rotation";
            Description = "Improves base rotation rate. Removes rotation penalty with larger ships";
            TextureResource = "Cog";
        }
    }
}
