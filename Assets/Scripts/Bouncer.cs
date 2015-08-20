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
						EventManager.instance.subcultureCurrent = false;
						levelToLoad = "Credits";
					} else {
						EventManager.instance.subcultureCurrent = false;
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
						EventManager.instance.mainstreamCurrent = false;
						levelToLoad = "Credits";
					} else {
						EventManager.instance.mainstreamCurrent = false;
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
		if (levelToLoad == "Credits") {
			EventManager.instance.musicSource.Play();
		}
		Application.LoadLevel (levelToLoad);
	}
}
