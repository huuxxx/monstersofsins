using UnityEngine;
using System.Collections;

public class EnemyAI2 : MonoBehaviour
{
	// Enemy AI 2 moves laterally until colliding with a wall, then switches direction
	// Enemy AI 2 will momentarily jump

	public GameObject enemy;

	public Rigidbody enemyRigid;
	
	public float movementForce = 5;

	public float jumpForce = 5;
	
	private bool walledLeft = false;
	
	private bool walledRight = false;
	
	private float spawnDir;

	private float jumpTimer = 0;

	private int jumpInterval = 4;

	private bool rightFaced;

	public Animator anim;
	
	void Start ()
	{
		anim.SetBool ("Run", true);
		enemy = this.gameObject;
		enemyRigid = GetComponent<Rigidbody> ();

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

		// Increment timer
		jumpTimer += Time.deltaTime;	
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

	// AI Movement
	private void Movement()
	{
		// Jump
		if (jumpTimer >= jumpInterval)
		{
			StartCoroutine("Jump");		
		}
		
		// Walled check
		if (Physics.Raycast (enemy.transform.position, Vector3.left, 2))
		{
			walledLeft = true;
			walledRight = false;
		}
		
		if (Physics.Raycast (enemy.transform.position, Vector3.right, 2))
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

	// Jump coroutine
	IEnumerator Jump()
	{
		enemyRigid.AddForce(Vector3.up * jumpForce);
		yield return new WaitForSeconds(.25f);
		jumpTimer = 0;
	}

}
