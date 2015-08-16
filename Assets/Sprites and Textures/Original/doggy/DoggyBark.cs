using UnityEngine;
using System.Collections;

public class DoggyBark : MonoBehaviour {

	AudioSource dogBark;
	Animator dogAnim;
	Transform playerTrans;
	Vector3 startScale;
	float barkTimer = 0f;

	void Start(){
		dogBark = GetComponent<AudioSource> ();
		dogAnim = GetComponent<Animator> ();
		playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		startScale = transform.localScale;
	}

	void Update(){
		if (playerTrans.position.x < transform.position.x) {
			transform.localScale = new Vector3 (-startScale.x, transform.localScale.y, transform.localScale.z);
		} else {
			transform.localScale = new Vector3 (startScale.x, transform.localScale.y, transform.localScale.z);
		}

		if (barkTimer > 0f) {
			barkTimer -= Time.deltaTime;
		} else {
			dogAnim.SetBool("Bark 0", false);
		}
	}
	
	void OnTriggerEnter (Collider col) {
		
		if (col.gameObject.tag == "Player") {
			dogBark.Play();
			dogAnim.SetBool("Bark 0", true);
			barkTimer = 5f;
		}

	}
}
