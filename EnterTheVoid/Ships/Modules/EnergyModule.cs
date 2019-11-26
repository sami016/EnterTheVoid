using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnterTheVoid.Flight;
using EnterTheVoid.General;
using EnterTheVoid.Utility;
using Forge.Core.Components;
using Forge.Core.Engine;

namespace EnterTheVoid.Ships.Modules
{
    class EnergyModule : Module
    {
        public override string FullName { get; } = "Energy Storage";

        public override string ShortName { get; } = "Energy Storage";

        public override string DescriptionShort { get; } = "Provides energy storage and generation.";
        public virtual int PassiveCapacity { get; set; } = 10;

        public EnergyModule()
        {
            MaxHealth = 10;
        }

        public override void OnDestruction(FlightShip ship, FlightNode node)
        {
            for (var direction = 0; direction < 6; direction++)
            {
                var gridLocation = HexagonHelpers.GetFromDirection(node.GridLocation, (int)direction);
                // Boom
                ship.Damage(gridLocation, 100);
            }
            
            var explosionEnt = ship.Entity.EntityManager.Create();
            explosionEnt.Add(new Transform() { Location = node.GlobalLocation });
            explosionEnt.Add(new ClusterExplosionEffect()
            {
                DistanceScaleFactor = 2f,
                ScaleFactor = 3f,
                Components = 16
            });
        }
    }
}
