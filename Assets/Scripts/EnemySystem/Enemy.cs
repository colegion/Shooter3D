using System;
using System.Collections.Generic;
using Helpers;
using Interfaces;
using Scriptables.Weapons;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemySystem
{
    public class Enemy : BaseDamageable, IPoolable
    {
        [SerializeField] private Collider enemyCollider;
        [SerializeField] private GameObject visuals;
        [SerializeField] private Weapon weapon;
        private IEnemyState _currentState;
        private PatrolState _patrolState;
        private AttackState _attackState;
        
        private float _health = Utilities.BaseHealth;
        private float _armor = Utilities.BaseArmor;
        private WeaponConfig _weaponConfig;
        private Dictionary<StateType, IEnemyState> _statesByType = new Dictionary<StateType, IEnemyState>();
        private Player _target;
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
            var randomIndex = Random.Range(0, Enum.GetValues(typeof(WeaponType)).Length);
            _weaponConfig = GameController.Instance.GetWeaponConfigByType((WeaponType)randomIndex);
            weapon.Initialize(_weaponConfig);
            if (_patrolState == null)
                _patrolState = new PatrolState();
            if (_attackState == null)
                _attackState = new AttackState();
            
            _statesByType.Add(StateType.Patrol, _patrolState);
            _statesByType.Add(StateType.Attack, _attackState);

            ResetBars();
            SwitchState(_patrolState);
            enemyCollider.enabled = true;
            SwitchState(_patrolState);
        }

        private void Update()
        {
            if (_target == null) return;
            UpdateRotation(_target.transform.position);
        }

        public void SwitchState(IEnemyState newState)
        {
            _currentState?.ExitState(this);
            _currentState = newState;
            _currentState.EnterState(this);
        }
        
        public void Shoot()
        {
            weapon.FireBullet(GetTargetDirection());
        }
        
        private Vector3 GetTargetDirection()
        {
            Vector3 enemyForward = transform.forward;
            return enemyForward * weapon.GetRange();
        }

        public void UpdateRotation(Vector3 target)
        {
            var position = transform.position;
            target.y = position.y;
            Vector3 direction = target - position;
            direction.y = 0;
            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
        
        public override void Die()
        {
            enemyCollider.enabled = false;
            visuals.gameObject.SetActive(false);
            _patrolState = null;
            _attackState.StopAttacking();
            _attackState = null;
            _statesByType.Clear();
            _target = null;
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
            visuals.gameObject.SetActive(false);
        }
        
        public void OnReleaseFromPool()
        {
            enemyCollider.enabled = true;
            visuals.gameObject.SetActive(true);
        }
        
        public GameObject GameObject()
        {
            return gameObject;
        }

        public WeaponConfig GetWeaponConfig()
        {
            return _weaponConfig;
        }

        public void SetTarget(Player player)
        {
            _target = player;
            _attackState.SetTarget(player);
        }

        public IEnemyState GetState(StateType type)
        {
            return _statesByType[type];
        }
    }
}