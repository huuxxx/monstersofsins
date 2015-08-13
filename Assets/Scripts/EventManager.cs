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
	private AudioSource _musicSource = null;
	
	[SerializeField]
	private AudioSource _sfxSource = null;
	
	[SerializeField]
	private AudioClip[] _soundEffects = null;
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
		get { return !_musicSource.mute; }
		set
		{
			if (_musicSource.mute == value)
			{
				_musicSource.mute = !value;
				_sfxSource.mute = !value;
				PlayerPrefs.SetInt(audioEnabledKey, value ? 1 : 0);
				
				if (!value)
				{
					_musicSource.Stop();
				}
				else
				{
					_musicSource.Play();
				}
			}
		}
	}

	public float musicPlayTime
	{
		get { return _musicSource.time; }
	}
	
	public void PlaySfx(string name)
	{
		foreach (var clip in _soundEffects)
		{
			if (clip.name == name)
			{
				_sfxSource.PlayOneShot(clip);
				break;
			}
		}
	}
	
	public void ToggleAudio()
	{
		audioEnabled = !audioEnabled;
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
