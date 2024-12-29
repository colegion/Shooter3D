using System;
using System.Collections;
using System.Collections.Generic;
using Scriptables.Bullets;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    [SerializeField] private MeshFilter bulletMesh;
    [SerializeField] private MeshRenderer bulletRenderer;
    private BulletConfig _config;

    public void Initialize(BulletConfig config)
    {
        _config = config;
        SetVisuals();
    }

    private void SetVisuals()
    {
        bulletMesh.mesh = _config.bulletMesh;
        bulletRenderer.material = _config.bulletMaterial;
    }

    private void OnCollisionEnter(Collision other)
    {
        Explode();
    }
    

    public virtual void Explode()
    {
        
    }
}
