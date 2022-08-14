using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Enemy",menuName = "Create New Enemy SO",order = 1)]
public class EnemyStatsSO : ScriptableObject
{
    public int maxHealth;
    public int attackDamage;
    public int attackRange;
    public int viosinRange;
    // ? not sure about the role but ok
    public int visionAngle;

}
// script Created by Amit 