using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Constants
{
    public enum Upgrade
    {
        A_1_Rocket,
        A_2_Rocket,
        A_3_Rocket,
        B_1_Guns,
        B_2_Guns,
        B_3_Guns,
        C_1_Guns,
        C_2_Guns,
        C_3_Guns,
        D_1_Guns,
        D_2_Guns,
        D_3_Guns
    }

    public static class UpgradeHelpers
    {
        public static string GetName(Upgrade upgrade)
        {
            switch (upgrade)
            {

            }
            return "";
        }

        public static string GetDescription(Upgrade upgrade)
        {
            switch (upgrade)
            {

            }
            return "";
        }
    }
}
