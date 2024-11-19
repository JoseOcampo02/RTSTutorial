using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class UnitAttackState : StateMachineBehaviour
{
    NavMeshAgent agent;
    AttackController attackController;

    public float stopAttackingDistance = 3f;
    public float attackRate = 2f;
    public float attackTimer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entered attack state!");
        agent = animator.GetComponent<NavMeshAgent>();
        attackController = animator.GetComponent<AttackController>();
        attackController.SetAttackMaterial();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.transform.GetComponent<UnitMovement>().isCommandedToMove == true || attackController.targetToAttack == null)
        {
            // HERE --------------------------------------------------------
            animator.SetBool("isAttacking", false);
        }
        else
        {
            LookAtTarget();

            // Keep moving towards enemy
            agent.SetDestination(attackController.targetToAttack.position);

            // Actually attack unit
            if (attackTimer <= 0)
            {
                Attack();
                attackTimer = 1f / attackRate;
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
            

            // Should unit still attack
            float distanceFromTarget = Vector3.Distance(attackController.targetToAttack.position, animator.transform.position);
            // Attacking is stopped if enemy is too far away or enemy dies or target changes
            if (distanceFromTarget > stopAttackingDistance || attackController.targetToAttack == null)
            {
                //Debug.Log("Exiting attack state: distanceFromtarget = " + distanceFromTarget + ", stopAttackingDistance = " + stopAttackingDistance);
                animator.SetBool("isAttacking", false); // Move to follow state
            }
        }
    }

    private void Attack()
    {
        var damageToInflict = attackController.unitDamage;
        bool died = attackController.targetToAttack.GetComponent<Unit>().TakeDamage(damageToInflict);
    }

    private void LookAtTarget()
    {
        Vector3 direction = attackController.targetToAttack.position - agent.transform.position;
        agent.transform.rotation = Quaternion.LookRotation(direction);

        var yRotation = agent.transform.eulerAngles.y;
        agent.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
