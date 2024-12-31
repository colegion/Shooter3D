using System;
using System.Collections.Generic;
using EnemySystem;
using UnityEngine;

namespace Helpers
{
    public class EnemyPool : MonoBehaviour
    {
        [SerializeField] private Transform poolParent;
        private readonly int _poolAmount = 100;
        private Queue<Enemy> _pooledEnemies = new Queue<Enemy>();

        private void Awake()
        {
            PoolEnemies();
        }

        private void PoolEnemies()
        {
            for (int i = 0; i < _poolAmount; i++)
            {
            }
        }
    }
}
