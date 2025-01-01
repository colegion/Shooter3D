using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using Interfaces;
using Scriptables.Upgradeables;
using Scriptables.Weapons;
using UnityEngine;

public class Player : BaseDamageable
{
    [SerializeField] private GameObject visuals;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Weapon weapon;
    
    private Direction _currentDirection;
    private WeaponConfig _currentWeaponConfig;
    
    private void OnEnable()
    {
        AddListeners();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    public void Initialize()
    {
        OnWeaponChanged(WeaponType.Pistol);
    }

    public void MoveToDirection(Direction direction)
    {
        _currentDirection = direction;
        var moveDirection = Utilities.DirectionVectors[_currentDirection];
        transform.Translate(moveDirection * (moveSpeed * Time.deltaTime), Space.World);
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

    public void Shoot()
    {
        weapon.FireBullet(GetTargetDirection());
    }

    public bool IsAlreadyAttachedToCurrentWeapon(UpgradeableConfig config)
    {
        return weapon.IsAttachmentExists(config);
    }

    public void ApplyUpgradeable(UpgradeableConfig config)
    {
        weapon.ApplyAttachment(config);
    }
    
    private Vector3 GetTargetDirection()
    {
        Vector3 playerForward = transform.forward;
        return playerForward * weapon.GetRange();
    }

    public void OnWeaponChanged(WeaponType type)
    {
        _currentWeaponConfig = GameController.Instance.GetWeaponConfigByType(type);
        weapon.Initialize(_currentWeaponConfig);
    }

    public override void Die()
    {
        visuals.SetActive(false);
        EventBus.Trigger(new OnGameOver());
    }

    public override void ReSpawn()
    {
        visuals.SetActive(true);
        Reset();
    }

    private void AddListeners()
    {
        
    }

    private void RemoveListeners()
    {
        
    }
}
