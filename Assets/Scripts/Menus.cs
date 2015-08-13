using UnityEngine;
using System.Collections;

public class Menus : MonoBehaviour
{
	void Start ()
	{
	
	}

	void Update ()
	{
		if (Input.GetKeyDown ("space"))
		{
			Application.LoadLevel(1);
			EventManager.states CurrentState = EventManager.states.MAINSTREAM;
		}
	}
}
