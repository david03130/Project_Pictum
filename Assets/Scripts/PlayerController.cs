using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 1.5f;
    public float runSpeed = 2f;

    [SerializeField]
    private bool _isMoving = false;
    // IsMoving also sets the animation bool inside
    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        set
        {
            _isMoving = value;
            _animator.SetBool("isMoving", value);
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
            _animator.SetBool("isRunning", value);
        }
    }

    public float CurrentMoveSpeed
    {
        get
        {
            if (IsMoving)
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

    Vector2 moveInput;
    Rigidbody2D rb;
    Animator _animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        float x = CurrentMoveSpeed;
        float y = rb.velocity.y;
        rb.velocity = new Vector2(x, y);
        //transform.Translate(x, y, 0);
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
        // TODO: Change this to actual jumping functionality.
        if (context.started)
        {
            IsMoving = true;
            IsRunnign = true;
        }
        // else if (context.canceled)
        // {
        //     IsRunnign = false;
        // }
    }

    #endregion
}
