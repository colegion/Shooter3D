using Helpers;
using Scriptables.Weapons;
using UnityEngine;

namespace Interfaces.WeaponStrategy
{
    public class PistolShootingStrategy : IShootingStrategy
    {
        public void Shoot(Vector3 target, WeaponConfig config)
        {
            var bullet = PoolController.Instance.GetItemFromPool(PoolableTypes.BulletPistol) as Bullet;
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
