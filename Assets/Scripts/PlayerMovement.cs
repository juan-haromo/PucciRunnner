using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpFoce = 5;
    [SerializeField] Rigidbody rb;
    PlayerInput input;
    bool canJump = false;
    float moveDirection;
    [SerializeField] float moveSpeed;
    [SerializeField] Animator animator;
    [SerializeField, Range(1, 3)] float animationSpeedMultiplier = 1f;
    [SerializeField] Material background;
    [SerializeField] Material ground;
    [SerializeField] float backgroundSpeed = 0;

    void Start()
    {

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
        input.Runner.Disable();
        background.SetFloat("_Speed", 0);
        ground.SetFloat("_Speed", 0);
        ScoreManager.Instance?.Lose();
    }

    private void Update()
    {
        moveDirection = input.Runner.Move.ReadValue<float>();
        if (moveDirection != 0)
        {
            rb.linearVelocity = new Vector3(moveDirection * moveSpeed, rb.linearVelocity.y, 0);
        }
        else
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
        animator.SetFloat("Speed", moveDirection * animationSpeedMultiplier);
        Crawl();
        backgroundSpeed += Time.deltaTime * 0.01f;
        background.SetFloat("_Speed", backgroundSpeed);
        ground.SetFloat("_Speed", -backgroundSpeed);
    }

    void Crawl()
    {
        if (!canJump) { return; }
        animator.SetBool("IsCrawling", input.Runner.Crawl.IsPressed());
    }

    public void Jump()
    {
        animator.SetTrigger("Jump");
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        rb.AddForce(transform.up * jumpFoce, ForceMode.Impulse);
    }

    private void Jump(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!canJump) { return; }
        Jump();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            canJump = true;
            animator.SetBool("Grounded", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            canJump = false;
            animator.SetBool("Grounded", false);
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
    public void Flip()
    {
        Physics.gravity *= -1;
        StartCoroutine(FlipModel());
    }

    public void Flip(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!canJump) { return; }
        Flip();
    }

    IEnumerator FlipModel()
    {
        float i = 0;
        while (i < 180)
        {
            yield return new WaitForEndOfFrame();
            transform.Rotate(Vector3.forward, 150 * Time.deltaTime);
            i += 150 * Time.deltaTime;

        }

    }
}