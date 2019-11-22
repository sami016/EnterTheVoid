using GreatSpaceRace.Ships.Modules;
using System;
using System.Text;

namespace GreatSpaceRace.Ships.Generation
{
    public class ModuleDistribution : Distribution<ModuleWithConnectorDistribution>
    {
        public ModuleDistribution Add(float nonNormalizedChance, Type moduleType, ConnectionLayoutDistribution connectionLayoutDistribution)
        {
            Add((nonNormalizedChance, new ModuleWithConnectorDistribution(moduleType, connectionLayoutDistribution)));
            return this;
        }
    }
}
