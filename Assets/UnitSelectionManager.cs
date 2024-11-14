using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitSelectionManager : MonoBehaviour
{
    
    // We only want one instance of UnitSelectionManager. To accomplish this we
    // employ the Singleton pattern

    public static UnitSelectionManager Instance { get; set; }

    public List<GameObject> allUnitsList = new List<GameObject>();
    public List<GameObject> unitsSelected = new List<GameObject>();

    public LayerMask clickable;
    public LayerMask ground;
    public GameObject groundMarker;
    private Camera cam;
    

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        // if left click
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            // Check if we are hitting a clickable object
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    MultiSelect(hit.collider.gameObject);
                }
                else
                {
                    SelectByClicking(hit.collider.gameObject);
                }
            }
            else // If we are NOT hitting clickable object deselect all
            {
                if (Input.GetKey(KeyCode.LeftShift) == false)
                DeselectAll();
            }

        }

        if (Input.GetMouseButtonDown(1) && unitsSelected.Count > 0)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            // Check if we are hitting a clickable object
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                groundMarker.transform.position = hit.point;

                groundMarker.SetActive(false);
                groundMarker.SetActive(true);

            }

        }

    }

    private void MultiSelect(GameObject unit)
    {
         if (unitsSelected.Contains(unit) == false)
        {
            unitsSelected.Add(unit);
            EnableUnitMovement(unit, true);
            TriggerSelectionIndicator(unit, true);
        }
         else
        {
            unitsSelected.Remove(unit);
            EnableUnitMovement(unit, false);
            TriggerSelectionIndicator(unit, false);
        }
    }

    private void SelectByClicking(GameObject unit)
    {
        DeselectAll();

        unitsSelected.Add(unit);
        TriggerSelectionIndicator(unit, true);

        EnableUnitMovement(unit, true);
    }

    private void EnableUnitMovement(GameObject unit, bool shouldMove)
    {
        unit.GetComponent<UnitMovement>().enabled = shouldMove;
    }

    private void DeselectAll()
    {
        foreach (var unit in unitsSelected)
        {
            EnableUnitMovement(unit, false);
            TriggerSelectionIndicator(unit, false);
        }

        groundMarker.SetActive(false);

        unitsSelected.Clear();
    }

    private void TriggerSelectionIndicator(GameObject unit, bool isVisible)
    {
        unit.transform.GetChild(0).gameObject.SetActive(isVisible);
    }

}
