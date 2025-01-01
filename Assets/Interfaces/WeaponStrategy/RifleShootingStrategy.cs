using System.Collections;
using Helpers;
using Scriptables.Weapons;
using UnityEngine;

namespace Interfaces.WeaponStrategy
{
    public class RifleShootingStrategy : IShootingStrategy
    {
        public void Shoot(Vector3 target, WeaponConfig config, Transform initialPosition)
        {
            CoroutineHandler.Instance.StartCoroutine(BurstBullets(target, config, initialPosition));
        }

        private IEnumerator BurstBullets(Vector3 target, WeaponConfig config, Transform initialPosition)
        {
            for (int i = 0; i < 3; i++)
            {
                var bullet = PoolController.Instance.GetItemFromPool(PoolableTypes.BulletRifle) as Bullet;
                if (bullet != null)
                {
                    bullet.transform.position = initialPosition.position;
                    bullet.Initialize(config);
                    bullet.MoveTowardsTarget(target);
                    yield return new WaitForSeconds(0.15f);
                }
            }
        }
    }
}