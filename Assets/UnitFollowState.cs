using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class UnitFollowState : StateMachineBehaviour
{
    AttackController attackController;
    NavMeshAgent agent;
    public float attackingDistance = 1f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackController = animator.transform.GetComponent<AttackController>();
        agent = animator.transform.GetComponent<NavMeshAgent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Should unit transition to idle state?
        if (attackController.targetToAttack == null)
        {
            animator.SetBool("isFollowing", false);
        }
        else // unit has a target
        {
            // Check if direct movement command overrides automatic movement/behavior
            if (agent.transform.GetComponent<UnitMovement>().isCommandedToMove == false)
            {
                // Moving unit toward enemy
                agent.SetDestination(attackController.targetToAttack.position);
                animator.transform.LookAt(attackController.targetToAttack);

                //// Should unit transition to attack state?
                //float distanceFromTarget = Vector3.Distance(attackController.targetToAttack.position, animator.transform.position);
                //if (distanceFromTarget < attackingDistance)
                //{
                //    agent.SetDestination(attackController.targetToAttack.position);
                //    animator.SetBool("isAttacking", true); // Move to attacking state
                //}
            }
        }
    }
}
