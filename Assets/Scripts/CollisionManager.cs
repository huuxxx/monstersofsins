using UnityEngine;
using System.Collections;

public class CollisionManager : MonoBehaviour
{
	// Script to handle all collision interactions
	// ATTACH THIS SCRIPT TO ANY OBJECT THAT WILL HAVE A COLLISION INTERACTION
	// THEN SET THE APPROPRIATE BOOLEAN(S) TO 'TRUE' IN THE INSPECTOR

	public GameObject objectDestory;
	
	public bool checkpoint;

	public bool statusPickup;

	public bool damageCollider;

	public bool ladder;

	public bool subcultureNPC;

	public bool mainstreamNPC;

	public bool thirdPartyNPC;

	public AudioClip[] taunts;

	void OnTriggerEnter(Collider temp)
	{
		// Do action determined by bool setting
		if (temp.gameObject.tag == "Player") {

			// End game checkpoint triggers the next scene
			if (checkpoint) {
				if (EventManager.instance.mainstreamCurrent == true && EventManager.instance.mainStatus >= 80) {
					print ("Main stream complete");
					EventManager.instance.mainstreamComplete = true;
					EventManager.instance.CompletionChecker ();
					StartCoroutine("MainstreamComplete");
				} else if (EventManager.instance.mainStatus < 80) {
					print ("Not enough main stream status, restarting level");
					StartCoroutine("MainstreamMaleIntro");
					Application.LoadLevel("Mainstream");
				}

				if (EventManager.instance.subcultureCurrent == true && EventManager.instance.subStatus >= 80) {
					print ("Subculture complete");
					EventManager.instance.subcultureComplete = true;
					EventManager.instance.CompletionChecker ();
				} else if (EventManager.instance.subStatus < 80) {
					print ("Not enough subculture status, restarting level");
					Application.LoadLevel("Subculture");
				}

			}

			// Status gain pickup
			else if (statusPickup) {
				if (EventManager.instance.mainstreamCurrent) {
					EventManager.instance.mainStatus += 10;
					Debug.Log ("Main stream status gained");
					EventManager.instance.PlaySfx ("StatusGain");
				} else if (EventManager.instance.subcultureCurrent) {
					EventManager.instance.subStatus += 10;
					Debug.Log ("Subculture status gained");
					EventManager.instance.PlaySfx ("StatusGain");
				}

			}

			// Damage collider
			else if (damageCollider && EventManager.instance.damageTaken == false) {
				EventManager.instance.DamageManager ();
				EventManager.instance.damageTaken = true;

				int rng = Random.Range (0, taunts.Length);
				if (taunts[rng] != null){
					AudioSource.PlayClipAtPoint(taunts[rng], transform.position);
				}

				if (EventManager.instance.mainstreamCurrent && subcultureNPC == true) {
					EventManager.instance.mainStatus -= 5;
					EventManager.instance.PlaySfx ("StatusLoss");
					print ("Mainstream status lost");
				} else if (EventManager.instance.subcultureCurrent && mainstreamNPC == true) {
					EventManager.instance.subStatus -= 5;
					EventManager.instance.PlaySfx ("StatusLoss");
					print ("Subculture status lost");
				} else if (EventManager.instance.subcultureCurrent && thirdPartyNPC == true) {
					EventManager.instance.subStatus -= 5;
					EventManager.instance.PlaySfx ("StatusLoss");
					print ("Subculture status lost");
				} else if (EventManager.instance.mainstreamCurrent && thirdPartyNPC == true) {
					EventManager.instance.mainStatus -= 5;
					EventManager.instance.PlaySfx ("StatusLoss");
					print ("Mainstream status lost");
				}
			}

			// Ladder collider
			else if (ladder) {
				temp.gameObject.SendMessage ("Ladder", 1f, SendMessageOptions.DontRequireReceiver);
			}
		}

		// NPC kill collider
		if (temp.gameObject.tag == "PlayerFeet") {
			if (damageCollider) {
				Debug.Log (objectDestory + " Killed");
				GameObject.Destroy (objectDestory);
			}
		}
	}

	// Ladder exit send message function
	void OnTriggerExit(Collider temp)
	{
		if (ladder)
			
		{
			temp.gameObject.SendMessage("Ladder", 0f, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Bouncer coroutines
	IEnumerator MainstreamComplete()
	{
		EventManager.instance.PlaySfx ("BoucnerApprove");
		yield return new WaitForSeconds(5f);
		Application.LoadLevel (1);
	}

	
}
