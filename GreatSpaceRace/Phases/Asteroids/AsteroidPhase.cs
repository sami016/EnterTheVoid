using Forge.Core.Space;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Phases.Asteroids
{
    public class AsteroidPhase : Phase
    {
        private AsteroidSpawner _asteroidSpawner;
        private readonly int _difficult;

        public AsteroidPhase(int difficulty = 10)
        {
            _difficult = difficulty;
            Title = "Asteroid Field";
            Description = "Clear the asteroid field as quickly as possible.";
            CompleteMessage = "Asteroid field cleared.";
        }

        public override void Start()
        {
            _asteroidSpawner = Entity.Create().Add(new AsteroidSpawner(_difficult));
        }

        public override void Stop()
        {
            _asteroidSpawner.Stop();
        }

        public override void Dispose()
        {
            _asteroidSpawner.Entity.Delete();
        }
    }
}
