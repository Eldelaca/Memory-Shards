using UnityEngine;
using UnityEngine.UI; 

public class StaminaBar : MonoBehaviour
{
    public static StaminaBar instance; 
    public Slider staminaSlider; 
    public float maxStamina = 100f;
    public float currentStamina;

    private void Awake()
    {
        // If there is already an instance, destroy this object to ensure only one instance exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps the StaminaBar across scenes
        }

        currentStamina = maxStamina; // Set starting stamina
        UpdateStaminaBar(); 
    }

    // Call this method to set stamina
    public void SetStamina(float value)
    {
        currentStamina = Mathf.Clamp(value, 0f, maxStamina); 
        UpdateStaminaBar(); 
    }

    // Update the stamina bar UI
    private void UpdateStaminaBar()
    {
        if (staminaSlider != null)
        {
            staminaSlider.value = currentStamina / maxStamina;
        }
    }

    // Uses the Stamina
    public bool UseStamina(float amount)
    {
        if (currentStamina >= amount)
        {
            currentStamina -= amount;
            UpdateStaminaBar();
            return true; // Used stamina
        }
        return false; // Not enough stamina
    }

    // Regenerate Stamina overtime
    public void RegenerateStamina(float amount)
    {
        currentStamina += amount;
        currentStamina = Mathf.Min(currentStamina, maxStamina); 
        UpdateStaminaBar(); 
    }

    public float GetCurrentStamina()
    {
        return currentStamina;
    }

}
