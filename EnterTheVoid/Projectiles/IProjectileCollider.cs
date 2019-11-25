using Forge.Core.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterTheVoid.Projectiles
{
    public interface IProjectileCollider
    {
        void OnHit(Entity projectileEntity, ProjectileBase projectile);
    }
}
