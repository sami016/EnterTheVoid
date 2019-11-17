using Forge.Core.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatSpaceRace.Projectiles
{
    public interface IProjectileCollider
    {
        void OnHit(Entity projectileEntity, Projectile projectile);
    }
}
