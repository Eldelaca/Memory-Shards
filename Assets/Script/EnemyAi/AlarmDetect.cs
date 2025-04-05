using UnityEngine;

public class AlarmDetect : MonoBehaviour
{
    private Wave waveScript;                
    public float normalSpeed = 1.0f;       // Default speed 
    public float normalScale = 1.0f;       // Default scale
    public float maxSpeedMultiplier = 2.0f; // Max speed 
    public float maxScaleMultiplier = 2.0f; // Max scale 
    public float maxDistance = 10.0f;      // Max detectrange
    public float smoothing = 5.0f;         // Smoothing

    public GameObject enemy;             

    private float targetSpeed;             
    private float targetScale;             

    private float currentSpeed;            
    private float currentScale;            

    void Start()
    {
        waveScript = GetComponent<Wave>();  
        
        currentSpeed = normalSpeed;
        currentScale = normalScale;
    }

    void Update()
    {
        AlarmDetection();  
    }

    void AlarmDetection()
    {
        
        float distance = Vector3.Distance(transform.position, enemy.transform.position);

        
        float distanceBetween;
        if (distance >= maxDistance)
        {
            distanceBetween = 0; // No speed increase beyond max distance
        }
        else if (distance <= 0)
        {
            distanceBetween = 1; // Max speed at distance
        }
        else
        {
            // distance from 0 to 1
            distanceBetween = 1 - (distance / maxDistance);
        }

        targetSpeed = normalSpeed + (distanceBetween * (maxSpeedMultiplier - normalSpeed));
        targetScale = normalScale + (distanceBetween * (maxScaleMultiplier - normalScale));

        // Smoothly transition speed and scale
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * smoothing);
        currentScale = Mathf.Lerp(currentScale, targetScale, Time.deltaTime * smoothing);

        // Apply the smoothly transitioned speed and scale to the wave script
        if (waveScript != null)
        {
            waveScript.AdjustSpeed(currentSpeed); 
            waveScript.scale = currentScale;     
        }

    }
}
