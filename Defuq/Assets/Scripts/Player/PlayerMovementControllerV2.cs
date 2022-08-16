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
    Vector3 _camForward;
    Vector3 _camOffset;
    Vector3 _moveW_Offset;
    bool dashAvailable = true;
    bool lockMovement = false;

    //Vector3 targetDir = target.position - transform.position;
    //angleBetween = Vector3.Angle(transform.forward, targetDir);

    private void Start()
    {
        Camera mainCamera = Camera.main;
        _camForward = mainCamera.transform.forward;
        Debug.Log(_camForward);
        Debug.Log(_camForward.normalized);
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw(HORIZONTAL_AXIS);
        float verticalInput = Input.GetAxisRaw(VERTICAL_AXIS);

        _moveInput = new Vector3(horizontalInput, 0f, verticalInput).normalized; //recieve input from player
        _camOffset = new Vector3(_moveInput.x - _camForward.x, _moveInput.y,_moveInput.z-_camForward.z);
        Vector3 fuckoff = new Vector3(horizontalInput, 0f, verticalInput) - _camForward;
        Debug.Log("input not norm - cam: " + fuckoff);
        Debug.Log("normalized: " + fuckoff.normalized);

        if (Input.GetKeyDown(data.dashKey) && dashAvailable)
        {
            Dash();
        }

    }


    private void FixedUpdate()
    {
        if (!lockMovement)
        {
            Move();
        }
    }
    private void Dash()
    {
        StartCoroutine(DashCooldown());
        StartCoroutine(DashMovement());
    }

    private void Move()
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
            Debug.Log("Movement: " + movement);
            //_moveW_Offset = new Vector3(movement.x - _camOffset.x, movement.y, movement.z - _camOffset.y);
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
