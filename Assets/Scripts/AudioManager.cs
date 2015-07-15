using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {
	public AudioClip button;
	public AudioClip fail;
	public AudioClip pop;
	public AudioSource soundEffectsSource;
	public static AudioManager aMgr;
	// Use this for initialization
	void Start () {
		soundEffectsSource = GetComponent<AudioSource> ();
		aMgr = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void PlayFailSound()
	{
		soundEffectsSource.Stop ();
		soundEffectsSource.clip = fail;
		soundEffectsSource.Play ();
	}
	
	public void PlayPopSound()
	{
		soundEffectsSource.Stop ();
		soundEffectsSource.clip = pop;
		soundEffectsSource.Play ();
	}
	
	public void PlayButtonSound()
	{
		soundEffectsSource.Stop ();
		soundEffectsSource.clip = button;
		soundEffectsSource.Play ();
	}
}
