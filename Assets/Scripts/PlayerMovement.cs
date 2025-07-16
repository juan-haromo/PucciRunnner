using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpFoce = 5;
    private Rigidbody rb;
    PlayerInput input;
    bool canJump = false;
    float moveDirection;
    [SerializeField] float moveSpeed;
    [SerializeField] Animator animator;
    [SerializeField, Range(1,3)] float animationSpeedMultiplier = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        input = new PlayerInput();
        input.Runner.Enable();
        input.Runner.Jump.performed += Jump;
        input.Runner.Flip.performed += Flip;
        StartCoroutine(SpeedUp());
    }

    private void OnDestroy()
    {
        input.Runner.Jump.performed -= Jump;
        input.Runner.Flip.performed -= Flip;
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
        animator.SetFloat("Speed",moveDirection * animationSpeedMultiplier);
        animator.SetBool("IsCrawling", input.Runner.Crawl.IsPressed());
    }


    private void Jump(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!canJump) { return; }
        animator.SetTrigger("Jump");
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

    IEnumerator SpeedUp()
    {
        while (animationSpeedMultiplier < 3)
        {
            yield return new WaitForSeconds(20);
            animationSpeedMultiplier += 1;
        }
    }

    public void Flip(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!canJump) { return; }
        Physics.gravity *= -1;
        transform.Rotate(Vector3.left, 180);
    }
}