using GreatSpaceRace.Ships.Connections;
using GreatSpaceRace.Ships.Modules;
using GreatSpaceRace.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Ships
{
    /// <summary>
    /// A ship section.
    /// </summary>
    public class Section
    {
        public int Rotation { get; private set; } = Direction.East;
        public Module Module { get; }
        public ConnectionLayout ConnectionLayout { get; }

        public Section(Module module, ConnectionLayout connectionLayout)
        {
            Module = module;
            ConnectionLayout = connectionLayout;
        }

        public void Rotate(int amount)
        {
            Rotation = ((Rotation) + amount) % 6;
        }
    }
}
