using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float interactRange = 1f;

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
    
        playerInputAction.Player.Interaction.performed +=  PlayerInputAction_Interaction;
    }

    private void OnDisable() 
    {
        playerInputAction.Disable();
        playerInputAction.Player.Movement.performed -= PlayerInputAction_Movement;
        playerInputAction.Player.Movement.canceled -= PlayerInputAction_Movement_Canceled;

        playerInputAction.Player.Interaction.performed -=  PlayerInputAction_Interaction;
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

    private void PlayerInputAction_Interaction(InputAction.CallbackContext context)
    {
        IInteractable interactable = GetInteractableObject();
        if(interactable!= null)
        {
            interactable.Interact(transform);
        }
    }

    private void Movement()
    {
        Vector3 moveDirection = new Vector3(directionMovement.x, 0, directionMovement.y).normalized;
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
        rb.velocity = moveDirection * moveSpeed;
    }

    public IInteractable GetInteractableObject()
    {
        List<IInteractable> interactableList = new List<IInteractable>();
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            if(collider.TryGetComponent(out IInteractable interactable))
            {
                interactableList.Add(interactable);
            }
        }

        IInteractable closesInteractable = null;
        foreach (IInteractable interactable in interactableList)
        {
            if(closesInteractable == null)
            {
                closesInteractable = interactable;
            }
            else
            {
                if((transform.position - interactable.GetTransform().position).sqrMagnitude < (transform.position - closesInteractable.GetTransform().position).sqrMagnitude)
                {
                    closesInteractable = interactable;
                }
            }
        }
        
        return closesInteractable;
    }
}
