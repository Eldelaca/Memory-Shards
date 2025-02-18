using UnityEngine;

public class DoorClosing : MonoBehaviour
{

    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("doorClose", true);
        }
    }

    /* Update is called once per frame
    void Update()
    {
        
    }
    */
}
