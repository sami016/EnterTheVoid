using Forge.Core.Components;
using Forge.Core.Interfaces;
using GreatSpaceRace.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Builder
{
    public class BuildFloor : Component, IInit
    {
        public void Initialise()
        {
            for (var gridX=0; gridX<5; gridX++)
            {
                for (var gridZ=0; gridZ<5; gridZ++)
                {
                    var gridPosition = new Point(gridX, gridZ);
                    var child = Entity.Create();
                    child.Add(new Transform
                    {
                        Location = HexagonHelpers.GetGridWorldPosition(gridPosition),
                        Rotation = Quaternion.CreateFromYawPitchRoll(0, 0, 0)
                    });
                    child.Add(new Floor(gridPosition));
                }
            }
        }
    }
}
