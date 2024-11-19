using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private float unitHealth;
    public float unitMaxHealth;
    public HealthTracker healthTracker;


    // Start is called before the first frame update
    void Start()
    {
        UnitSelectionManager.Instance.allUnitsList.Add(gameObject);
        unitHealth = unitMaxHealth;
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        healthTracker.UpdateSliderValue(unitHealth, unitMaxHealth);

        if (unitHealth <= 0)
        {
            // Dying logic

            // Destruction or Dying Animation

            //Dying Sound Effect
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        UnitSelectionManager.Instance.allUnitsList.Remove(gameObject);
    }

    internal bool TakeDamage(int damageToInflict)
    {
        unitHealth -= damageToInflict;
        UpdateHealthUI();
        if (unitHealth <= 0)
            return true;
        return false;
    }
}
