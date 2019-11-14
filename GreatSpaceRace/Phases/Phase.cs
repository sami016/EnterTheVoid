using Forge.Core;
using Forge.Core.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Phases
{
    public abstract class Phase : Component
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public TimeSpan Duration { get; set; } = TimeSpan.FromSeconds(40);

        public virtual void Tick(TickContext context)
        {
        }
    }
}
