using System;
using Helpers;
using Interfaces;
using UnityEngine;

namespace EnemySystem
{
    public class Enemy : MonoBehaviour, IDamageable, IPoolable
    {
        [SerializeField] private GameObject visuals;
        private float _health = Utilities.BaseHealth;
        private float _armor = Utilities.BaseArmor;
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
    
        public void TakeDamage(float amount)
        {
            _health -= amount;
            if(_health <= 0)
                Die();
        }

        public void Die()
        {
            visuals.gameObject.SetActive(false);
            _patrolState = null;
            _attackState = null;
            _dieState = null;
            PoolController.Instance.EnqueueItemToPool(PoolableTypes.Enemy, this);
            EventBus.Trigger(new OnEnemyDie());
        }

        public void ReSpawn()
        {
            _health = Utilities.BaseHealth;
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
