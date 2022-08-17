using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBehavior : MonoBehaviour
{
    public UnityEvent enemyKilled;
    public UnityEvent enemyAttack;

    [SerializeField] Animator enemyAnimator;
    [SerializeField] string KilledBoolExpersion;
    [SerializeField] string AttackBoolExpersion;
    [SerializeField] EnemyStatsSO currentEnemySO;


    private int currentHealth;
    private int currentDamage;

    public void AnimatorKilledTrue()
    {
        enemyAnimator.SetBool(KilledBoolExpersion, true);
    }

    public void AnimatorAttackTrue()
    {
        enemyAnimator.SetBool(AttackBoolExpersion, true);
    }

    public void AnimatorAttackFalse()
    {
        enemyAnimator.SetBool(AttackBoolExpersion, false);
    }

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


    // Used to debug Animations, Attack will Initiate, and killed will initate evantualy
    private void OnMouseDown()
    {
        TakeDamage(25);
        enemyAttack.Invoke();
    }
}

// Code By Amit
