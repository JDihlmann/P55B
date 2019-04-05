using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance { get; private set; }

	#region Variables
	[Header("Components")]
	public Sound[] sounds;
	[Space]
	[Header("Variables")]
	public bool muted;
	#endregion

	#region Methods
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}

	private void Start()
	{
		Play("Theme");	
	}

	public void Play(string name)
	{
		if (!muted)
		{
			Sound s = Array.Find(sounds, sound => sound.name == name);
			if (s == null)
			{
				Debug.LogWarning("Sound: " + name + " not found!");
				return;
			}
			s.source.Play();
		}
		else
		{
			// Debug.LogWarning("WARNING: Audio is muted");
		}
	}

	public void MuteAudio()
	{
		muted = true;
	}

	public void UnmuteAudio()
	{
		muted = false;
	}

	#endregion
}
