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
        if (Input.GetKey(KeyCode.W))
        {
            _currentDirection = Direction.Forward;
            TriggerDirectionUpdate();
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            _currentDirection = Direction.Right;
            TriggerDirectionUpdate();
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            _currentDirection = Direction.Back;
            TriggerDirectionUpdate();
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            _currentDirection = Direction.Left;
            TriggerDirectionUpdate();
        }

        if (Input.GetMouseButton(0))
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
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            Vector3 targetPosition = hit.point;
            player.UpdateRotation(targetPosition);
        }
    }

    private void TriggerDirectionUpdate()
    {
        player.MoveToDirection(_currentDirection);
    }
}
