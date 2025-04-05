using UnityEngine;
using System.Collections;


public class Enemy : MonoBehaviour
{

    void Update()
    {
        
    }

    /*
    public void TakeFlashEffect()
    {
        // Kills player
        Destroy(gameObject); 
        Debug.Log("Object was hit");

    use later to stun or stop enemy from killing player

    }
    */

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }
}