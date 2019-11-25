using EnterTheVoid.Ships.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.Ships.Generation
{
    public class ModuleWithConnectorDistribution
    {
        public ModuleWithConnectorDistribution(Type moduleType, ConnectionLayoutDistribution connectionLayoutDistribution)
        {
            ModuleType = moduleType;
            ConnectionLayoutDistribution = connectionLayoutDistribution;
        }

        public Type ModuleType { get; }
        public ConnectionLayoutDistribution ConnectionLayoutDistribution { get; }
    }
}
