using UnityEngine;
using System.Collections;


public class Enemy : MonoBehaviour
{
    public void TakeFlashEffect()
    {
        Destroy(gameObject); 
        Debug.Log("Object was hit");

    }
}