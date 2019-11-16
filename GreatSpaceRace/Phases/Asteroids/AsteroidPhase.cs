using Forge.Core.Space;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Phases.Asteroids
{
    public class AsteroidPhase : Phase
    {
        private AsteroidSpawner _asteroidSpawner;

        public AsteroidPhase()
        {
            Title = "Asteroid Field";
            Description = "Clear the asteroid field as quickly as possible.";
            CompleteMessage = "Asteroid field cleared.";
        }

        public override void Start()
        {
            _asteroidSpawner = Entity.Create().Add(new AsteroidSpawner());
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
