using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UI;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private Dictionary<BaseDamageable, BarHelper> _damageableIndicators =
        new Dictionary<BaseDamageable, BarHelper>();

    private void OnEnable()
    {
        AddListeners();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
        
    }

    private void RemoveListeners()
    {
        
    }
}
