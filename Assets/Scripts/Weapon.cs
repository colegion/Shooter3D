using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using Interfaces.WeaponStrategy;
using Scriptables.Upgradeables;
using Scriptables.Weapons;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private MeshFilter weaponMesh;
    [SerializeField] private MeshRenderer weaponRenderer;

    private WeaponConfig _config;
    private IShootingStrategy _shootingStrategy;
    private float _lastFireTime;

    private readonly Dictionary<WeaponType, List<UpgradeableConfig>> _collectedAttachmentsByWeapon = 
        Enum.GetValues(typeof(WeaponType))
            .Cast<WeaponType>()
            .ToDictionary(weaponType => weaponType, _ => new List<UpgradeableConfig>());

    public void Initialize(WeaponConfig config)
    {
        _config = config;
        _shootingStrategy = GameController.Instance.GetStrategyByType(_config.weaponType);
        SetVisuals();
    }

    private void SetVisuals()
    {
        weaponMesh.mesh = _config.weaponMesh;
        weaponRenderer.material = _config.weaponMaterial;
    }

    public void FireBullet(Vector3 target)
    {
        if (CanFire())
        {
            _lastFireTime = Time.time;
            _shootingStrategy.Shoot(target, _config);
        }
    }

    public void ApplyAttachment(UpgradeableConfig attachmentData)
    {
        if (!_collectedAttachmentsByWeapon.TryGetValue(_config.weaponType, out var attachments))
        {
            attachments = new List<UpgradeableConfig>();
            _collectedAttachmentsByWeapon[_config.weaponType] = attachments;
        }

        if (attachments.Contains(attachmentData)) return;

        _config.ApplyEffect(attachmentData);
        attachments.Add(attachmentData);
    }

    
    private bool CanFire()
    {
        return Time.time >= _lastFireTime + (1f / _config.fireRate);
    }

    public float GetRange()
    {
        return _config.Range;
    }
}
