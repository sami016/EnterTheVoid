using System;
using System.Collections.Generic;
using System.Text;
using Forge.Core.Components;
using Forge.Core.Sound;
using EnterTheVoid.Ships.Generation;

namespace EnterTheVoid.Phases.Asteroids
{
    public class IceAsteroidPhase : AsteroidPhase
    {
        private FrozenOverlay _overlay;
        [Inject] MusicManager MusicManager { get; set; }

        public IceAsteroidPhase(int difficulty, Distribution<Type> distribution) : base(difficulty, distribution)
        {
            Title = "Ice Field";
            Description = "Clear the ice field as quickly as possible.";
            CompleteMessage = "Ice field cleared.";
            Duration = null;
        }

        public override void Start()
        {
            MusicManager.Start("Ice");
            base.Start();
            _overlay = Entity.Create().Add(new FrozenOverlay());
        }

        public override void Stop()
        {
            base.Stop();
            _overlay.Fade();
        }
    }
}
