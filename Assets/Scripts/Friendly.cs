using UnityEngine;
using System.Collections;

public class Friendly : MonoBehaviour {

	public int numberFriendlies = 1; //set this to the amount of friendlies in this area so you get more status per npc
	public AudioClip praise;
	public SpriteRenderer sprUI;
	public Player playerObj;

		
	void Start(){
		playerObj = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		sprUI = GetComponentInChildren<SpriteRenderer> ();
	}

	void Showoff () {
		if (EventManager.instance.subcultureCurrent){
			EventManager.instance.subStatus += 13;
			EventManager.instance.PlaySfx("StatusGain");
		} else {
			EventManager.instance.mainStatus += 13;
			EventManager.instance.PlaySfx("StatusGain");
		}

		sprUI.enabled = false;
		Invoke ("Praise", playerObj.showoffTimer);
	}

	void OnTriggerEnter(Collider col){
		col.gameObject.SendMessage ("EnterFriendly", this.gameObject, SendMessageOptions.DontRequireReceiver);
	}

	void OnTriggerExit(Collider col){
		col.gameObject.SendMessage ("ExitFriendly", SendMessageOptions.DontRequireReceiver);
	}

	void Praise(){
		AudioSource.PlayClipAtPoint (praise, transform.position); //Has to be a clip @ point because obj is destroyed
		Destroy (this.gameObject, 0.2f);
	}
}
