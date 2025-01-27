using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider staminaBar;

    // Flash Stamina
    public PlayerMovement playerMovement;

    private float maxStamina = 100f;
    private float currentStamina;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regen;

    public static StaminaBar instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;   
    }

    public float GetCurrentStamina()
    {
        return currentStamina;
    }

    public bool UseStamina(float amount)
    {
        if(currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            staminaBar.value = currentStamina;
            return true;

            /*
            if(regen != null)
                StopCoroutine(regen);


            regen = StartCoroutine(RegenStamina());
            */
        }
        else
        {
            return false;
            Debug.Log("Not enough stamina");
        }

    }

    /*
    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(2);

        while(currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 100f;
            staminaBar.value = currentStamina;
            yield return regenTick;
        }
        regen = null;
    }
    */

}
