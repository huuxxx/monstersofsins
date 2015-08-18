using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CultureSelect : MonoBehaviour {

	public Button subButton;
	public Button mainButton;

	public Text subText;
	public Text mainText;

	void Start(){

		if (EventManager.instance.mainstreamComplete && EventManager.instance.subcultureComplete) {
			EventManager.instance.subcultureComplete = false;
			EventManager.instance.mainstreamComplete = false;
		}

		if (EventManager.instance.mainstreamComplete) {
			mainButton.interactable = false;
			mainText.text = "Closed";
		}

		if (EventManager.instance.subcultureComplete) {
			subButton.interactable = false;
			subText.text = "Closed";
		}

	}

	//This is needed because buttons cant find objects loaded from another level.

	public void Subculture () {
		EventManager.instance.SendMessage ("SubcultureGUI");
	}

	public void Mainstream () {
		EventManager.instance.SendMessage ("MainstreamGUI");
	}
}
