using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private bool faceMoveDirection;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 2f;      // match the Walk threshold in the blend tree
    [SerializeField] private float runSpeed = 6f;       // match the Run threshold in the blend tree
    [SerializeField] private float acceleration = 15f;  // how fast you speed up / slow down
    [SerializeField] private float jumpForce = 5f;

    [Header("Animation")]
    [SerializeField] private float animDampTime = 0.1f;

    private Animator animator;
    private Rigidbody rb;
    private Vector2 moveInput;
    private bool isSprinting;
    private bool isGrounded;

    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int JumpHash = Animator.StringToHash("Jump");
    private static readonly int AttackHash = Animator.StringToHash("Attack");
    private GameUI gameUI;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        gameUI = FindAnyObjectByType<GameUI>();
    }

    void Update()
    {
        UpdateAnimator();
    }

    // Hooked up in the Player Input component (Events > Player > Move)
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // Hooked up in the Player Input component (Events > Player > Sprint)
    public void Sprint(InputAction.CallbackContext context)
    {
        // true while the button is held, false once it is released
        isSprinting = context.ReadValueAsButton();
        Debug.Log($"Sprint: {isSprinting} ({context.phase})");
    }
    // Hooked up in the Player Input component (Events > Player > Jump)
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            if (animator != null)
            {
                animator.SetTrigger(JumpHash);
            }
        }
    }
    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded && animator != null)
        {
            animator.SetTrigger(AttackHash);
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

        // Walk or run, scaled by how far the stick is pushed (keyboard is always full)
        float inputAmount = Mathf.Clamp01(moveInput.magnitude);
        float targetSpeed = (isSprinting ? runSpeed : walkSpeed) * inputAmount;
        Vector3 targetVelocity = movement.normalized * targetSpeed;

        // Ease the horizontal velocity toward the target, keep vertical velocity for jumping/gravity
        Vector3 velocity = rb.linearVelocity;
        Vector3 horizontal = new Vector3(velocity.x, 0f, velocity.z);
        horizontal = Vector3.MoveTowards(horizontal, targetVelocity, acceleration * Time.fixedDeltaTime);

        rb.linearVelocity = new Vector3(horizontal.x, velocity.y, horizontal.z);

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

        // Send the real horizontal speed in m/s: 0 = idle, walkSpeed = walk, runSpeed = run
        Vector3 v = rb.linearVelocity;
        float horizontalSpeed = new Vector3(v.x, 0f, v.z).magnitude;

        animator.SetFloat(SpeedHash, horizontalSpeed, animDampTime, Time.deltaTime);
    }
    
}