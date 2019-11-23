using Forge.Core.Space;
using IntoTheVoid.Ships.Generation;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntoTheVoid.Phases.Asteroids
{
    public class AsteroidPhase : Phase
    {
        private AsteroidSpawner _asteroidSpawner;
        private readonly int _difficult;
        private readonly Distribution<Type> _distribution;

        public AsteroidPhase(int difficulty, Distribution<Type> distribution)
        {
            _difficult = difficulty;
            _distribution = distribution;
            Title = "Asteroid Field";
            Description = "Clear the asteroid field as quickly as possible.";
            CompleteMessage = "Asteroid field cleared.";
        }

        public override void Start()
        {
            _asteroidSpawner = Entity.Create().Add(new AsteroidSpawner(_difficult, _distribution));
        }

        public override void Stop()
        {
            _asteroidSpawner.Stop();
            _asteroidSpawner.Entity.Delete();
        }

    }
}
