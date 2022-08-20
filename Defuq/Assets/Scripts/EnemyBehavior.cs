using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBehavior : MonoBehaviour
{
    public UnityEvent enemyKilled;
    public UnityEvent enemyAttack;

    [SerializeField] EnemyStatsSO currentEnemySO;

    private int currentHealth;
    private int currentDamage;



    private void Awake()
    {
        currentHealth = currentEnemySO.maxHealth;
        currentDamage = currentEnemySO.attackDamage;
    }

    // Can be change if we decide the player manage all combat logic
    private void TakeDamage(int damageTaken)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageTaken;
            // Debug
            Debug.Log("Damage Taken");
        }
        else if (currentHealth <= 0)
        {
            currentHealth = 0;
            enemyKilled.Invoke();
            Debug.Log("Enemy Killed");
        }
    }
}

// Code By Amit
