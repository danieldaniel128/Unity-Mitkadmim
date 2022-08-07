using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBehavior : MonoBehaviour
{
    public UnityEvent enemyKilled;

    [SerializeField] EnemyStatsSO currentEnemy = new EnemyStatsSO();

    private int currentHealth;

    private void Start()
    {
        currentHealth = currentEnemy.maxHealth;
    }

    // Can be change if we decide the player manage all combat logic
    void TakeDamage(int damageTaken)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageTaken;
            // Debug
            Debug.Log("Damage Taken");
        }
        else if (currentHealth < 0)
        {
            enemyKilled.Invoke();
            currentHealth = 0;
            // Debug
            Debug.Log("Enemy Killed");
        }
    }
}

// Code By Amit
