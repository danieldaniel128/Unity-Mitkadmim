using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedCameraFollow : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float zfollowSpeed;
    [SerializeField] float xfollowSpeed;
    [SerializeField] float zOffset;
    Vector3 cameraPosition;
    float x;
    float z;

    private void Start()
    {
        cameraPosition = Camera.main.transform.position;
        x= cameraPosition.x;//x is camera x axis coordinate
        z = cameraPosition.z;//z is camera z axis coordinate
    }

    void LateUpdate()
    {
        x += (target.transform.position.x - x) * 0.01f* xfollowSpeed;//moves 1%*followSpeed closer towards our target(player) x axis
        z += (target.transform.position.z - z- zOffset) * 0.01f* zfollowSpeed;//moves 1%*followSpeed closer towards our target(player) z axis
        cameraPosition = new Vector3(x, cameraPosition.y, z);
        Camera.main.transform.position = cameraPosition*Time.timeScale;//incase we want to pause the game(default equals 1)
    }
}
