using System.Collections;
using Helpers;
using Interfaces;
using UnityEngine;

namespace EnemySystem
{
    public class AttackState : IEnemyState
    {
        private Player _target;
        private Coroutine _shootRoutine;
        private readonly float _maxRange = 20f;

        public void SetTarget(Player target)
        {
            _target = target;
        }

        public void EnterState(Enemy enemy)
        {
            if (_target == null)
            {
                enemy.SwitchState(enemy.GetState(StateType.Patrol));
                return;
            }
            _shootRoutine = CoroutineHandler.Instance.StartCoroutine(ShootingRoutine(enemy));
        }

        private IEnumerator ShootingRoutine(Enemy enemy)
        {
            var config = enemy.GetWeaponConfig();

            while (true)
            {
                var position = _target.transform.position;
                enemy.Shoot();
                
                var distance = Vector3.Distance(position, enemy.GameObject().transform.position);
                if (distance > _maxRange)
                {
                    break;
                }

                yield return new WaitForSeconds(config.fireRate);
            }

            enemy.SwitchState(enemy.GetState(StateType.Patrol));
        }

        public void ExitState(Enemy enemy)
        {
            StopAttacking();
        }

        public void StopAttacking()
        {
            if (_shootRoutine != null)
            {
                CoroutineHandler.Instance.StopCoroutine(_shootRoutine);
                _shootRoutine = null;
                _target = null;
            }
        }
    }
}