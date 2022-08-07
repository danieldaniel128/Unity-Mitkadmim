using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBehavior : MonoBehaviour
{
    public UnityEvent enemyKilled;

    [SerializeField] Animator animator;
    [SerializeField] string KilledBoolExpersion;
    [SerializeField] EnemyStatsSO currentEnemy = new EnemyStatsSO();


    private int currentHealth;

    public void AnimatorBoolToTrue()
    {
        animator.SetBool(KilledBoolExpersion, true);
    }

    private void Start()
    {
        currentHealth = currentEnemy.maxHealth;
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

    // Used to debug Enemy Hit And death Logic
    private void OnMouseDown()
    {
        TakeDamage(25);
    }
}

// Code By Amit
