using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCam : MonoBehaviour
{
    public float sensX = 15f;
    public float sensY = 15f;

    public Transform orientation;
    public Transform Hand;
    public Transform playerCamera;
    public float cameraAcceleration = 5f;

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
        
    }

    private void Update()
    {
        float mouseX = lookInput.x * sensX * Time.deltaTime;
        float mouseY = lookInput.y * sensY * Time.deltaTime;

        

        yRotation += mouseX; 
        xRotation += mouseY; 

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        Hand.localRotation = Quaternion.Euler(-xRotation, yRotation, 0);

        transform.localRotation = 
            Quaternion.Lerp(transform.localRotation,
            Quaternion.Euler(-xRotation, yRotation, 0),
            cameraAcceleration * Time.deltaTime);

        orientation.localRotation = 
            Quaternion.Lerp(orientation.localRotation,
            Quaternion.Euler(0f, yRotation, 0f),
            cameraAcceleration * Time.deltaTime);
    }

}
