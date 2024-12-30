using System;
using System.Collections;
using System.Collections.Generic;
using Scriptables.Upgradeables;
using UnityEngine;

public class Upgradeable : MonoBehaviour
{
    [SerializeField] private GameObject visuals;
    private UpgradeableConfig _config;

    public void Initialize(UpgradeableConfig config)
    {
        visuals.gameObject.SetActive(true);
        _config = config;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            player.ApplyUpgradeable(_config);
            ResetSelf();
        }
    }

    private void ResetSelf()
    {
        _config = null;
        visuals.gameObject.SetActive(false);
    }
}
