using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
	// Soft dampening camera script

	public float cameraChaseSpeed = 25f;

	private float interpVelocity;

	private float minDistance;

	private float followDistance;

	public GameObject target;
	
	Vector3 targetPos;

	void Start ()
	{
		targetPos = transform.position;
	}

	void FixedUpdate ()
	{
		if (target)
		{
			Vector3 posNoZ = transform.position;

			posNoZ.z = target.transform.position.z;
			
			Vector3 targetDirection = (target.transform.position - posNoZ);
			
			interpVelocity = targetDirection.magnitude * cameraChaseSpeed;
			
			targetPos =  new Vector3 (target.transform.position.x, target.transform.position.y + 3, target.transform.position.z - 10) + (targetDirection.normalized * interpVelocity * Time.deltaTime);
						
			transform.position = Vector3.Lerp( transform.position, targetPos, 0.25f);
			
		}
	}
}
