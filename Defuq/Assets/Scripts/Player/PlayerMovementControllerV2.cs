using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementControllerV2 : MonoBehaviour
{
    const string HORIZONTAL_AXIS = "Horizontal";
    const string VERTICAL_AXIS = "Vertical";

    public MovementData data; //data containing variables like max speed, acceleration, dash variables...
    
    [SerializeField] Rigidbody rb;
    Vector3 _moveInput;
    bool dashAvailable = true;
    bool lockMovement = false;

    

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw(HORIZONTAL_AXIS);
        float verticalInput = Input.GetAxisRaw(VERTICAL_AXIS);

        _moveInput = new Vector3(horizontalInput, 0f, verticalInput).normalized; //recieve input from player

        if(Input.GetKeyDown(data.dashKey) && dashAvailable)
        {
            Dash();
        }

    }

    private void Dash()
    {
        StartCoroutine(DashCooldown());
        StartCoroutine(DashMovement());
    }

    private void FixedUpdate()
    {
        if (!lockMovement)
        {
            Run();
        }
    }

    private void Run()
    {

        Vector3 targetSpeed = _moveInput * data.runMaxSpeed;
        Vector3 speedDif = targetSpeed - rb.velocity;
        Vector3 movement = speedDif * (targetSpeed == Vector3.zero ? data.runDeceleration : data.runAcceleration);
        if(_moveInput == Vector3.zero && rb.velocity.magnitude <= 0.02f)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            rb.AddForce(movement, ForceMode.Acceleration);
        }
    }
    private IEnumerator DashMovement()
    {
        //lock player movement
        lockMovement = true;
        
        //reset velocity for more controlled dashspeed
        rb.velocity = Vector3.zero;
        
        //add velocity to player (dashSpeed)
        rb.velocity = _moveInput != Vector3.zero ? _moveInput * data.dashSpeed : Vector3.forward * data.dashSpeed;
        
        //for short duration (dashDuration)
        yield return new WaitForSeconds(data.dashDuration);

        //set player movement at max speed
        rb.velocity = data.resetVelocityAfterDash ? Vector3.zero : _moveInput * data.runMaxSpeed;

        //unlock player movement
        lockMovement = false;
    }

    private IEnumerator DashCooldown()
    {
        dashAvailable = false;
        yield return new WaitForSeconds(data.dashCooldown);
        dashAvailable = true;
    }
}
