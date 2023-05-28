using Assets.Scripts.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class Bolita : MonoBehaviour
{
    public DetectionZone playerDetectionZone;
    public DetectionZone cliffDetectionZone;
    public float walkSpeed;
    Rigidbody2D rigidBody;
    Animator animator;

    public enum WalkableDirection
    {
        Right,
        Left
    }

    private Vector2 walkDirectionVector = Vector2.right;
    TouchingDirections touchingDirections;

    private WalkableDirection _walkDirection;
    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                walkDirectionVector = value == WalkableDirection.Right ? Vector2.right : Vector2.left;
            }
            _walkDirection = value;
        }
    }

    private bool _hasTarget = false;

    public bool HasTarget
    {
        get { return _hasTarget; }
        set
        {
            _hasTarget = value;
            animator.SetBool(AnimatorStrings.HasTarget, value);
        }
    }


    void Update()
    {
        HasTarget = playerDetectionZone.detectedColliders.Count > 0;
    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall || cliffDetectionZone.detectedColliders.Count == 0)
        {
            FlipDirection();
        }

        rigidBody.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rigidBody.velocity.y);
    }

    private void FlipDirection()
    {
        WalkDirection = WalkDirection == WalkableDirection.Right ? WalkableDirection.Left : WalkableDirection.Right;
    }
}
