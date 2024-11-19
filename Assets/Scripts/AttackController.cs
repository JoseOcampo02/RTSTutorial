using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public Transform targetToAttack;

    public Material idleStateMaterial;
    public Material followStateMaterial;
    public Material attackStateMaterial;
    public bool isPlayer;
    public int unitDamage;

    // For when enemies enter friendly unit detection range.
    // Automatically responds without explicit user commands.
    private void OnTriggerEnter(Collider other)
    {
        if (isPlayer && other.CompareTag("Enemy") && targetToAttack == null)
        {
            targetToAttack = other.transform;
            transform.GetComponent<UnitMovement>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPlayer && other.CompareTag("Enemy") && targetToAttack != null)
        {
            //Debug.Log("targetToAttack set to null");
            targetToAttack = null;

            if (transform.GetComponent<UnitMovement>().isCommandedToMove == false && !(transform.GetComponent<UnitSelectionManager>().unitsSelected.Contains(gameObject)))
            {
                transform.GetComponent<UnitMovement>().enabled = false;
            }
        }
    }
    // Critical checks added above to ensure UnitMovement script is enabled/disabled as necessary


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIdleMaterial()
    {
        GetComponent<Renderer>().material = idleStateMaterial;
    }

    public void SetFollowMaterial()
    {
        GetComponent<Renderer>().material = followStateMaterial;
    }

    public void SetAttackMaterial()
    {
        GetComponent<Renderer>().material = attackStateMaterial;
    }

    private void OnDrawGizmos()
    {
        // Follow Distance
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 10f * 0.5f);

        // Attack Distance
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2f);

        // Stop Attack Distance
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 3f);
    }
}
