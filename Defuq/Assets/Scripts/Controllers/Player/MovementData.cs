using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers_Player
{
    [CreateAssetMenu(fileName = "New Movement", menuName = "Movement")]
    public class MovementData : ScriptableObject
    {
        [Header("Movement Variables")]
	    public float runMaxSpeed; //Target speed we want the player to reach.
        public float runAcceleration; //Time (approx.) time we want it to take for the player to accelerate from 0 to the runMaxSpeed.
	    public float runDeceleration; //Time (approx.) we want it to take for the player to accelerate from runMaxSpeed to 0.

        [Header("Dash Variables")]
        public KeyCode dashKey;
        public float dashSpeed;
        public float dashCooldown;
        public float dashDuration;
        public bool resetVelocityAfterDash;
        private void OnValidate()
        {
            runAcceleration = Mathf.Clamp(runAcceleration,1,runMaxSpeed);
            runDeceleration = Mathf.Clamp(runDeceleration,1,runMaxSpeed);
            dashCooldown = dashCooldown >= dashDuration ? dashCooldown : dashDuration + 0.1f;
        }
    }
}
