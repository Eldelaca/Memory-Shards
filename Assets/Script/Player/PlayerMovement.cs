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
    #region Variables

    [Header("Speed Var")]
    public float moveSpeed;
    public float groundDrag;
    public float airMultiplier;

    public GameObject player;
    // Bool checks for isGrounded checks
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    public Transform orientation; // Grabs player orientation

    public Camera playerCamera;

    [Header("Movement Var")]

    public LayerMask enemyLayer; 
    public float flashRange = 3f; 
    public float flashRadius = 5f;

    [Header("Flash Camera Settings")]
    public GameObject flash;
    private Light flashLight; // Light Component

    public float flashIntensity = 50f; // Intensity Value
    public float fadeDuration = 3f; // Duration of flash fade

    private Coroutine flashCoroutine;
    [SerializeField] private bool isFlashActive = false;
    

    public float cooldownTime = 5f;
    private bool canFlash = true;

    [Header("Switching Weapons")]

    public GameObject CameraItem;
    public GameObject AlarmItem;



    [Header("Movement Var")]
    private Vector2 moveInput;
    private Vector3 moveDirection;
    public Rigidbody rb;

    public PlayerControls inputActions;
    public StaminaBar staminaBar;


    [Header("Dissolve Shader Settings")]
    public float dissolveDuration = 1f;            
    public string dissolveProperty = "_DissolveAmount";
    private Coroutine dissolveCoroutine;

    #endregion

    #region New Input System
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

    #endregion

    #region Input Action
    // Switch Input
    private void OnNextPressed(InputAction.CallbackContext context)
    {
        // immediately hide the old one
        CameraItem.SetActive(false);

        // start dissolving in the AlarmItem
        if (dissolveCoroutine != null) StopCoroutine(dissolveCoroutine);
        dissolveCoroutine = StartCoroutine(DissolveIn(AlarmItem));
        Debug.Log("Next item button was pressed – dissolving in AlarmItem");
    }

    private void OnPrevPressed(InputAction.CallbackContext context)
    {
        AlarmItem.SetActive(false);

        if (dissolveCoroutine != null) StopCoroutine(dissolveCoroutine);
        dissolveCoroutine = StartCoroutine(DissolveIn(CameraItem));
        Debug.Log("Prev item button was pressed – dissolving in CameraItem");
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
            if (StaminaBar.instance != null && StaminaBar.instance.UseStamina(15f))
            {
                flashCoroutine = StartCoroutine(FlashRoutine());
            }
            
        

    }

    #endregion

    #region Flashlight Action
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

    // This detects if the flash was pressed or not
    public bool IsFlashPressed()
    {
        return inputActions.Gameplay.Flash.triggered; // Checks if the flash input has been pressed
    }

    #endregion

    #region Alarm Detection
    private void DetectEnemies()
    {
        // Puts sphere in front of player
        Vector3 sphereCenter = playerCamera.transform.position + playerCamera.transform.forward * flashRange;

        // Creates the sphere
        Collider[] hitColliders = Physics.OverlapSphere(sphereCenter, flashRadius, enemyLayer);

        // Detect and apply flash effect to enemies within range
        foreach (Collider hitCollider in hitColliders)
        {
            EnemyAI enemyAI = hitCollider.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.TakeFlashEffect();
            }
        }
    }

    #endregion

    #region Dissolve Shader Settings

    private void SetDissolve(GameObject obj, float amount)
    {
        foreach (var i in obj.GetComponentsInChildren<Renderer>())
        {
            
            i.material.SetFloat(dissolveProperty, amount);
        }
    }

    private IEnumerator DissolveIn(GameObject obj)
    {
        
        obj.SetActive(true);
        SetDissolve(obj, 1f);

        float elapsed = 0f;
        while (elapsed < dissolveDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / dissolveDuration;
            
            SetDissolve(obj, 1f - t);
            yield return null;
        }
        
        SetDissolve(obj, 0f);
    }

    #endregion

    #region Player Movement/ Actions
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

        SetDissolve(AlarmItem, 1f);
        AlarmItem.SetActive(true);

        SetDissolve(CameraItem, 1f);
        CameraItem.SetActive(false);

        if (dissolveCoroutine != null) StopCoroutine(dissolveCoroutine);
        dissolveCoroutine = StartCoroutine(DissolveIn(AlarmItem));
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


    public void SavePlayerPosition()
    {
        // Save the current position of the player
        SaveLoadManager.instance.SaveGame();
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
    #endregion
}
