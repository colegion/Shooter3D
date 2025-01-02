using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using Scriptables.Upgradeables;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class AttachmentSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ground;
    [SerializeField] private List<AttachmentConfig> attachmentConfigs;
    private List<Attachment> _spawnedAttachments = new List<Attachment>();
    private int _spawnCount = 3;
    
    private void OnEnable()
    {
        AddListeners();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    private void Start()
    {
        SpawnAttachments(_spawnCount);
    }

    private void SpawnAttachments(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var attachment = PoolController.Instance.GetItemFromPool(PoolableTypes.Attachment) as Attachment;
            if (attachment != null)
            {
                Vector3 spawnPosition = GetRandomPointOnGround();
                attachment.transform.position = spawnPosition;
                attachment.gameObject.SetActive(true);
                attachment.Initialize(GetRandomAttachmentConfig());
                _spawnedAttachments.Add(attachment);
            }
        }
    }
    
    private Vector3 GetRandomPointOnGround()
    {
        Bounds groundBounds = ground.GetComponent<Renderer>().bounds;
        float randomX = Random.Range(groundBounds.min.x, groundBounds.max.x);
        float randomZ = Random.Range(groundBounds.min.z, groundBounds.max.z);
        float groundY = ground.transform.position.y;
        return new Vector3(randomX, groundY, randomZ);
    }

    private AttachmentConfig GetRandomAttachmentConfig()
    {
        var index = Random.Range(0, attachmentConfigs.Count);
        return attachmentConfigs[index];
    }

    private void HandleOnAttachmentLooted(OnAttachmentLooted e)
    {
        SpawnAttachments(1);
    }
    
    private void AddListeners()
    {
        EventBus.Register<OnAttachmentLooted>(HandleOnAttachmentLooted);
    }

    private void RemoveListeners()
    {
        EventBus.Unregister<OnAttachmentLooted>(HandleOnAttachmentLooted);
    }
}
