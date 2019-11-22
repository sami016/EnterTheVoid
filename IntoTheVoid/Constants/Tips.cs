using System;
using System.Collections.Generic;
using System.Text;

namespace IntoTheVoid.Constants
{
    public static class Tips
    {
        public static string[] Items = new[]
        {
            "In build mode, you may shuffle the available modules if you get stuck by pressing \"Q\".",
            "Use the \"Q\" and \"E\" keys to rotate the ship. Rotation may be used for aiming and steering.",
            "Some asteroids can be brocket to release fuel ore. Collect these to process into additional fuel.",
            "Damage may be repaired during open stretches of space. Sections that have been destroyed cannot be repaired.",
            "Rotary engines can be used to increase your ships rate of rotation, making it easier to aim.",
            "Ship building is constrained by connector positions. Pay careful attention to these.",
            "Upgrade modules allow you to collect more upgrades during your journey. You will start with one of these.",
            "Mysterious transmissions for useful upgrade may be received during your journey and can be decoded at a price.",
            "Your journey will start from Earth and end at Pluto, with an upgrade satelite stationed in orbit at each planet along the way.",
        };

        public static string Sample()
        {
            return Items[new Random().Next(Items.Length)];
        }
    }
}
