using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCam : MonoBehaviour
{
    public float sensX = 15f;
    public float sensY = 15f;

    public Transform orientation; 
    public Camera playerCamera;  

    private float xRotation = 0f; 
    private float yRotation = 0f; 

    private Vector2 lookInput;

    private PlayerControls inputActions;

    private void Awake()
    {
        inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Gameplay.Look.performed += OnLook;
        inputActions.Gameplay.Look.canceled += OnLook;
    }

    private void OnDisable()
    {
        inputActions.Gameplay.Look.performed -= OnLook;
        inputActions.Gameplay.Look.canceled -= OnLook;
        inputActions.Disable();
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerCamera.fieldOfView = 60f;
    }

    private void Update()
    {
        float mouseX = lookInput.x * sensX * Time.deltaTime;
        float mouseY = lookInput.y * sensY * Time.deltaTime;


        Debug.Log("Horizontal Input (X): " + lookInput.x);

        yRotation += mouseX; 
        xRotation -= mouseY; 

        
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        
        playerCamera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        
        
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
