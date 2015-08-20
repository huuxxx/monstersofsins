 using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	// Script to handle player controls and most interactions

	public GameObject player;

	public GameObject playerSprite;

	public GameObject statusBar;

	public Rigidbody playerRigid;

	public Material statusColour;

	public float movementForce = 15;

	public float jumpForce = 75;

	public Animator playerAnimate;

	public bool lockMovement = false;

	public AudioClip[] interactSFX;

	public SpriteRenderer camFlash;

	private AudioSource runningSFX;

	private bool grounded = false;

	private bool rightFaced;

	public bool climbingLadder;

	public float showoffLength = 5f;

	public float showoffTimer;

	private float h;

	private float v;

	private GameObject showOffArea;

	private float startXScale;

	private float statusBarScale;

	private float climbingMod = 1;

	void Start(){
		startXScale = playerSprite.transform.localScale.x;
		runningSFX = GetComponent<AudioSource> ();
	}

	void Awake(){
		startXScale = playerSprite.transform.localScale.x;
		runningSFX = GetComponent<AudioSource> ();
	}

	void FixedUpdate ()
	{
		// Get and set control inputs
		if (climbingLadder) {
			climbingMod = 2.5f;
		} else {
			climbingMod = 1f;
		}
		h = (Input.GetAxis("Horizontal") / climbingMod);
		v = Input.GetAxis("Vertical");
		
		// Function calls
		Grounded ();
		StatusBar ();

		if (showoffTimer <= 0 && !lockMovement) {
			Controls ();
		}
		showoffTimer -= Time.deltaTime;
		
		if (camFlash != null) {
			camFlash.color = new Color (camFlash.color.r, camFlash.color.g, camFlash.color.b, Mathf.Lerp (camFlash.color.a, 0.05f, Time.deltaTime * 1.5f));
		}
	}

	void Update()
	{
		Pause ();
		Animate ();
	}

	// Flip sprite depending on direction
	private void Flip()
	{
		if (rightFaced)
		{
			Vector3 spriteScale = playerSprite.transform.localScale;
			spriteScale.x = startXScale;
			playerSprite.transform.localScale = spriteScale;
		}

		if (!rightFaced)
		{
			Vector3 theScale = playerSprite.transform.localScale;
			theScale.x = -startXScale;
			playerSprite.transform.localScale = theScale;
		}

		
	}

	// Grounded check, uses 3 downward rays - left, middle, right
	private void Grounded()
	{
		Vector3 leftRay = new Vector3 (this.transform.position.x - 1, this.transform.position.y, this.transform.position.z);
		Vector3 rightRay = new Vector3 (this.transform.position.x + 1, this.transform.position.y, this.transform.position.z);

		if (Physics.Raycast (leftRay, Vector3.down, 1.5f) || Physics.Raycast (rightRay, Vector3.down, 1.5f) || Physics.Raycast (transform.position, Vector3.down, 1.5f))
		{
			grounded = true;
		}
		
		else grounded = false;

	}

	// Controls
	private void Controls()
	{
		//Showoff
		if (Input.GetKey (KeyCode.F) && !climbingLadder && showoffTimer < 0f) {
			//Set timer that stops movement and plays animation
			int rng = Random.Range (0, interactSFX.Length);
			if (interactSFX.Length != 0){
				AudioSource.PlayClipAtPoint(interactSFX[rng], transform.position);
			}

			if (EventManager.instance.mainstreamCurrent && camFlash != null){
				Invoke ("CamFlash", 2f);
			}
			showoffTimer = interactSFX[rng].length;
			
			//Add status, disable that friendly.
			if (showOffArea != null){
					showOffArea.SendMessage("Showoff");
			}
		}


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

		//Run sfx
		if (h != 0 && grounded) {
			if (!runningSFX.isPlaying) {
				runningSFX.Play ();
			}
		} else {
			if (runningSFX.isPlaying) {
				runningSFX.Stop ();
			}
		}
		
		// Jump
		if ((grounded == true) && Input.GetButtonDown("Jump") && !climbingLadder)
		{
			if (EventManager.instance.male){
				EventManager.instance.PlaySfx ("PlayerMaleJump");
			} else {
				EventManager.instance.PlaySfx ("PlayerFemJump");
			}

			playerRigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
		}

		// Climb
		if (climbingLadder)
		{
			playerRigid.useGravity = false;

			// Ascend
			if (v > 0)
			{
				player.transform.Translate(Vector3.up * (movementForce / 3f) * Mathf.Abs (v) * Time.deltaTime);
			}

			// Descend
			else player.transform.Translate(Vector3.down * (movementForce / 3f) * Mathf.Abs (v) * Time.deltaTime);
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
			playerRigid.velocity = Vector3.zero;
			climbingLadder = true;
		}

		else climbingLadder = false;

		if (!climbingLadder)
		{
			i = 0;
		}
	}

	// Scale and colour status bar depending on current culture and amound of status
	public void StatusBar()
	{
		if (EventManager.instance.mainstreamCurrent)
		{
			statusBarScale = Mathf.Lerp (statusBarScale, EventManager.instance.mainStatus / 100, Time.deltaTime);
			statusBar.transform.localScale = new Vector3 (statusBarScale * 20, 1, 1);
			if (EventManager.instance.mainStatus <= 20)
			{
				statusColour.color = Color.red;
			}
		
			if (EventManager.instance.mainStatus > 20 && EventManager.instance.mainStatus < 80)
			{
				statusColour.color = Color.yellow;
			}
		
			if (EventManager.instance.mainStatus > 80)
			{
				statusColour.color = Color.green;
			}
		}
		else if (EventManager.instance.subcultureCurrent)
		{
			statusBarScale = Mathf.Lerp (statusBarScale, EventManager.instance.subStatus / 100, Time.deltaTime);
			statusBar.transform.localScale = new Vector3 (statusBarScale * 20, 1, 1);
			if (EventManager.instance.subStatus <= 20)
			{
				statusColour.color = Color.red;
			}
		
			if (EventManager.instance.subStatus > 20 && EventManager.instance.subStatus < 80)
			{
				statusColour.color = Color.yellow;
			}
		
			if (EventManager.instance.subStatus > 80)
			{
				statusColour.color = Color.green;
			}
		}
	}

	void Animate(){

		if (showoffTimer > 0) {
			playerAnimate.SetBool ("Run", false);
			playerAnimate.SetBool ("Jump", false);
			playerAnimate.SetBool ("Showoff", true);
		} else {
			playerAnimate.SetBool ("Showoff", false);
			if (!grounded) {
				playerAnimate.SetBool ("Jump", true);
				playerAnimate.SetBool ("Run", false);
			} else {
				playerAnimate.SetBool ("Jump", false);
			}

			if (h != 0 && grounded) {
				playerAnimate.SetBool ("Run", true);
			} else {

				playerAnimate.SetBool ("Run", false);
			}
		}
	}

	void EnterFriendly(GameObject area){
		showOffArea = area;
	}

	void ExitFriendly(){
		showOffArea = null;
	}

	void Lock(bool lockMov){
		if (lockMov) {
			lockMovement = true;
		} else {
			lockMovement = false;
		}
	}

	void CamFlash(){
		camFlash.color = new Color(camFlash.color.r, camFlash.color.g, camFlash.color.b, 1f);
	}
}
