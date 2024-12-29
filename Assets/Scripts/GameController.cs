using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using Interfaces.WeaponStrategy;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private StrategyPool _strategyPool;
    private static GameController _instance;

    public static GameController Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameObject("GameController").AddComponent<GameController>();
            return _instance;
        }
    }

    private void Awake()
    {
        _strategyPool = new StrategyPool();
        _strategyPool.PoolStrategies();
    }

    public IShootingStrategy GetStrategyByType(WeaponType type)
    {
        return _strategyPool.GetStrategyByType(type);
    }
}
