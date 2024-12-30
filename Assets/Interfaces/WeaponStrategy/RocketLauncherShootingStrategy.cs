using Helpers;
using Scriptables.Weapons;
using UnityEngine;

namespace Interfaces.WeaponStrategy
{
    public class RocketLauncherShootingStrategy : IShootingStrategy
    {
        public void Shoot(Vector3 target, WeaponConfig config)
        {
            var bullet = ObjectPool.Instance.GetAvailableBullet(BulletType.RocketLauncher);
            bullet.Initialize(config);
            bullet.MoveTowardsTarget(target);
        }
    }
}
