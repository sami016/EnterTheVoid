using Forge.Core.Space;
using IntoTheVoid.Flight;
using IntoTheVoid.Ships.Generation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntoTheVoid.Phases.Asteroids
{
    public class AsteroidPhase : Phase
    {
        private PhaseDistanceTarget _phaseDistanceTarget;
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
            var targetEnt = Entity.Create();
            _phaseDistanceTarget = targetEnt.Add(new PhaseDistanceTarget(this, Entity.EntityManager.GetAll<FlightShip>().First(), 90f + 2f * _difficult / 5f));
            targetEnt.Add(new PhaseDistanceTargetRenderable());
            _asteroidSpawner = Entity.Create().Add(new AsteroidSpawner(_difficult, _distribution, _phaseDistanceTarget));
        }

        public override void Stop()
        {
            _asteroidSpawner.Stop();
            _asteroidSpawner.Entity.Delete();
            _phaseDistanceTarget.Entity.Delete();
        }

    }
}
