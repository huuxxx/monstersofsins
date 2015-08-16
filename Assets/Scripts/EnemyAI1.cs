using UnityEngine;
using System.Collections;

public class EnemyAI1 : MonoBehaviour
{
	// Enemy AI 1 moves laterally until colliding with a wall, then switches direction

	public GameObject enemy;

	public float movementForce = 5;

	private bool walledLeft = false;

	private bool walledRight = false;

	private float spawnDir;

	private bool rightFaced;

	void Start ()
	{
		enemy = this.gameObject;
		// Roll a number for first direction to move
		spawnDir = Random.Range (1, 10);

		// Commence move upon spawning
		if (spawnDir <= 5)
		{
			walledLeft = true;
		}
		
		else walledRight = true;

	}

	void FixedUpdate ()
	{
		Movement ();
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

	private void Movement()
	{
		// Walled check
		if (Physics.Raycast (enemy.transform.position, Vector3.left, 1))
		{
			walledLeft = true;
			walledRight = false;
		}
		
		if (Physics.Raycast (enemy.transform.position, Vector3.right, 1))
		{
			walledRight = true;
			walledLeft = false;
		}
		
		// Move away from wall
		if (walledLeft)
		{
			enemy.transform.Translate(Vector3.right * movementForce * Time.deltaTime);
			rightFaced = true;
			Flip ();
		}
		
		if (walledRight)
		{
			enemy.transform.Translate(Vector3.left * movementForce * Time.deltaTime);
			rightFaced = false;
			Flip ();
		}
	}

}
