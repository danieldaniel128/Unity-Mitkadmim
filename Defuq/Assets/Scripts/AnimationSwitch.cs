using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSwitch : MonoBehaviour
{
    [SerializeField] Animator enemyAnimator;
    [SerializeField] string KilledBoolExpersion;
    [SerializeField] string AttackBoolExpersion;

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
}

// Made by Amit
