using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Assets.Scripts.Constants;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{
    // I'm setting these fields in the editor Player prefab.
    public float walkSpeed;
    public float runSpeed;
    public float jumpImpulse;
    TouchingDirections touchingDirections;
    //public float airWalkSpeed;

    // These properties also set the animation states inside their setter.
    [SerializeField]
    private bool _isMoving = false;
    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        set
        {
            _isMoving = value;
            animator.SetBool(AnimatorStrings.IsMoving, value);
        }
    }

    [SerializeField]
    private bool _isRunnign = false;
    public bool IsRunnign
    {
        get
        {
            return _isRunnign;
        }
        set
        {
            _isRunnign = value;
            animator.SetBool(AnimatorStrings.IsRunning, value);
        }
    }

    [SerializeField]
    private bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        set
        {
            if (_isFacingRight != value)
            {
                // The reason we flip the entire GameObject is bc that's going to make flipping the child elements
                // of the object much easier than if we just flipped the sprite alone.
                transform.localScale = new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    public float CurrentMoveSpeed
    {
        get
        {
            if (IsMoving && !touchingDirections.IsOnWall)
            {
                if (IsRunnign)
                {
                    return runSpeed;
                }
                else
                {
                    return walkSpeed;
                }
            }
            else
            {
                // Iddle speed is 0.
                return 0;
            }
        }
    }

    //Vector2 moveInput;
    Rigidbody2D rigidBody;
    Animator animator;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        IsMoving = true;
        IsRunnign = true;
    }

    private void FixedUpdate()
    {
        float x = CurrentMoveSpeed;
        float y = rigidBody.velocity.y;
        rigidBody.velocity = new Vector2(x, y);

        animator.SetFloat(AnimatorStrings.yVelocity, rigidBody.velocity.y);
    }


    #region Events
    // De normal esta función no se va a ejecutar pk el movimiento es automático.
    public void OnMove(InputAction.CallbackContext context)
    {
        //moveInput = context.ReadValue<Vector2>();
        //IsMoving = moveInput != Vector2.zero;
    }

    // De normal esta función no se va a ejecutar pk el movimiento es automático.
    public void OnRun(InputAction.CallbackContext context)
    {
        //if (context.started)
        //{
        //    IsRunnign = true;
        //}
        //else if (context.canceled)
        //{
        //    IsRunnign = false;
        //}
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // TODO: Check if alive aswell
        if (context.started && touchingDirections.IsGrounded)
        {
            animator.SetTrigger(AnimatorStrings.Jump);
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpImpulse);
        }
    }

    #endregion
}
