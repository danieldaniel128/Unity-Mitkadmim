using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] float _speed;

    private void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            Movement(Vector3.forward);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Movement(Vector3.back);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Movement(Vector3.left);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Movement(Vector3.right);
        }
    }

    void Movement(Vector3 dir)
    {
        Vector3 newPos =  (dir * _speed * Time.deltaTime);
        transform.position += newPos;
    }
}
