using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour

	// Persistent singleton class to manage overall game data
	// ATTACH ONE OF THESE SCRIPTS TO AN EMPTY GAME OBJECT IN EVERY SCENE
	// IT WILL AUTOMATICALLY SEARCH AND DESTROY DUPLICATES

{
	#region Game variables

	// Player variables

	public int mainStatus;

	public int subStatus;

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

	public AudioSource menuAudioSource;

	public AudioSource cultureselectAudioSource;

	public AudioSource mainstreamAudioSource;

	public AudioSource subcultureAudioSource;

	// GUI variables

	public GameObject hallPicture;

	public GameObject classPicture;

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
		musicSource = menuAudioSource;
		sfxSource = menuAudioSource;
		yield return 0;
	}

	IEnumerator CultureSelect()
	{
		musicSource = cultureselectAudioSource;
		sfxSource = cultureselectAudioSource;
		yield return 0;
	}

	IEnumerator Mainstream()
	{
		musicSource = mainstreamAudioSource;
		sfxSource = mainstreamAudioSource;
		yield return 0;
	}

	IEnumerator Subculture()
	{
		musicSource = subcultureAudioSource;
		sfxSource = subcultureAudioSource;
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
			if (clip.name == name)
			{
				sfxSource.PlayOneShot(clip);
				break;
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
		hallPicture.gameObject.SetActive(true);
		yield return new WaitForSeconds(5f);
		hallPicture.gameObject.SetActive(false);
		classPicture.gameObject.SetActive(true);
		yield return new WaitForSeconds(5f);
		Application.LoadLevel (2);
		
	}

	IEnumerator MainstreamFemaleIntro()
	{
		hallPicture.gameObject.SetActive(true);
		yield return new WaitForSeconds(5f);
		hallPicture.gameObject.SetActive(false);
		classPicture.gameObject.SetActive(true);
		yield return new WaitForSeconds(5f);
		Application.LoadLevel (2);
		
	}

	IEnumerator SubcultureMaleIntro()
	{
		hallPicture.gameObject.SetActive(true);
		yield return new WaitForSeconds(5f);
		hallPicture.gameObject.SetActive(false);
		classPicture.gameObject.SetActive(true);
		yield return new WaitForSeconds(5f);
		Application.LoadLevel (3);
		
	}

	IEnumerator SubcultureFemaleIntro()
	{
		hallPicture.gameObject.SetActive(true);
		yield return new WaitForSeconds(5f);
		hallPicture.gameObject.SetActive(false);
		classPicture.gameObject.SetActive(true);
		yield return new WaitForSeconds(5f);
		Application.LoadLevel (3);
		
	}

	public void MainstreamGUI()
	{
		NewGame ();
		mainstreamCurrent = true;

		if (male == true)
		{
			StartCoroutine("MainstreamMaleIntro");
		}
		else StartCoroutine("MainstreamFemaleIntro");

	}
	
	public void SubcultureGUI()
	{
		NewGame ();
		subcultureCurrent = true;

		if (male == true)
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
			//death and reload main
		}

		if (subcultureCurrent && subStatus <= 0)
		{
			//death and reload sub
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
