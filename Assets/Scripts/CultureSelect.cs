using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CultureSelect : MonoBehaviour {

	public Button subButton;
	public Button MainButton;

	public Text subText;
	public Text MainText;

	void Start(){

		if (EventManager.instance.mainstreamComplete && EventManager.instance.subcultureComplete) {
			EventManager.instance.subcultureComplete = false;
			EventManager.instance.mainstreamComplete = false;
		}

		if (EventManager.instance.mainstreamComplete) {
			MainButton.interactable = false;
			subText.text = "Closed";
		}

		if (EventManager.instance.subcultureComplete) {
			subButton.interactable = false;
			MainText.text = "Closed";
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
