using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	// Script to handle player controls and most interactions

	public GameObject player;

	public Rigidbody playerRigid;

	public float movementForce = 15;

	public float jumpForce = 75;

	private bool grounded = false;

	private bool rightFaced;

	public bool climbingLadder;

	private float h;

	private float v;


	void FixedUpdate ()
	{
		// Get and set control inputs
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");

		// Function calls
		Grounded ();
		Controls ();

	}

	void Update()
	{
		Pause ();
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

	// Grounded check, uses 3 downward rays - left, middle, right
	private void Grounded()
	{
		Vector3 leftRay = new Vector3 (this.transform.position.x - 1, this.transform.position.y, this.transform.position.z);
		Vector3 rightRay = new Vector3 (this.transform.position.x + 1, this.transform.position.y, this.transform.position.z);

		if (Physics.Raycast (leftRay, Vector3.down, 2) || Physics.Raycast (rightRay, Vector3.down, 2) || Physics.Raycast (transform.position, Vector3.down, 2))
		{
			grounded = true;
		}
		
		else grounded = false;

	}

	// Controls
	private void Controls()
	{
		// Right
		if (h > 0)
		{
			player.transform.Translate(Vector3.right * movementForce * Mathf.Abs (h) * Time.deltaTime);
			rightFaced = true;
			Flip ();
		}
		
		// Left
		if (h < 0)
		{
			player.transform.Translate(Vector3.left * movementForce * Mathf.Abs (h) * Time.deltaTime);
			rightFaced = false;
			Flip ();
		}
		
		// Jump
		if ((grounded == true) && (v > 0) && (!climbingLadder))
		{
			playerRigid.AddForce(Vector3.up * jumpForce);
		}

		// Climb
		if (climbingLadder)
		{
			playerRigid.useGravity = false;

			// Ascend
			if (v > 0)
			{
				player.transform.Translate(Vector3.up * (movementForce / 2.5f) * Mathf.Abs (v) * Time.deltaTime);
			}

			// Descend
			else player.transform.Translate(Vector3.down * (movementForce / 2.5f) * Mathf.Abs (v) * Time.deltaTime);
		}
		
		else playerRigid.useGravity = true;

	}

	// Pause and Unpause Game
	private void Pause()
	{
		// Pause
		if (Input.GetKeyDown (KeyCode.P))
		{
			if (EventManager.instance.paused == false)
			{
				EventManager.instance.Pause ();
			}

			// Unpause
			else if (EventManager.instance.paused == true)
			{		
				EventManager.instance.Unpause ();
			}
		}		
	}


	// Send message receiver for ladder state
	void Ladder(float ladderState)
	{
		float i = ladderState;

		if (i == 1)
		{
			climbingLadder = true;
		}

		else climbingLadder = false;

		if (!climbingLadder)
		{
			i = 0;
		}
	}
}
