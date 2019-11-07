using Forge.Core.Components;
using Forge.Core.Interfaces;
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
            for (var gridX=0; gridX<3; gridX++)
            {
                for (var gridZ=0; gridZ<3; gridZ++)
                {
                    var child = Entity.Create();
                    child.Add(new Transform
                    {
                        Location = new Vector3(gridX, 0, gridZ),
                        Rotation = Quaternion.CreateFromYawPitchRoll(0, 0, 0)
                    });
                    child.Add(new Floor());
                }
            }
        }
    }
}
