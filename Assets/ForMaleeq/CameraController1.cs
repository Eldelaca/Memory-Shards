using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Camera FirstPersonPov;
    public CameraControls cameraControls;

    public float sensX = 15f;
    public float sensY = 15f;


    private float xRotation = 0f; 
    private float yRotation = 0f; 

    // For looking
    private Vector2 lookInput;

    // For Moving
    public float moveSpeed;
    private Vector2 moveInput;
    private Vector3 moveDirection;

    // Grounded value
    public bool grounded; // This can be used universally
    public float airMultiplier;
    public float groundDrag;
    public float playerHeight = 2f; // For Height of player
    public LayerMask whatIsGround; // Layer for WhatIsGround

    public Rigidbody rb; // RigidBody
    public Transform orientation; // player rotating

    private void Awake()
    {
        cameraControls = new CameraControls();
    }

    private void OnEnable()
    {
        cameraControls.Enable(); // Enabling

        // Camera Looking
        cameraControls.Player.Look.performed += OnLook;
        cameraControls.Player.Look.canceled += OnLook;

        // Camera Moving
        cameraControls.Player.Move.performed += OnMove;
        cameraControls.Player.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        // Camera Looking
        cameraControls.Player.Look.performed -= OnLook;
        cameraControls.Player.Look.canceled -= OnLook;

        // Camera Moving
        cameraControls.Player.Move.performed += OnMove;
        cameraControls.Player.Move.canceled += OnMove;
        cameraControls.Disable(); // Disabling
    }

    private void OnMove(InputAction.CallbackContext context)
    {   // Move
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnLook(InputAction.CallbackContext context)
    {   // Looking
        lookInput = context.ReadValue<Vector2>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        float mouseX = lookInput.x * sensX * Time.deltaTime;
        float mouseY = lookInput.y * sensY * Time.deltaTime;


        Debug.Log("Horizontal Input (X): " + lookInput.x);

        yRotation += mouseX; 
        xRotation -= mouseY; 

        
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        FirstPersonPov.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.2f, whatIsGround);

        SpeedControl();

        // Handle drag
        rb.linearDamping = grounded ? groundDrag : 0;
    }

    private void FixedUpdate()
    {
        // Tells if its grounded or not
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f, whatIsGround); 

        MovePlayer();
        GetMoveDirection();

        // Handle drag
        rb.linearDamping = grounded ? groundDrag : 0;
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * moveInput.y + orientation.right * moveInput.x;

        if (grounded == true)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            Debug.Log("Grounded");
        }
        else
        {
            // This is for momentum during air time doesn't work right (player flies)
            // rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            Debug.Log("Not Grounded");
        }
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

