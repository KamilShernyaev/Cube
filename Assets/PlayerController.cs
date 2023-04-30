using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 10f;

    private PlayerInputAction playerInputAction = null;
    private Rigidbody rb = null;
    private Vector3 directionMovement = Vector3.zero;

    private void Awake() 
    {
        playerInputAction = new PlayerInputAction();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable() 
    {
        playerInputAction.Enable();
        playerInputAction.Player.Movement.performed += PlayerInputAction_Movement;
        playerInputAction.Player.Movement.canceled += PlayerInputAction_Movement_Canceled;
    }

    private void OnDisable() 
    {
        playerInputAction.Disable();
        playerInputAction.Player.Movement.performed -= PlayerInputAction_Movement;
        playerInputAction.Player.Movement.canceled -= PlayerInputAction_Movement_Canceled;
    }

    private void FixedUpdate() 
    {
        Movement();
    }

    private void PlayerInputAction_Movement(InputAction.CallbackContext value)
    {
        directionMovement = value.ReadValue<Vector2>();
    }

    private void PlayerInputAction_Movement_Canceled(InputAction.CallbackContext value)
    {
        directionMovement = Vector2.zero;
    }

    private void Movement()
    {
        Vector3 moveDirection = new Vector3(directionMovement.x, 0, directionMovement.y).normalized;
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
        rb.velocity = moveDirection * moveSpeed;
    }
}
