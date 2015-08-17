using UnityEngine;
using System.Collections;

public class CultureSelect : MonoBehaviour {

	//This is needed because buttons cant find objects loaded from another level.
	
	public void Subculture () {
		EventManager.instance.SendMessage ("SubcultureGUI");
	}

	public void Mainstream () {
		EventManager.instance.SendMessage ("MainstreamGUI");
	}
}
