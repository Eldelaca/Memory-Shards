using UnityEngine;

public class turn : StateMachineBehaviour
{

    private AudioSource[] audio;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        // Get access to GetComponentsInParent, this will return an array
        audio = animator.GetComponentsInParent<AudioSource>();
        // Play audio OneStep
        audio[2].Play();
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        // Stop audio stepping around
        audio[2].Stop();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

}
