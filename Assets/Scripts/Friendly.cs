using UnityEngine;
using System.Collections;

public class Friendly : MonoBehaviour {

	public int numberFriendlies = 1; //set this to the amount of friendlies in this area so you get more status per npc
		
	void Showoff () {
		if (EventManager.instance.subcultureCurrent){
			EventManager.instance.subStatus += numberFriendlies * 10;
		} else {
			EventManager.instance.mainStatus += numberFriendlies * 10;
		}

		Destroy (this.gameObject);
	}

	void OnTriggerEnter(Collider col){
		col.gameObject.SendMessage ("EnterFriendly", this.gameObject, SendMessageOptions.DontRequireReceiver);
	}

	void OnTriggerExit(Collider col){
		col.gameObject.SendMessage ("ExitFriendly", SendMessageOptions.DontRequireReceiver);
	}
}
