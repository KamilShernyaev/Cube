using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    
    private void Start() 
    {
        animator = GetComponent<Animator>();
    }

    public void MovementAnimation(Vector2 directionMovement)
    {
        animator.SetFloat("Horizontal", directionMovement.y);
        animator.SetFloat("Vertical", directionMovement.x);
    }
}
