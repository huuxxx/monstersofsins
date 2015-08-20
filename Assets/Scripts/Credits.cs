using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	public GameObject scrollingText;

	public float scrollSpeed = 0.6f;

	public float endTimer = 35f;

	void Start(){
		EventManager.instance.male = false;
		EventManager.instance.female = false;
	}

	void FixedUpdate()
	{
		scrollingText.transform.position += Vector3.up * scrollSpeed * Time.deltaTime;

		endTimer -= Time.deltaTime;

		if (endTimer <= 0)
		{
			Application.LoadLevel ("MainMenu");
		}

		if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.Mouse0) || Input.GetKeyDown (KeyCode.Escape))
		{
			Application.LoadLevel ("MainMenu");
		}
	}
}
