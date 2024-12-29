using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Direction _currentDirection;
    private void OnEnable()
    {
        AddListeners();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    public void MoveToDirection(Direction direction)
    {
        _currentDirection = direction;
        var moveDirection = Utilities.DirectionVectors[_currentDirection];
        transform.Translate(moveDirection * (moveSpeed * Time.deltaTime), Space.World);
    }

    public void UpdateRotation(Vector3 target)
    {
        //transform.LookAt(target);
        Vector3 direction = target - transform.position;
        direction.y = 0;
        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    private void AddListeners()
    {
        
    }

    private void RemoveListeners()
    {
        
    }
}
