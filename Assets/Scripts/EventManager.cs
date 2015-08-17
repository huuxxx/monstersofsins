using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour

	// Persistent singleton class to manage overall game data
	// ATTACH ONE OF THESE SCRIPTS TO AN EMPTY GAME OBJECT IN EVERY SCENE
	// IT WILL AUTOMATICALLY SEARCH AND DESTROY DUPLICATES

{
	#region Game variables

	// Player variables

	public float mainStatus;

	public float subStatus;

	public bool male;

	public bool female;
	
	// Game state variables

	public int checkPoint = 0;

	public bool paused = false;
	
	public bool damageTaken = false;

	public float invulnTimer = 0;

	public bool mainstreamCurrent = false;

	public bool mainstreamComplete = false;

	public bool subcultureCurrent = false;

	public bool subcultureComplete = false;

	// Audio variables

	private const string audioEnabledKey = "IsAudioEnabled";
	
	[SerializeField]
	private AudioSource musicSource = null;
	
	[SerializeField]
	private AudioSource sfxSource = null;
	
	[SerializeField]
	private AudioClip[] soundEffects = null;

	[SerializeField]
	private AudioClip[] riffs = null;
	
	// GUI variables

	public GameObject hallPicture;

	public GameObject classPicture;

	public GameObject failPicture;

	public GameObject burgerKanePicture;

	#endregion

	#region GameStates
	// Finite state machine for current scene
	// YOU MUST SET THE CURRENT STATE EXTERNALLY UPON CHANGING SCENE

	public enum states
	{
		MAINMENU,
		CULTURESELECT,
		MAINSTREAM,
		SUBCULTURE,
		NONE,
	}
	
	states CurrentState = states.NONE;

	public void GameStates()
	{
		if (CurrentState == states.NONE)
		{
			return;
		}
	}
	
	IEnumerator MainMenu()
	{
		yield return 0;
	}

	IEnumerator CultureSelect()
	{
		yield return 0;
	}

	IEnumerator Mainstream()
	{
		yield return 0;
	}

	IEnumerator Subculture()
	{
		yield return 0;
	}
	#endregion

	// Set the singleton and maintain it

	private static EventManager inst;

	public static EventManager instance
	{
		get
		{
			if(inst == null)
			{
				inst = GameObject.FindObjectOfType<EventManager>();
				
				DontDestroyOnLoad(inst.gameObject);
			}
			
			return inst;
		}
	}

	#region Audio Functions
	public bool audioEnabled
	{
		get { return !musicSource.mute; }
		set
		{
			if (musicSource.mute == value)
			{
				musicSource.mute = !value;
				sfxSource.mute = !value;
				PlayerPrefs.SetInt(audioEnabledKey, value ? 1 : 0);
				
				if (!value)
				{
					musicSource.Stop();
				}
				else
				{
					musicSource.Play();
				}
			}
		}
	}

	public float musicPlayTime
	{
		get { return musicSource.time; }
	}
	
	public void PlaySfx(string name)
	{
		foreach (var clip in soundEffects)
		{
			if (clip != null){
				if (clip.name == name)
				{
					sfxSource.PlayOneShot(clip);
					break;
				}
			}
		}
	}
	
	public void ToggleAudio()
	{
		audioEnabled = !audioEnabled;
	}
	#endregion

	#region GUI Functions
	public void Male()
	{
		male = true;
		Application.LoadLevel (1);
	}
	
	public void Female()
	{
		female = true;
		Application.LoadLevel (1);
	}

	IEnumerator MainstreamMaleIntro()
	{
		EventManager.instance.PlaySfx ("MaleMainOpening");
		Instantiate (hallPicture, new Vector3 (0, 5, -6), Quaternion.identity);
		//hallPicture.gameObject.SetActive(true);
		yield return new WaitForSeconds(9.3f);
		Instantiate (classPicture, new Vector3 (0, 5, -7), Quaternion.identity);
		//hallPicture.gameObject.SetActive(false);
		//classPicture.gameObject.SetActive(true);
		yield return new WaitForSeconds(4f);
		Instantiate (failPicture, new Vector3 (0, 5, -8), Quaternion.identity);
		//classPicture.gameObject.SetActive(false);
		//failPicture.gameObject.SetActive(true);
		yield return new WaitForSeconds(7f);
		Instantiate (burgerKanePicture, new Vector3 (0, 5, -9), Quaternion.identity);
		//failPicture.gameObject.SetActive(false);
		//burgerKanePicture.gameObject.SetActive(true);
		yield return new WaitForSeconds(8f);
		Application.LoadLevel ("Mainstream");
		EventManager.instance.PlaySfx ("MaleMainGameStart");
		musicSource.Stop ();
	}

	IEnumerator MainstreamFemaleIntro()
	{
		EventManager.instance.PlaySfx ("FemMainOpening");
		Instantiate (hallPicture, new Vector3 (0, 5, -6), Quaternion.identity);
		//hallPicture.gameObject.SetActive(true);
		yield return new WaitForSeconds(11f);
		Instantiate (classPicture, new Vector3 (0, 5, -7), Quaternion.identity);
		//hallPicture.gameObject.SetActive(false);
		//classPicture.gameObject.SetActive(true);
		yield return new WaitForSeconds(6f);
		Instantiate (failPicture, new Vector3 (0, 5, -8), Quaternion.identity);
		//classPicture.gameObject.SetActive(false);
		//failPicture.gameObject.SetActive(true);
		yield return new WaitForSeconds(16f);
		Instantiate (burgerKanePicture, new Vector3 (0, 5, -9), Quaternion.identity);
		//failPicture.gameObject.SetActive(false);
		//burgerKanePicture.gameObject.SetActive(true);
		yield return new WaitForSeconds(6f);
		Application.LoadLevel ("Mainstream");
		EventManager.instance.PlaySfx ("FemMainGameStart");
		musicSource.Stop ();
	}

	IEnumerator SubcultureMaleIntro()
	{
		EventManager.instance.PlaySfx ("MaleSubOpening");
		Instantiate (hallPicture, new Vector3 (0, 5, -6), Quaternion.identity);
		//hallPicture.gameObject.SetActive(true);
		yield return new WaitForSeconds(6f);
		Instantiate (classPicture, new Vector3 (0, 5, -7), Quaternion.identity);
		//hallPicture.gameObject.SetActive(false);
		//classPicture.gameObject.SetActive(true);
		yield return new WaitForSeconds(8f);
		Instantiate (failPicture, new Vector3 (0, 5, -8), Quaternion.identity);
		//classPicture.gameObject.SetActive(false);
		//failPicture.gameObject.SetActive(true);
		yield return new WaitForSeconds(11f);
		Instantiate (burgerKanePicture, new Vector3 (0, 5, -9), Quaternion.identity);
		//failPicture.gameObject.SetActive(false);
		//burgerKanePicture.gameObject.SetActive(true);
		yield return new WaitForSeconds(12f);
		Application.LoadLevel ("Subculture");
		EventManager.instance.PlaySfx ("MaleSubGameStart");
		musicSource.Stop ();
	}

	IEnumerator SubcultureFemaleIntro()
	{
		EventManager.instance.PlaySfx ("FemSubOpening");
		Instantiate (hallPicture, new Vector3 (0, 5, -6), Quaternion.identity);
		//hallPicture.gameObject.SetActive(true);
		yield return new WaitForSeconds(7.5f);
		Instantiate (classPicture, new Vector3 (0, 5, -7), Quaternion.identity);
		//hallPicture.gameObject.SetActive(false);
		//classPicture.gameObject.SetActive(true);
		yield return new WaitForSeconds(8.3f);
		Instantiate (failPicture, new Vector3 (0, 5, -8), Quaternion.identity);
		//classPicture.gameObject.SetActive(false);
		//failPicture.gameObject.SetActive(true);
		yield return new WaitForSeconds(11f);
		Instantiate (burgerKanePicture, new Vector3 (0, 5, -9), Quaternion.identity);
		//failPicture.gameObject.SetActive(false);
		//burgerKanePicture.gameObject.SetActive(true);
		yield return new WaitForSeconds(9f);
		Application.LoadLevel ("Subculture");
		EventManager.instance.PlaySfx ("FemSubGameStart");
		musicSource.Stop ();
	}

	public void MainstreamGUI()
	{
		mainstreamCurrent = true;
		//mainstreamComplete = true;

		if (male)
		{
			StartCoroutine("MainstreamMaleIntro");
		}
		else StartCoroutine("MainstreamFemaleIntro");

	}
	
	public void SubcultureGUI()
	{
		subcultureCurrent = true;
		//subcultureComplete = true;

		if (male)
		{
			StartCoroutine("SubcultureMaleIntro");
		}
		else StartCoroutine("SubcultureFemaleIntro");

	}
	#endregion

	void Awake() 
	{
		// Set starting status
		mainStatus = 20;
		subStatus = 20;

		// Protect script from being destroyed
		if(inst == null)
		{
			inst = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			// Delete duplicate singletons
			if(this != inst)
				Destroy(this.gameObject);
		}
	}

	// Event Handler to determine which level/scene is to be loaded
	public void CompletionChecker()
	{
		if (mainstreamComplete == true && subcultureComplete == true)
		{
			print ("Game Complete!");
			// load end game
		}
		else if (mainstreamComplete == true && subcultureComplete == false)
		{
			subcultureCurrent = true;
			mainstreamCurrent = false;
			// load subculture
		}

		else if (mainstreamComplete == false && subcultureComplete == true)
		{
			subcultureCurrent = false;
			mainstreamCurrent = true;
			// load mainstream
		}
	}

	// Make player temporarily invulnerable after taking damage and check for death
	public void DamageManager()
	{
		if (mainstreamCurrent && mainStatus <= 0)
		{
			Application.LoadLevel ("Mainstream");
		}

		if (subcultureCurrent && subStatus <= 0)
		{
			Application.LoadLevel ("Subculture");
		}

		if (damageTaken)
		{
			invulnTimer += Time.deltaTime;
		}
		
		if (invulnTimer >= 1.5f)
		{
			damageTaken = false;
			invulnTimer = 0;
		}
	}

	// Reset Player Data function
	public void NewGame()
	{
		subcultureComplete = false;
		mainstreamComplete = false;
		mainStatus = 20;
		subStatus = 20;
	}

	// Pause function
	public void Pause()
	{
		print ("Paused");
		Time.timeScale = 0;
		paused = true;
	}

	// Unpause function
	public void Unpause()
	{
		Time.timeScale = 1;
		print ("Unpaused");
		paused = false;
	}
}
