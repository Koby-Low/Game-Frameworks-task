using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private bool faceMoveDirection;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    private Animator animator;

    private Rigidbody rb;
    private Vector2 moveInput;
    private bool isGrounded;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        UpdateAnimator();
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("Jump");
        }
    }

    void FixedUpdate()
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 movement = forward * moveInput.y + right * moveInput.x;

        Vector3 velocity = rb.linearVelocity;

        velocity.x = movement.x * moveSpeed;
        velocity.z = movement.z * moveSpeed;

        rb.linearVelocity = velocity;

        if (faceMoveDirection && movement.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 10f * Time.fixedDeltaTime));
        }
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    void UpdateAnimator()
    {
        if (animator == null) return;

        //Convert movement input into a speed value (0 when idle, >0 when moving)
        float currentSpeed = new Vector3(moveInput.x, 0f, moveInput.y).magnitude;

        animator.SetFloat("Speed", currentSpeed);
    }
}
