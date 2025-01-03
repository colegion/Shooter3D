using System.Collections;
using Helpers;
using Interfaces;
using UnityEngine;

namespace EnemySystem
{
    public class PatrolState : IEnemyState
    {
        private Vector3 _patrolDirection;
        private readonly float _patrolSpeed = 2f;
        private Coroutine _patrolCoroutine;
        private readonly float _detectionRadius = 10f;
        private readonly float _patrolTime = 5f;
        private float _directionChangeTimer;

        public void EnterState(Enemy enemy)
        {
            _patrolDirection = GetRandomDirection();
            _directionChangeTimer = Time.time + _patrolTime;
            _patrolCoroutine = CoroutineHandler.Instance.StartCoroutine(PatrolRoutine(enemy));
        }

        public void ExitState(Enemy enemy)
        {
            if (_patrolCoroutine != null)
            {
                CoroutineHandler.Instance.StopCoroutine(_patrolCoroutine);
                _patrolCoroutine = null;
            }
        }

        private Vector3 GetRandomDirection()
        {
            float randomAngle = Random.Range(0, 360) * Mathf.Deg2Rad;
            return new Vector3(Mathf.Cos(randomAngle), 0, Mathf.Sin(randomAngle)).normalized;
        }

        private bool ShouldChangeDirection()
        {
            return Time.time > _directionChangeTimer;
        }

        private IEnumerator PatrolRoutine(Enemy enemy)
        {
            while (true)
            {
                enemy.transform.Translate(_patrolDirection * (_patrolSpeed * Time.deltaTime), Space.World);
                
                if (_patrolDirection != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(_patrolDirection, Vector3.up);
                    enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRotation, Time.deltaTime * 5f);
                }
                
                if (ShouldChangeDirection())
                {
                    _patrolDirection = GetRandomDirection();
                    _directionChangeTimer = Time.time + _patrolTime;
                }
                
                Collider[] results = new Collider[10];
                int size = Physics.OverlapSphereNonAlloc(enemy.transform.position, _detectionRadius, results);
                for (int i = 0; i < size; i++)
                {
                    var hitCollider = results[i];

                    if (hitCollider.TryGetComponent(out Player player))
                    {
                        enemy.SetTarget(player);
                        enemy.SwitchState(enemy.GetState(StateType.Attack));
                        yield break;
                    }
                }

                yield return null;
            }
        }

    }
}
