using UnityEngine;
using System.Collections;

public class AnimationScript : MonoBehaviour {

	// Setup a variable to point to the Animator Controller for the character
	Animator animator;
	// Setup 2 float for vertical/horizontal input
	float verticalInput;
	float horizontalInput;

	void Start () {
		//get the Animator Controller Component from the character component hierarchy
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		// Get the input from vertical/horizontal axis
		verticalInput = Input.GetAxis("Vertical");
		horizontalInput = Input.GetAxis("Horizontal");
	}
	void FixedUpdate(){
		// Now set the animator float values (vAxisInput/hAxisInput)
		animator.SetFloat ("vAxisInput", verticalInput);
		animator.SetFloat ("hAxisInput", horizontalInput);

		// Detect Z Key press
		if (Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.W)) {
			// Set runBool to true if pressed
			animator.SetBool ("runBool", true);
	
		} else {
			// Set runBool to false if not pressed
			animator.SetBool ("runBool", false);
		}

		if (Input.GetKey(KeyCode.C))
		{
			// Set the Crouch Layer Weight to 0.5, this
			// activtes the masked couch animation
			animator.SetLayerWeight(1, 0.5f);
			animator.SetBool("crouchBool", true);
			Debug.Log("Crouching");
		}
		else
		{
			// Set the Couch Layer Weight back to 0.0
			// This deactivated the crouch animation
			animator.SetLayerWeight(1, 0.0f);
            animator.SetBool("crouchBool", false);
        }
    }
}