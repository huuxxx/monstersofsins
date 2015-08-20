using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public void LoadCredits(){
		Application.LoadLevel ("Credits");
	}

	public void Male(){
		EventManager.instance.Male ();
	}

	public void Female(){
		EventManager.instance.Female ();
	}
}
