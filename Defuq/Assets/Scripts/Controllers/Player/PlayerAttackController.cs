using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers.States;

public class PlayerAttackController : MonoBehaviour
{
    //added animation for attack
    [SerializeField] Animator playerAnimator;
    //Create a counter for combos
    int combos = 0;
    //create timer for time passed between clicks
    float timer = 0f;
    //create var for time it takes for combo to reset
    float timeBetweenComboHits = 1.5f;

    //time it takes to do a charge attack
    float chargeAttackTime = 2f;

    //create a method to be called by the event
    public void Attack(PlayerState state)
    {

        //first, is the player holding the attack btn?
        if (state.tag != "holdAttack")
        {
            //is the player released the attack?
            if (state.tag != "releaseAttack") return;

            //if too much time has passed, then reset combo counter
            Debug.Log(timer);
            if (timer > timeBetweenComboHits)
            {
                Debug.Log("Reset Combo to 0");
                combos = 0;
            }

            //coroutine to start a timer
            StartCoroutine(ComboTimer());

            //Player release the attack
            var _releaseState = state as AttackReleaseState;
            var holdTime = _releaseState.holdTime % 60;
            holdTime = Mathf.FloorToInt(holdTime) + 1;
            combos++;

            //depending on combo, play matching animation
            if (combos == 3)
            {
                Debug.Log("combo: " + combos);
                combos = 0;
                //get the hold time in seconds
                Debug.Log($"third attack");
                playerAnimator.SetTrigger("Attack3");
                return;
            }
            else if (combos == 2)
            {
                Debug.Log("combo: " + combos);
                Debug.Log("second attack");
                playerAnimator.SetTrigger("Attack2");
                return;
            }

            //check if charge attack
            if (holdTime >= chargeAttackTime)
            {
                Debug.Log("CRIT for: " + holdTime * 2);
                playerAnimator.SetTrigger("ChargeAttack");
            }
            else
            {
                Debug.Log("combo: " + combos);
                Debug.Log("first attack");
                playerAnimator.SetTrigger("Attack1");
            }
            return;
        }
        //Player is holding the attack
        Debug.Log("Holding");
    }


    private IEnumerator ComboTimer()
    {
        timer = 0;
        while (timer < timeBetweenComboHits)
        {
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
