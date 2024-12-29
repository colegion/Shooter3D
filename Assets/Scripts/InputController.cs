using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.Serialization;

public class InputController : MonoBehaviour
{
    [SerializeField] private Player player;
    private Direction _currentDirection;
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        
        GetMousePosition();
        if (Input.GetKeyDown(KeyCode.W))
        {
            _currentDirection = Direction.Forward;
            TriggerDirectionUpdate();
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            _currentDirection = Direction.Right;
            TriggerDirectionUpdate();
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            _currentDirection = Direction.Back;
            TriggerDirectionUpdate();
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            _currentDirection = Direction.Left;
            TriggerDirectionUpdate();
        }

        if (Input.GetMouseButtonDown(0))
        {
            player.Shoot();
        }

        for (int i = 0; i < Enum.GetValues(typeof(WeaponType)).Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                player.OnWeaponChanged((WeaponType)i);
            }
        }
    }

    private void GetMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(mousePosition);
        worldPosition.y = transform.position.y;
        player.UpdateRotation(worldPosition);
    }

    private void TriggerDirectionUpdate()
    {
        player.MoveToDirection(_currentDirection);
    }
}
