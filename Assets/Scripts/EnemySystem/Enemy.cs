using System;
using Helpers;
using Interfaces;
using UnityEngine;

namespace EnemySystem
{
    public class Enemy : BaseDamageable, IPoolable
    {
        [SerializeField] private GameObject visuals;
        private IEnemyState _currentState;
        private PatrolState _patrolState;
        private AttackState _attackState;
        private DieState _dieState;

        public void Initialize()
        {
            _patrolState = new PatrolState();
            _attackState = new AttackState();
            _dieState = new DieState();
            
            SwitchState(_patrolState);
        }

        public void SwitchState(IEnemyState newState)
        {
            _currentState?.ExitState(this);
            _currentState = newState;
            _currentState.EnterState(this);
        }
        

        public override void Die()
        {
            visuals.gameObject.SetActive(false);
            _patrolState = null;
            _attackState = null;
            _dieState = null;
            PoolController.Instance.EnqueueItemToPool(PoolableTypes.Enemy, this);
            EventBus.Trigger(new OnEnemyDie());
        }

        public override void ReSpawn()
        {
            Reset();
            visuals.gameObject.SetActive(true);
        }

        public void OnCreatedForPool()
        {
            visuals.gameObject.SetActive(false);
        }
        
        public void OnReleasePool()
        {
            visuals.gameObject.SetActive(true);
        }
        
        public GameObject GameObject()
        {
            return gameObject;
        }
    }
}
