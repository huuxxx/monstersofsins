using UnityEngine;
using System.Collections;

public class EnemyAI3 : MonoBehaviour {

	// Enemy AI 3 is static and periodically throws projectiles towards the player
	
	public GameObject enemy;

	public GameObject projectile;

	public GameObject projectileSpawn;

	public float throwTimer = 0;
	
	private bool rightFaced;
	

	void Start ()
	{
		rightFaced = false;
		Flip ();
		enemy = this.gameObject;
		projectileSpawn = this.gameObject; //temp until we get animations with actual throw positions.
	}

	void FixedUpdate ()
	{
		throwTimer += Time.deltaTime;

		if (throwTimer >= 1)
		{
			Instantiate(projectile, projectileSpawn.transform.position, Quaternion.identity);
			throwTimer = 0;
		}
		
	}
	
	// Flip sprite depending on direction
	private void Flip()
	{
		if (rightFaced)
		{
			Vector3 spriteScale = transform.localScale;
			spriteScale.x = 1;
			transform.localScale = spriteScale;
		}
		
		if (!rightFaced)
		{
			Vector3 theScale = transform.localScale;
			theScale.x = -1;
			transform.localScale = theScale;
		}
		
	}
}
