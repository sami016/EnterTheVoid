using GreatSpaceRace.Ships.Connections;
using GreatSpaceRace.Ships.Modules;
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
        public Module Module { get; }
        public ConnectionLayout ConnectionLayout { get; }
    }
}
