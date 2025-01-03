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
        target.y = position.y; // Ensure y-axis alignment
        Vector3 direction = target - position;
        direction.y = 0; // Prevent vertical rotation

        if (direction.sqrMagnitude > 0.01f) // Avoid very small rotations
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }


    public void Shoot()
    {
        if (weapon == null) return;
        weapon.FireBullet(GetTargetDirection());
    }

    public bool IsAlreadyAttachedToCurrentWeapon(AttachmentConfig config)
    {
        return weapon.IsAttachmentExists(config);
    }

    public void ApplyUpgradeable(AttachmentConfig config)
    {
        weapon.ApplyAttachment(config);
    }
    
    private Vector3 GetTargetDirection()
    {
        // Raycast from mouse position to determine where the target is
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Check if we hit something on the ground (or another valid layer)
        int layerMask = LayerMask.GetMask("Ground");
    
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            Vector3 targetPosition = hit.point;
            targetPosition.y = transform.position.y; // Keep it level with the player

            // Calculate direction from player to the target position
            Vector3 direction = (targetPosition - transform.position).normalized;

            // Return direction scaled by the weapon range
            return direction * weapon.GetRange();
        }

        // If no hit, return the default forward direction
        return transform.forward * weapon.GetRange();
    }
    
    public void OnWeaponChanged(WeaponType type)
    {
        _currentWeaponConfig = GameController.Instance.GetWeaponConfigByType(type);
        weapon.Initialize(_currentWeaponConfig);
    }

    public override void Die()
    {
        visuals.SetActive(false);
        //ResetBars();
        EventBus.Trigger(new OnGameOver());
    }

    public override void ReSpawn()
    {
        visuals.SetActive(true);
        _health = Utilities.BaseHealth;
        _armor = Utilities.BaseArmor;
        weapon.ClearAttachments();
        OnWeaponChanged(WeaponType.Pistol);
        
    }
}
