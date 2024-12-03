using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Create an instance of the UnitHealth class
    private UnitHealth unitHealth;

    // You can expose initial health and max health in the Inspector
    public int initialHealth = 100;
    public int maxHealth = 100;

    void Start()
    {
        // Initialize the UnitHealth with the values from the Inspector
        unitHealth = new UnitHealth(initialHealth, maxHealth);
    }

    void Update()
    {
        // Example: you could check for damage or healing over time
        // For now, let's just simulate taking damage when the space key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10); // Take 10 damage when space is pressed
        }

        // For debugging purposes, you can log current health
        Debug.Log("Current Health: " + unitHealth.Health);
    }

    // Method to apply damage to the enemy
    public void TakeDamage(int damage)
    {
        unitHealth.DmgUnit(damage);
        if (unitHealth.Health <= 0)
        {
            Die();
        }
    }

    // Method to heal the enemy
    public void Heal(int healAmount)
    {
        unitHealth.HealUnit(healAmount);
    }

    // Handle death (could trigger animations, destruction, etc.)
    void Die()
    {
        // For simplicity, just destroy the object
        Destroy(gameObject);
    }
}