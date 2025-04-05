using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaSlider;
    public GameData gameData; // Ref to the GameData

    void Start()
    {
        staminaSlider.maxValue = gameData.maxStamina;
        staminaSlider.value = gameData.stamina;
    }

    void Update()
    {
        staminaSlider.value = gameData.stamina; // Updates stamina value from GameData
    }

    // Method to decrease stamina
    public void UseStamina(float amount)
    {
        if (gameData.stamina - amount >= 0)
        {
            gameData.stamina -= amount;
        }
    }

    // Method to regenerate stamina
    public void RegenerateStamina(float amount)
    {
        if (gameData.stamina + amount <= gameData.maxStamina)
        {
            gameData.stamina += amount;
        }
    }
}
