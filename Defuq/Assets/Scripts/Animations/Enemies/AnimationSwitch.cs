using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animations.Enemies
{
    public class AnimationSwitch : MonoBehaviour
    {
        [SerializeField] Animator enemyAnimator;
        [SerializeField] string KilledBoolExpersion;
        [SerializeField] string AttackBoolExpersion;
        [SerializeField] string SpeedFloatExpersion;

        public void AnimatorKilledTrue()
        {
            enemyAnimator.SetBool(KilledBoolExpersion, true);
        }


        public void AnimatorKilledFalse()
        {
            enemyAnimator.SetBool(KilledBoolExpersion, false);
        }

        public void AnimatorAttackTrue()
        {
            enemyAnimator.SetBool(AttackBoolExpersion, true);
        }

        public void AnimatorAttackFalse()
        {
            enemyAnimator.SetBool(AttackBoolExpersion, false);
        }

        public bool GetAttackState()
        {
            return enemyAnimator.GetBool(AttackBoolExpersion);
        }
        
        public void SetSpeed(float speed)
        {
            enemyAnimator.SetFloat(SpeedFloatExpersion, speed);
        }
    }
}


// Made by Amit
