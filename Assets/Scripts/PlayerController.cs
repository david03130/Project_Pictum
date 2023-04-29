using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public bool IsMoving { get; private set; }

    Vector2 moveInput;
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(1 * 1.5f * Time.deltaTime, 0, 0);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float x = moveInput.x * walkSpeed;
        float y = rb.velocity.y;
        rb.velocity = new Vector2(x, y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;
    }
}
