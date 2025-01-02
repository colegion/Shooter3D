using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using Interfaces;
using Scriptables.Upgradeables;
using UnityEngine;

public class Attachment : MonoBehaviour, IPoolable
{
    [SerializeField] private Collider attachmentCollider;
    [SerializeField] private GameObject visuals;
    [SerializeField] private MeshFilter attachmentFilter;
    [SerializeField] private MeshRenderer attachmentRenderer;
    private AttachmentConfig _config;

    public void Initialize(AttachmentConfig config)
    {
        attachmentCollider.enabled = true;
        visuals.gameObject.SetActive(true);
        _config = config;
        attachmentFilter.mesh = _config.mesh;
        attachmentRenderer.material = _config.material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            if (player.IsAlreadyAttachedToCurrentWeapon(_config)) return;
            player.ApplyUpgradeable(_config);
            ResetSelf();
        }
    }

    private void ResetSelf()
    {
        attachmentCollider.enabled = false;
        _config = null;
        visuals.gameObject.SetActive(false);
        PoolController.Instance.EnqueueItemToPool(PoolableTypes.Attachment, this);
        EventBus.Trigger(new OnAttachmentLooted());
    }

    public void OnCreatedForPool()
    {
        attachmentCollider.enabled = false;
        visuals.gameObject.SetActive(false);
    }

    public void OnReleaseFromPool()
    {
        visuals.gameObject.SetActive(true);
    }

    public GameObject GameObject()
    {
        return gameObject;
    }
}
