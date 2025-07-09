using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpFoce = 5;
    private Rigidbody rb;
    PlayerInput input;
    bool canJump = false;
    float moveDirection;
    [SerializeField] float moveSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        input = new PlayerInput();
        input.Runner.Enable();
        input.Runner.Jump.performed += Jump;
    }

    private void OnDestroy()
    {
        input.Runner.Jump.performed -= Jump;
    }

    private void Update()
    {
        moveDirection = input.Runner.Move.ReadValue<float>();
        if(moveDirection != 0)
        {
            rb.linearVelocity = new Vector3(moveDirection * moveSpeed, rb.linearVelocity.y, 0);
        }
        else
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }


    private void Jump(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!canJump) { return; }
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpFoce, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            canJump = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            canJump = false;
        }
    }
}