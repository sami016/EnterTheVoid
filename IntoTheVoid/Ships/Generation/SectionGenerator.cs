using IntoTheVoid.Ships.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntoTheVoid.Ships.Generation
{
    public class SectionGenerator
    {
        private readonly ModuleDistribution _distribution;

        public SectionGenerator(ModuleDistribution distribution)
        {
            _distribution = distribution;
        }

        public Section Generate()
        {
            var moduleWithConnectorDist = _distribution.Sample();
            var moduleType = moduleWithConnectorDist.ModuleType;
            var connectorLayout = moduleWithConnectorDist.ConnectionLayoutDistribution.Sample();
            return new Section(Activator.CreateInstance(moduleType) as Module, connectorLayout);
        }
    }
}
