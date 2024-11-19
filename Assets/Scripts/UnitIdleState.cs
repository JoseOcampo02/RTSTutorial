using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitIdleState : StateMachineBehaviour
{
    AttackController attackController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackController = animator.transform.GetComponent<AttackController>();
        attackController.SetIdleMaterial();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Check for movement command, if command is true, skip all and stay in idle
        if (animator.transform.GetComponent<UnitMovement>().isCommandedToMove == false)
        {

            // Check if there is a target
            if (attackController.targetToAttack != null)
            {
                // Transition to Follow State
                animator.SetBool("isFollowing", true);
            }
        }
    }
}
