using EnterTheVoid.Flight;
using EnterTheVoid.General;
using Forge.Core.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.Ships.Modules
{
    /// <summary>
    /// Represents a type of ship module.
    /// </summary>
    public abstract class Module
    {
        public abstract string FullName { get; }
        public abstract string ShortName { get; }
        public abstract string DescriptionShort { get; }

        public virtual int MaxHealth { get; set; } = 100;

        public virtual void OnDestruction(FlightShip ship, FlightNode node)
        {
            var explosionEnt = ship.Entity.EntityManager.Create();
            explosionEnt.Add(new Transform() { Location = node.GlobalLocation });
            explosionEnt.Add(new ClusterExplosionEffect()
            {
                DistanceScaleFactor = 3f,
                ScaleFactor = 2f,
                Components = 16
            });
        }
    }
}
