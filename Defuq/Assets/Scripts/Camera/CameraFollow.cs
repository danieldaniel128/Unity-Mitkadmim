using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public float followVelocity;
	public GameObject target;
	Vector3 newCameraPosition;
	[SerializeField] private float _followSpeed;
	[SerializeField] private float _followDistance;
	private Vector3 _offset;
	void Start()
	{
		newCameraPosition = transform.position;
		_offset = new Vector3(0, 0, _followDistance);
	}

	void FixedUpdate()
	{
		Vector3 cameraPosition = transform.position;
		cameraPosition.y= target.transform.position.y;//inorder to find the direction only on x and z
		Vector3 targetDirection = (target.transform.position - cameraPosition);//vector that points to target
		followVelocity = targetDirection.magnitude * _followSpeed;//gets size of direction vector
		newCameraPosition = transform.position+ (targetDirection.normalized * followVelocity * Time.deltaTime);//old position +direction*size   --> equals to our new wanted position
		transform.position = Vector3.Lerp(transform.position, newCameraPosition + _offset, 0.25f);
	}
}
