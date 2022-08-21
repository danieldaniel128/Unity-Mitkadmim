using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Movement
{
    public class PlayerMovementAnimations : MonoBehaviour
    {
        [SerializeField] Animator animator;
        [SerializeField] MovementData data;
        [SerializeField] Rigidbody rb;
        [SerializeField] Transform playerV3;
        float currentVelocity;
        int velocityXHash, velocityZHash;
        float x, z;

        Vector2 playerV2;

        private void Start()
        {
            velocityXHash = Animator.StringToHash("VelocityX");
            velocityZHash = Animator.StringToHash("VelocityZ");
        }
        private void Update()
        {

            Vector2 VelocityV2 = new Vector2(rb.velocity.x, rb.velocity.z);
            playerV2 = new Vector2(playerV3.forward.x, playerV3.forward.z);
            currentVelocity = rb.velocity.magnitude;

            //direction of looking forward
            z = Mathf.Cos(Vector2.SignedAngle(playerV2, VelocityV2) * Mathf.Deg2Rad) * currentVelocity;

            //direction of looking right
            x = Mathf.Cos((Vector2.SignedAngle(playerV2, VelocityV2) + 90) * Mathf.Deg2Rad) * currentVelocity;

            Vector2 animationMovement = new Vector2(x, z).normalized;
            animator.SetFloat(velocityXHash, animationMovement.x);
            animator.SetFloat(velocityZHash, animationMovement.y);
            Debug.Log("z = " + z + " x = " + x);
        }
    }
}
