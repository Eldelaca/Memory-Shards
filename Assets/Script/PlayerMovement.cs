using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

/// <summary>
/// 
/// Player code movement 
/// 
/// </summary>

public class PlayerMovement : MonoBehaviour
{
    // Speed Variables
    public float moveSpeed;
    public float groundDrag;
    public float airMultiplier;

    // Bool checks for isGrounded checks
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    public Transform orientation; // Grabs player orientation

    public Camera playerCamera;

    // Enemy Detection Variables
    public LayerMask enemyLayer; 
    public float flashRange = 3f; 
    public float flashRadius = 5f; 

    // Flash Var
    public GameObject flash;
    
    private Light flashLight; // Light Component
    public float flashIntensity = 50f; // Light intensity
    public float fadeDuration = 3f; // Duration of flash fade
    [SerializeField] private bool isFlashActive = false;
    private Coroutine flashCoroutine;

    // Cooldown Variables
    public float cooldownTime = 5f;
    private bool canFlash = true;

    // Switching
    public GameObject CameraItem;
    public GameObject AlarmItem;



    // Movement Varaibles
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
        inputActions.Gameplay.Next.performed += OnNextPressed;
        inputActions.Gameplay.Prev.performed += OnPrevPressed;
        inputActions.Gameplay.Flash.performed += OnFlash;

    }

    private void OnDisable()
    {
        inputActions.Gameplay.Move.performed -= OnMove;
        inputActions.Gameplay.Move.canceled -= OnMove;
        inputActions.Gameplay.Next.performed -= OnNextPressed;
        inputActions.Gameplay.Prev.performed -= OnPrevPressed;

        inputActions.Gameplay.Flash.performed -= OnFlash;
        inputActions.Disable();
    }

    // Switch Input
    private void OnNextPressed(InputAction.CallbackContext context)
    {
       
        CameraItem.SetActive(false);
        AlarmItem.SetActive(true);
        Debug.Log("Next item button was pressed");
        
    }

    private void OnPrevPressed(InputAction.CallbackContext context)
    {
        
        AlarmItem.SetActive(false);
        CameraItem.SetActive(true);
        Debug.Log("Prev item button was pressed");
    }


    // Move Input
    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // Camera Flash Input
    private void OnFlash(InputAction.CallbackContext context)
    {
        // Checks flash trigger
        if (canFlash && !isFlashActive)
        {
            if (StaminaBar.instance.UseStamina(15))
            {
                flashCoroutine = StartCoroutine(FlashRoutine());
            }

        }
    }

    private IEnumerator FlashRoutine()
    {
        // How it activates the Flash
        isFlashActive = true;
        flash.SetActive(true);
        flashLight.intensity = flashIntensity; 

        // Flash duration
        yield return new WaitForSeconds(0.2f);

        // Detect Enemies
        DetectEnemies();

        float elapsedTime = 0f;
        float startIntensity = flashLight.intensity;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            flashLight.intensity = Mathf.Lerp(startIntensity, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        
        flashLight.intensity = 0f;

        // Deactivates after fadeaway
        flash.SetActive(false);
        isFlashActive = false;

        // The flash cooldown
        StartCoroutine(FlashCooldown());
    }

    private IEnumerator FlashCooldown()
    {
        canFlash = false;
        yield return new WaitForSeconds(cooldownTime); // Waiting....

        canFlash = true;
    }

    private void DetectEnemies()
    {
        // Puts sphere infront of player
        Vector3 sphereCenter = playerCamera.transform.position + playerCamera.transform.forward * flashRange;

        // creates the sphere
        Collider[] hitColliders = Physics.OverlapSphere(sphereCenter, flashRadius, enemyLayer);

        foreach (Collider hitCollider in hitColliders)
        {
            Enemy enemy = hitCollider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeFlashEffect();
            }
        }
    }



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        if (flash != null)
        {
            flash.SetActive(false);
            flashLight = flash.GetComponent<Light>();
            if (flashLight == null)
            {
                Debug.LogError("No Light component found on the flashlight GameObject!");
            }
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


    // Movement 
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


    // Drawing invisible colliders
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 sphereCenter = Camera.main.transform.position + Camera.main.transform.forward * flashRange;
        Gizmos.DrawWireSphere(sphereCenter, flashRadius);
    }

}
