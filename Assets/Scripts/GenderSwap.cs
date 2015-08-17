using UnityEngine;
using System.Collections;

public class GenderSwap : MonoBehaviour {

	public GameObject female;

	void Start () {
		if (EventManager.instance.female) {
			female.SetActive (true);
			this.gameObject.SetActive (false);
		}
	}
}
