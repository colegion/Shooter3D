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

    private readonly Dictionary<WeaponType, List<AttachmentConfig>> _collectedAttachmentsByWeapon = 
        Enum.GetValues(typeof(WeaponType))
            .Cast<WeaponType>()
            .ToDictionary(weaponType => weaponType, _ => new List<AttachmentConfig>());

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
            _shootingStrategy.Shoot(target, _config, transform);
        }
    }

    public void ApplyAttachment(AttachmentConfig attachmentData)
    {
        if (!_collectedAttachmentsByWeapon.TryGetValue(_config.weaponType, out var attachments))
        {
            attachments = new List<AttachmentConfig>();
            _collectedAttachmentsByWeapon[_config.weaponType] = attachments;
        }

        if (attachments.Contains(attachmentData)) return;

        _config.ApplyEffect(attachmentData);
        attachments.Add(attachmentData);
    }
    
    private bool CanFire()
    {
        if (_config == null) return false;
        return Time.time >= _lastFireTime + (1f / _config.fireRate);
    }

    public bool IsAttachmentExists(AttachmentConfig attachmentData)
    {
        if (_collectedAttachmentsByWeapon.TryGetValue(_config.weaponType, out var attachments))
        {
            return attachments.Contains(attachmentData);
        }

        return false;
    }

    public void ClearAttachments()
    {
        foreach (var key in _collectedAttachmentsByWeapon.Keys.ToList())
        {
            _collectedAttachmentsByWeapon[key].Clear();
        }
    }


    public float GetRange()
    {
        return _config == null ? 0f : _config.Range;
    }
}
