using GreatSpaceRace.Ships.Generation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Phases.Asteroids
{
    public class AsteroidDistributions
    {
        public static readonly Distribution<Type> StandardAsteroidDistribution = new Distribution<Type>()
        {
            (1f, typeof(Asteroid))
        };
        public static readonly Distribution<Type> IceAsteroidDistribution = new Distribution<Type>()
        {
            (1f, typeof(IceAsteroid))
        };
    }
}
