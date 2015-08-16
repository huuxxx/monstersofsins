using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
	// Script to handle enemy projectiles

	public Rigidbody projRigid;

	public int projForce = 15;

	private float destroyTimer;

	Vector3 throwAngle = new Vector3(-1, 1, 0);

	void Start ()
	{
		projRigid.AddForce (throwAngle * projForce, ForceMode.Impulse);
	}

	void Update ()
	{
		destroyTimer += Time.deltaTime;

		if (destroyTimer >= 1f)
		{
			GameObject.Destroy(this.gameObject);
		}
	}

}
