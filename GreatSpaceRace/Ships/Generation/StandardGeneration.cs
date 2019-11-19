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
            ( 1f, ConnectionLayouts.HalfConnectedLarge ),
            ( 1f, ConnectionLayouts.HalfConnectedSmall ),
            ( 1f, ConnectionLayouts.HalfConnectedAlternatingA ),
            ( 1f, ConnectionLayouts.HalfConnectedAlternatingB ),
            ( 1f, ConnectionLayouts.HalfConnected ),
            ( 1f, ConnectionLayouts.OnlyEast ),
            ( 1f, ConnectionLayouts.OnlyWest ),
            ( 1f, ConnectionLayouts.OnlyNorthEast ),
            ( 1f, ConnectionLayouts.OnlyNorthWest ),
            ( 1f, ConnectionLayouts.OnlySouthEast ),
            ( 1f, ConnectionLayouts.OnlySouthWest ),
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


        private static ConnectionLayoutDistribution _gunConnectionLayoutDistribution = new ConnectionLayoutDistribution
        {
            ( 10f, ConnectionLayouts.OnlySouthEast ),
            ( 10f, ConnectionLayouts.OnlySouthWest ),
            ( 1f, ConnectionLayouts.OnlyEast ),
            ( 1f, ConnectionLayouts.OnlyWest ),
            ( 1f, ConnectionLayouts.PassThroughD1 ),
            ( 1f, ConnectionLayouts.PassThroughD2 ),
            ( 1f, ConnectionLayouts.PassThroughD3 ),
            ( 1f, ConnectionLayouts.PassThroughC1 ),
            ( 1f, ConnectionLayouts.PassThroughC2 ),
            ( 1f, ConnectionLayouts.PassThroughC3 )
        };


        private static ConnectionLayoutDistribution _rocketConnectionLayoutDistribution = new ConnectionLayoutDistribution
        {
            ( 10f, ConnectionLayouts.OnlyNorthEast),
            ( 10f, ConnectionLayouts.OnlyNorthWest),
            ( 1f, ConnectionLayouts.OnlyEast ),
            ( 1f, ConnectionLayouts.OnlyWest ),
            ( 1f, ConnectionLayouts.PassThroughD1 ),
            ( 1f, ConnectionLayouts.PassThroughD2 ),
            ( 1f, ConnectionLayouts.PassThroughD3 ),
            ( 1f, ConnectionLayouts.PassThroughC1 ),
            ( 1f, ConnectionLayouts.PassThroughC2 ),
            ( 1f, ConnectionLayouts.PassThroughC3 )
        };

        private static ConnectionLayoutDistribution _highConnectivityConnectionLayoutDistribution = new ConnectionLayoutDistribution
        {
            ( 1f, ConnectionLayouts.HalfConnectedLarge ),
            ( 1f, ConnectionLayouts.HalfConnectedSmall ),
            ( 1f, ConnectionLayouts.HalfConnectedAlternatingA ),
            ( 1f, ConnectionLayouts.HalfConnectedAlternatingB ),
            ( 1f, ConnectionLayouts.HalfConnected ),
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
            .Add(1f, typeof(BlasterModule), _gunConnectionLayoutDistribution)
            .Add(1f, typeof(FuelModule), _standardConnectionLayoutDistribution)
            .Add(1f, typeof(RocketModule), _rocketConnectionLayoutDistribution)
            .Add(0.5f, typeof(RotaryEngine), _standardConnectionLayoutDistribution)
            .Add(1f, typeof(EmptyModule), _highConnectivityConnectionLayoutDistribution)
            .Add(1f, typeof(EnergyModule), _standardConnectionLayoutDistribution);
    }
}
