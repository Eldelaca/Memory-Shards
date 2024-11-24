using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float groundDrag;
    public float airMultiplier;

    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    public Transform orientation;

    // Flash Var
    public GameObject flash;
    [SerializeField]
    private bool isFlashActive = false;
    private Coroutine flashCoroutine;

    private Vector2 moveInput;
    private Vector3 moveDirection;
    public Rigidbody rb;

    public PlayerControls inputActions;

    private void Awake()
    {
        inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Gameplay.Move.performed += OnMove;
        inputActions.Gameplay.Move.canceled += OnMove;
        inputActions.Gameplay.Flash.performed += OnFlash;
    }

    private void OnDisable()
    {
        inputActions.Gameplay.Move.performed -= OnMove;
        inputActions.Gameplay.Move.canceled -= OnMove;
        inputActions.Gameplay.Flash.performed -= OnFlash;
        inputActions.Disable();
    }

    // Move Script
    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // Camera Flash
    private void OnFlash(InputAction.CallbackContext context)
    {
        // Toggle the flash
        if (!isFlashActive) // Prevent overlapping activations
        {
            flashCoroutine = StartCoroutine(FlashRoutine());
        }
    }

    private IEnumerator FlashRoutine()
    {
        // Activate the flashlight and set it to active state
        isFlashActive = true;
        flash.SetActive(true);

        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);

        // Deactivate the flashlight and reset the state
        flash.SetActive(false);
        isFlashActive = false;

        // Clear the coroutine reference
        flashCoroutine = null;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        if (flash != null)
        {
            flash.SetActive(false);
        }
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        SpeedControl();

        // Handles drag
        rb.linearDamping = grounded ? groundDrag : 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * moveInput.y + orientation.right * moveInput.x;

        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    public Vector3 GetMoveDirection()
    {
        Vector3 moveDir = orientation.forward * moveInput.y + orientation.right * moveInput.x;

        if (moveDir.magnitude < 0.1f)
        {
            moveDir = orientation.forward; 
        }

        return moveDir.normalized; 
    }
}
