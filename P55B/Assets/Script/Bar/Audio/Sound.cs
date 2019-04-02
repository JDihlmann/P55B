using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
	#region Variables
	[Header("Components")]
	public AudioClip clip;
	[Space]
	[Header("Variables")]
	public string name;
	[Range(0f, 1f)]
	public float volume = 1;
	[Range(.1f, 3f)]
	public float pitch = 1;
	public bool loop;

	[HideInInspector]
	public AudioSource source;
	#endregion

	#region Methods


	#endregion
}
