using Controllers.Creatures;
using Data.Creatures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CombatSystem : MonoBehaviour
{

    public void TakeDamage(int takenDamage, string creatureType)
    {

        if (creatureType.Equals("Player"))
        {
            if (takenDamage < 0)
                takenDamage = 0;
            int playerHealth = GameManager.instance.playerData.GetCurrentHealth();
            playerHealth -= takenDamage;
            if (playerHealth < 0)
            {
                GameManager.instance.GameOver();
            }
            GameManager.instance.playerData.SetCurrentHealth(playerHealth);
            Debug.Log("<color=red>Player's health: </color><color=green>" + GameManager.instance.playerData.GetCurrentHealth() + "</color>");
        }

    }
    public void TakeDamage(int takenDamage, GameObject Enemy, CreatureController _enemyData)
    {
        int enemyCurrentHealth = _enemyData.GetCurrentHealth();
        enemyCurrentHealth -= takenDamage;
        if (enemyCurrentHealth <= 0)
        {
            enemyCurrentHealth = 0;
            Enemy.GetComponent<MeshCollider>().enabled = false;
            _enemyData.GetEnemyEvent().enemyKilled.Invoke();
            _enemyData._lineOfSight.targetsAquired.Clear();
            _enemyData._lineOfSight.targetInAttRange.Clear();

            if (Enemy.tag.Equals("Boss"))
            {
                GameManager.instance.WinGame();
            }
            GameManager.instance.enemiesAlive--;
            Debug.Log("Enemy Killed");
        }
        _enemyData.SetCurrentHealth(enemyCurrentHealth);
        Debug.Log("<color=red>Enemy's health: </color><color=green>" + _enemyData.GetCurrentHealth() + "</color>");
    }



}
