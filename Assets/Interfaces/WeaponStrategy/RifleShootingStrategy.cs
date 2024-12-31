using System.Collections;
using Helpers;
using Scriptables.Weapons;
using UnityEngine;

namespace Interfaces.WeaponStrategy
{
    public class RifleShootingStrategy : IShootingStrategy
    {
        public void Shoot(Vector3 target, WeaponConfig config)
        {
            CoroutineHandler.Instance.StartCoroutine(BurstBullets(target, config));
        }

        private IEnumerator BurstBullets(Vector3 target, WeaponConfig config)
        {
            for (int i = 0; i < 3; i++)
            {
                var bullet = PoolController.Instance.GetItemFromPool(PoolableTypes.BulletRifle) as Bullet;
                if (bullet != null)
                {
                    bullet.Initialize(config);
                    bullet.MoveTowardsTarget(target);
                    yield return new WaitForSeconds(0.15f);
                }
                else
                {
                    Debug.LogError("Pooled object is not of type Bullet.");
                }
            }
        }
    }
}
