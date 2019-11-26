using EnterTheVoid.Obstacles;
using EnterTheVoid.Ships.Generation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.Phases.Asteroids
{
    public class AsteroidDistributions
    {
        public static readonly Distribution<Type> MiningFieldDistribution = new Distribution<Type>()
        {
            (0.2f, typeof(Asteroid)),
            (1f, typeof(OreAsteroid))
        };
        public static readonly Distribution<Type> StandardAsteroidDistribution = new Distribution<Type>()
        {
            (1f, typeof(Asteroid)),
            (0.3f, typeof(OreAsteroid))
        };
        public static readonly Distribution<Type> IceAsteroidDistribution = new Distribution<Type>()
        {
            (1f, typeof(IceAsteroid)),
            (0.1f, typeof(OreAsteroid))
        };
    }
}
