using System.Collections.Generic;
using Helpers;
using UnityEngine;

namespace EnemySystem
{
   public class EnemySpawner : MonoBehaviour
   {
      [SerializeField] private GameObject ground;
      private List<Enemy> _spawnedEnemies = new List<Enemy>();
      private int _spawnCount = 5;


      private void OnEnable()
      {
         AddListeners();
      }

      private void OnDisable()
      {
         RemoveListeners();
      }

      private void Start()
      {
         SpawnEnemies(_spawnCount);
      }

      private void SpawnEnemies(int count)
      {
         for (int i = 0; i < count; i++)
         {
            var enemy = PoolController.Instance.GetItemFromPool(PoolableTypes.Enemy) as Enemy;
            if (enemy != null)
            {
               Vector3 spawnPosition = GetRandomPointOnGround();
               enemy.transform.position = spawnPosition;
               enemy.gameObject.SetActive(true);
               enemy.Initialize();
               _spawnedEnemies.Add(enemy);
            }
         }
      }

      private Vector3 GetRandomPointOnGround()
      {
         Bounds groundBounds = ground.GetComponent<Renderer>().bounds;
      
         float randomX = UnityEngine.Random.Range(groundBounds.min.x, groundBounds.max.x);
         float randomZ = UnityEngine.Random.Range(groundBounds.min.z, groundBounds.max.z);
      
         float groundY = ground.transform.position.y;

         return new Vector3(randomX, groundY, randomZ);
      }

      private void HandleOnEnemyDie(OnEnemyDie e)
      {
         SpawnEnemies(1);
      }

      private void AddListeners()
      {
         EventBus.Register<OnEnemyDie>(HandleOnEnemyDie);
      }

      private void RemoveListeners()
      {
         EventBus.Unregister<OnEnemyDie>(HandleOnEnemyDie);
      }
   }
}
