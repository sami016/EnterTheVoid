﻿using IntoTheVoid.Ships.Modules;
using System;
using System.Text;

namespace IntoTheVoid.Ships.Generation
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