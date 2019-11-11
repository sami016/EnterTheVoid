using GreatSpaceRace.Ships.Connections;
using GreatSpaceRace.Ships.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Ships.Generation
{
    public static class StandardGeneration
    {
        private static ConnectionLayoutDistribution _standardConnectionLayoutDistribution = new ConnectionLayoutDistribution
        {
            ( 10f, ConnectionLayouts.OnlyEast ),
            ( 1f, ConnectionLayouts.AccuteVA ),
            ( 1f, ConnectionLayouts.AccuteVB ),
            ( 1f, ConnectionLayouts.AccuteVSmallA ),
            ( 1f, ConnectionLayouts.AccuteVSmallB ),
            ( 1f, ConnectionLayouts.FullyConnected ),
            ( 1f, ConnectionLayouts.FullyConnectedLarge ),
            ( 1f, ConnectionLayouts.FullyConnectedSmall ),
            ( 1f, ConnectionLayouts.PassThrough ),
            ( 1f, ConnectionLayouts.PassThroughLarge ),
            ( 1f, ConnectionLayouts.PassThroughSmall ),
            ( 1f, ConnectionLayouts.PassThroughA ),
            ( 1f, ConnectionLayouts.PassThroughB ),
            ( 1f, ConnectionLayouts.SingleBoth ),
            ( 1f, ConnectionLayouts.SingleLarge ),
            ( 1f, ConnectionLayouts.SingleSmall ),
            ( 1f, ConnectionLayouts.WideVA ),
            ( 1f, ConnectionLayouts.WideVB ),
            ( 1f, ConnectionLayouts.WideVSmallA ),
            ( 1f, ConnectionLayouts.WideVSmallB )
        };

        public static ModuleDistribution Distribution { get; } = new ModuleDistribution()
            .Add(1f, typeof(LifeSupportModule), _standardConnectionLayoutDistribution)
            .Add(1f, typeof(BlasterModule), _standardConnectionLayoutDistribution);
    }
}
