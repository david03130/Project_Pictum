using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Assets.Scripts.Constants;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 0.2f;
    public float runSpeed = 0.5f;

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
            _animator.SetBool(AnimatorStrings.IsMoving, value);
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
            _animator.SetBool(AnimatorStrings.IsRunning, value);
        }
    }


    private bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        set
        {
            if (_isFacingRight != value)
            {
                // The reasoon we flip the entire GameObject is bc that's going to make flipping the child elements
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
        IsMoving = true;
        IsRunnign = true;
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
    }

    #endregion
}
