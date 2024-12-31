using Helpers;
using Scriptables.Weapons;
using UnityEngine;

namespace Interfaces.WeaponStrategy
{
    public class RocketLauncherShootingStrategy : IShootingStrategy
    {
        public void Shoot(Vector3 target, WeaponConfig config)
        {
            var bullet = PoolController.Instance.GetItemFromPool(PoolableTypes.BulletRocketLauncher) as Bullet;
            if (bullet != null)
            {
                bullet.Initialize(config);
                bullet.MoveTowardsTarget(target);
            }
            else
            {
                Debug.LogError("Pooled object is not of type Bullet.");
            }
        }
    }
}
