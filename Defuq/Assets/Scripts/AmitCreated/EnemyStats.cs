using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Scriptable;
using System.Events;

namespace Data.Enemy
{
    public class EnemyStats : MonoBehaviour
    {
        [SerializeField] EnemyStatsSO currentEnemySO;
        [SerializeField] CreatureEvents enemyEventScript;


        private int currentHealth;
        private int currentDamage;
        public float currentRadius;
        public float currentViewAngle;
        public float currentAttackRange;

        private void Awake()
        {
            currentHealth = currentEnemySO.maxHealth;
            currentDamage = currentEnemySO.attackDamage;
            currentRadius = currentEnemySO.visionRange;
            currentViewAngle = currentEnemySO.visionAngle;
            currentAttackRange = currentEnemySO.attackRange;
        }

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
                enemyEventScript.enemyKilled.Invoke();
                Debug.Log("Enemy Killed");
            }
        }
    }
}

