using Helpers;
using Scriptables.Weapons;
using UnityEngine;

namespace Interfaces.WeaponStrategy
{
    public class RocketLauncherShootingStrategy : IShootingStrategy
    {
        public void Shoot(Vector3 target, WeaponConfig config, Transform initialPosition)
        {
            var bullet = PoolController.Instance.GetItemFromPool(PoolableTypes.BulletRocketLauncher) as Bullet;
            if (bullet != null)
            {
                bullet.transform.position = initialPosition.position;
                bullet.Initialize(config);
                bullet.MoveTowardsTarget(target);
            }
        }
    }
}