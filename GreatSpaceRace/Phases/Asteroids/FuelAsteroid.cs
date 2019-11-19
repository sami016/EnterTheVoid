using Forge.Core.Components;
using Forge.Core.Engine;
using GreatSpaceRace.Projectiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Phases.Asteroids
{
    public class FuelAsteroid : Asteroid
    {
        public FuelAsteroid()
        {
            Health = 40;
        }
        public override void OnHit(Entity projectileEntity, Projectile projectile)
        {
            Entity.Delete();

            var fuelEnt = Entity.EntityManager.Create();
            fuelEnt.Add(new Transform
            {
                Location = Transform.Location
            });
            fuelEnt.Add(new Fuel());
        }
    }
}
