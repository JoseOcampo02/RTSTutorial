using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    Camera cam;
    NavMeshAgent agent;
    public LayerMask ground;

    public bool isCommandedToMove;
    public bool isCommandedtoMoveThisLoop;

    private void Start()
    {
        isCommandedToMove = false;
        isCommandedtoMoveThisLoop = false;
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        isCommandedtoMoveThisLoop = false;

        Debug.Log("isCommandedToMove? " + isCommandedToMove);

        // if right click
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                isCommandedToMove = true;
                isCommandedtoMoveThisLoop = true;
                agent.SetDestination(hit.point);
            }

        }

        // Agent reached destination
        if ((agent.hasPath == false || agent.remainingDistance <= agent.stoppingDistance) && !isCommandedtoMoveThisLoop)
        {
            isCommandedToMove = false;
        }

    }

}
