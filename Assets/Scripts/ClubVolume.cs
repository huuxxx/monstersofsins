using UnityEngine;
using System.Collections;

public class ClubVolume : MonoBehaviour {

	public float clubMaxVol = 1f;
	private Transform playerPos = null;
	private AudioSource clubVol;
	private float maxDistance;
	private float currentDistance;

	void Start () {
		clubVol = GetComponent<AudioSource> ();
		Invoke ("FindPlayer", 1f); // Had to invoke because player gender swap takes a second
	}

	void Update () {
		if (playerPos != null) {
			currentDistance = transform.position.x - playerPos.transform.position.x;
			clubVol.volume = Mathf.Lerp (clubMaxVol, -0.2f, currentDistance / maxDistance);
		}
	}

	void FindPlayer (){
		playerPos = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		maxDistance = transform.position.x - playerPos.transform.position.x;
	}
}
