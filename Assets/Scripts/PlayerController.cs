using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public event EventHandler OnMadnessChangedCallback;

    private PlayerInputAction playerInputAction = null;
    private PlayerAnimator playerAnimator;

    
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float interactRange = 1f;
    
    private Rigidbody rb = null;
    private Vector3 directionMovement = Vector3.zero;
    private bool isSelectedCharacter = false;

    [Header("Tired")] 
    [SerializeField] private float tiredLevel = 100f;
    private float defaultTiredLevel;
    private bool recreation = false;
    
    [Header("Madness")]
    [SerializeField] private int madnessLevel = 0;
    [SerializeField] private int maxMadnessLevel = 5;

    private void Awake() 
    {
        defaultTiredLevel = tiredLevel;
        playerInputAction = new PlayerInputAction();
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponentInChildren<PlayerAnimator>();
    }
    
    private void FixedUpdate() 
    {
        Movement();
    }
    private void Update() 
    {
        CharacterGetsTired();
        CharacterResting();
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

    private void PlayerInputAction_Movement(InputAction.CallbackContext value)
    {
        if(!isSelectedCharacter) return;

        directionMovement= value.ReadValue<Vector2>();
        playerAnimator.MovementAnimation(directionMovement);
    }

    private void PlayerInputAction_Movement_Canceled(InputAction.CallbackContext value)
    {
        if(!isSelectedCharacter) return;
        
        directionMovement = value.ReadValue<Vector2>();
        playerAnimator.MovementAnimation(directionMovement);
    }

    private void PlayerInputAction_Interaction(InputAction.CallbackContext context)
    {
        if(!isSelectedCharacter) return;

        IInteractable interactable = GetInteractableObject();
        if(interactable!= null)
        {
            interactable.Interact(transform);
        }
    }

    public void Movement()
    {
        if(!isSelectedCharacter) return;

        Vector3 moveDirection = new Vector3(directionMovement.x, 0, directionMovement.y).normalized;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
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

    public void CharacterGetsTired()
    {
        if(isSelectedCharacter)
        {
            tiredLevel -= 1 * Time.deltaTime;
            if(tiredLevel <= 0f)
            {
                GameManager.Instance.SetFatigueCharacter(this);
                recreation = true;
                directionMovement = Vector3.zero;
                playerAnimator.MovementAnimation(directionMovement);
                
            }
        }
    }

    private void AddMadness()
    {
        madnessLevel += 1;
        if (madnessLevel > maxMadnessLevel)
        {
            madnessLevel = maxMadnessLevel;

            if (OnMadnessChangedCallback != null)
                OnMadnessChangedCallback.Invoke(this, EventArgs.Empty);
        }   
    }

    private void CharacterResting()
    {
        if(recreation && !isSelectedCharacter)
        {
            tiredLevel += 1 *Time.deltaTime;
            if(tiredLevel >= defaultTiredLevel && recreation == true)
            {
                tiredLevel = defaultTiredLevel;
                recreation = false;
            }
        }
    }

    public bool GetRecreation()
    {
        return recreation;
    }

    public bool GetIsSelectedCharacter()
    {
        return isSelectedCharacter;
    }

    public float GetTiredLevel()
    {
        return tiredLevel;
    }

    public int GetMadnessLevel()
    {
        return madnessLevel;
    }

    public int GetMaxMadnessLevel()
    {
        return maxMadnessLevel;
    }

    public void SetIsSelectedCharacter(bool selectedPlayer)
    {
        isSelectedCharacter = selectedPlayer;
    }
}
