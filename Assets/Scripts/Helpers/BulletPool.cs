using System;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class BulletPool : MonoBehaviour
    {
        /*private readonly int _poolAmount = 100;
        private static Dictionary<BulletType, Queue<Bullet>> _pooledBullets;

        private static BulletPool _instance;

        public static BulletPool Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("BulletPool").AddComponent<BulletPool>();
                }

                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
            _pooledBullets = new Dictionary<BulletType, Queue<Bullet>>();
            PoolBullets();
        }

        private void PoolBullets()
        {
            foreach (BulletType type in Enum.GetValues(typeof(BulletType)))
            {
                var queue = new Queue<Bullet>();
                var prefab = Resources.Load<Bullet>(Utilities.BulletPath + type);

                if (prefab == null)
                {
                    Debug.LogError($"Bullet prefab for type {type} not found at path {Utilities.BulletPath + type}");
                    continue;
                }

                for (int i = 0; i < _poolAmount; i++)
                {
                    var tempBullet = Instantiate(prefab, transform);
                    queue.Enqueue(tempBullet);
                }

                _pooledBullets[type] = queue;
            }
        }

        public Bullet GetAvailableBullet(BulletType type)
        {
            if (_pooledBullets.TryGetValue(type, out Queue<Bullet> queue))
            {
                if (queue.Count > 0)
                {
                    return queue.Dequeue();
                }

                Debug.LogWarning(
                    $"No bullets available for {type}. Consider expanding the pool or handling this case.");
                return null;
            }

            Debug.LogError($"Bullet pool for type {type} not found.");
            return null;
        }

        public void ReturnBullet(Bullet bullet, BulletType type)
        {
            if (_pooledBullets.ContainsKey(type))
            {
                _pooledBullets[type].Enqueue(bullet);
            }
            else
            {
                Debug.LogError($"Bullet pool for type {type} not found. Returning bullet failed.");
            }
        }*/
    }
}