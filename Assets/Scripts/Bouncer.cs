using UnityEngine;
using System.Collections;

public class Bouncer : MonoBehaviour {

	private string levelToLoad;

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "Player") {
			col.gameObject.SendMessage("Lock", true);

			if (EventManager.instance.subcultureCurrent){
				if (EventManager.instance.subStatus >= 85){
					EventManager.instance.PlaySfx ("BouncerApprove");
					if (EventManager.instance.mainstreamComplete && EventManager.instance.subcultureComplete){
						levelToLoad = "Credits";
					} else {
						levelToLoad = "CultureSelection";
					}
					Invoke ("LoadLevel", 4f);
				} else {
					EventManager.instance.PlaySfx ("BouncerDecline");
					levelToLoad = "Subculture";
					Invoke ("LoadLevel", 4f);
				}
			} else {
				if (EventManager.instance.mainStatus >= 85){
					EventManager.instance.PlaySfx ("BouncerApprove");
					if (EventManager.instance.mainstreamComplete && EventManager.instance.subcultureComplete){
						levelToLoad = "Credits";
					} else {
						levelToLoad = "CultureSelection";
					}
					Invoke ("LoadLevel", 4f);
				} else {
					EventManager.instance.PlaySfx ("BouncerDecline");
					levelToLoad = "Mainstream";
					Invoke ("LoadLevel", 4f);
				}
			}
		}
	}

	void LoadLevel(){
		Application.LoadLevel (levelToLoad);
	}
}
