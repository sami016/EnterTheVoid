using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.Upgrades
{
    public class ShieldFortification : UpgradeBase
    {
        public ShieldFortification()
        {
            Name = "Shield Fortification";
            Description = "Increases forcefield bubble shield durability by 50%.";
            TextureResource = "Bubble";
        }
    }
}
