using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using IPoolable = Interfaces.IPoolable;
using Object = UnityEngine.Object;

namespace Helpers
{
    public class PoolController : MonoBehaviour
    {
        public static PoolController Instance;

        private Dictionary<PoolableTypes, Queue<Object>> _pools =
            new Dictionary<PoolableTypes, Queue<Object>>();

        private Dictionary<PoolableTypes, Object> _prefabPools = new Dictionary<PoolableTypes, Object>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            foreach (PoolableTypes type in Enum.GetValues(typeof(PoolableTypes)))
            {
                var prefabPath = Utilities.PrefabPath + type;
                var prefab = Resources.Load<GameObject>(prefabPath); // Load as GameObject
                if (prefab == null)
                {
                    Debug.LogError($"Prefab not found at path: {prefabPath}");
                    continue;
                }

                if (prefab.GetComponent<IPoolable>() == null)
                {
                    Debug.LogError($"Prefab at path {prefabPath} does not implement IPoolable.");
                    continue;
                }

                CreatePool(type, prefab, 100, transform);
            }
        }

        public void CreatePool(PoolableTypes poolName, GameObject prefab, int amount, Transform poolParentObject)
        {
            if (_pools.ContainsKey(poolName))
            {
                var createdObject = Instantiate(prefab, poolParentObject, true);
                var poolable = createdObject.GetComponent<IPoolable>();
                if (poolable != null)
                {
                    EnqueueItemToPool(poolName, poolable);
                }
                else
                {
                    Debug.LogError($"Prefab {prefab.name} does not implement IPoolable.");
                }
            }
            else
            {
                Queue<IPoolable> pool = new Queue<IPoolable>();
                for (int i = 0; i < amount; i++)
                {
                    var createdObject = Instantiate(prefab, poolParentObject, true);
                    var poolable = createdObject.GetComponent<IPoolable>();
                    if (poolable != null)
                    {
                        pool.Enqueue(poolable);
                        poolable.OnCreatedForPool();
                        poolable.OnAssignPool();
                    }
                    else
                    {
                        Debug.LogError($"Prefab {prefab.name} does not implement IPoolable.");
                    }
                }

                _pools[poolName] = new Queue<Object>(pool.Cast<Object>());
                _prefabPools.Add(poolName, prefab);
            }
        }



        public IPoolable GetItemFromPool(PoolableTypes poolName)
        {
            if (_pools.ContainsKey(poolName))
            {
                if (_pools[poolName].TryDequeue(out Object result))
                {
                    var pooleable = result.GetComponent<IPoolable>();
                    if (pooleable != null)
                    {
                        pooleable.OnReleasePool();
                        return pooleable;
                    }
                    Debug.LogError("Dequeued object does not implement IPoolable.");
                    return null;
                }
                else
                {
                    var instantiatedObject = Instantiate(_prefabPools[poolName] as GameObject);
                    var pooleable = instantiatedObject.GetComponent<IPoolable>();
                    if (pooleable != null)
                    {
                        pooleable.OnReleasePool();
                        return pooleable;
                    }
                    Debug.LogError("Instantiated object does not implement IPoolable.");
                    return null;
                }
            }
            else
            {
                Debug.LogError("Pool doesn't exist.");
                return null;
            }
        }


        public void EnqueueItemToPool(PoolableTypes poolName, IPoolable item)
        {
            if (_pools.ContainsKey(poolName))
            {
                item.OnAssignPool();
                _pools[poolName].Enqueue(item as Object);
            }
            else
            {
                Debug.LogError("Pool doesn't exist.");
            }
        }

    }
}