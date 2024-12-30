using Helpers;
using Scriptables.Weapons;
using UnityEngine;

namespace Interfaces.WeaponStrategy
{
    public class PistolShootingStrategy : IShootingStrategy
    {
        public void Shoot(Vector3 target, WeaponConfig config)
        {
            var bullet = ObjectPool.Instance.GetAvailableBullet(BulletType.Pistol);
            bullet.Initialize(config);
            bullet.MoveTowardsTarget(target);
        }
    }
}
