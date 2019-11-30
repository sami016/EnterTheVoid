using Forge.Core;
using Forge.Core.Components;
using Forge.Core.Engine;
using EnterTheVoid.Flight;
using EnterTheVoid.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Forge.Core.Utilities;

namespace EnterTheVoid.AI
{
    public class DeathRunBehaviour
    {
        private readonly FlightShip _ship;
        private readonly FlightShip _playerShip;
        private readonly PositionChaserBehaviour _positionChaserBehaviour;
        private readonly CompletionTimer _despawnTimer = new CompletionTimer(TimeSpan.FromSeconds(2));

        public bool Running { get; private set; } = false;

        public float CatchupSpeed { get; set; } = 1f;
        public int SectionLowBound { get; set; } = 1;

        public DeathRunBehaviour(FlightShip ship, FlightShip playerShip, PositionChaserBehaviour positionChaserBehaviour)
        {
            _ship = ship;
            _playerShip = playerShip;
            _positionChaserBehaviour = positionChaserBehaviour;
        }

        public void Tick(TickContext context)
        {
            var count = _ship.Topology.AllSections.Count();
            if (count > 0 && count <= SectionLowBound)
            {
                if (!Running)
                {
                    _positionChaserBehaviour.CatchupSpeed = CatchupSpeed;
                    var runDirection = _ship.GetRandomNode().GlobalLocation - _playerShip.GetRandomNode().GlobalLocation;
                    runDirection.Normalize();
                    _positionChaserBehaviour.Target = _ship.Transform.Location + runDirection * 1000;
                    Running = true;
                }
            }
            if (Running)
            {
                _despawnTimer.Tick(context.DeltaTime);
                if (_despawnTimer.Completed)
                {
                    _ship.Entity.Delete();
                }
            }
        }
    }
}
