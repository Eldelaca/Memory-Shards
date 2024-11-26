using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
            StaminaBar.instance.UseStamina(0.1f);

        if(Input.GetKeyDown(KeyCode.Space))
            StaminaBar.instance.UseStamina(15);
    }
}
