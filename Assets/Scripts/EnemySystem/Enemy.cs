using System;
using Helpers;
using Interfaces;
using UnityEngine;

namespace EnemySystem
{
    public class Enemy : BaseDamageable, IPoolable
    {
        [SerializeField] private Collider enemyCollider;
        [SerializeField] private GameObject visuals;
        private IEnemyState _currentState;
        private PatrolState _patrolState;
        private AttackState _attackState;
        private DieState _dieState;
        
        private float _health = Utilities.BaseHealth;
        private float _armor = Utilities.BaseArmor;
        
        protected override float Health
        {
            get => _health;
            set => _health = value;
        }

        protected override float Armor
        {
            get => _armor;
            set => _armor = value;
        }

        public void Initialize()
        {
            _patrolState = new PatrolState();
            _attackState = new AttackState();
            _dieState = new DieState();
            enemyCollider.enabled = true;
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
            _health = Utilities.BaseHealth;
            _armor = Utilities.BaseArmor;
            visuals.gameObject.SetActive(true);
            Initialize();
        }

        public void OnCreatedForPool()
        {
            enemyCollider.enabled = false;
            visuals.gameObject.SetActive(false);
        }
        
        public void OnReleasePool()
        {
            enemyCollider.enabled = true;
            visuals.gameObject.SetActive(true);
        }
        
        public GameObject GameObject()
        {
            return gameObject;
        }
    }
}