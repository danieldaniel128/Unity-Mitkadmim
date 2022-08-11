using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/Dash")]
public class DashAbility : Ability
{
    float cooldown;
    [SerializeField] float activeDuration;

    private void Awake()
    {
        cooldown = cooldownTime;
    }
    public override void Cast()
    {
        //activate dash ability based on rigidbody velocity
    }
}
