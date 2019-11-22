using Forge.Core.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntoTheVoid.Projectiles
{
    public interface IProjectileCollider
    {
        void OnHit(Entity projectileEntity, Projectile projectile);
    }
}
