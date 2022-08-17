using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elad
{
    public class PlayerAnimationManager : MonoBehaviour
    {
        [SerializeField] Animator animator;
        [SerializeField] MovementData data;
        float currentVelocity;
        int velocityHash;
        [SerializeField] Rigidbody rb;

        private void Start()
        {
            velocityHash = Animator.StringToHash("Velocity");
        }
        private void Update()
        {
            currentVelocity = rb.velocity.magnitude;
            animator.SetFloat(velocityHash, currentVelocity / data.runMaxSpeed);
        }
    }
}
