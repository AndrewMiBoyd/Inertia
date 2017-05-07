using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour {

	public AudioSource musicSource;
	public AudioSource attackSound;
	public AudioSource movementSound;
	public static MusicPlayer instance = null;                  

	void Awake ()
	{
		if (instance == null)
			instance = this;
		else
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
	}
		
	public void ChangeMusicVolume (float volume, Slider musicSlider) { 
		musicSource.volume = volume;
		musicSlider.value = volume;
	}

	public void ChangeEffectsVolume (float volume, Slider effectsSlider) {
		attackSound.volume = volume;
		movementSound.volume = volume;
		effectsSlider.value = volume;
	}
		
	public void ChangeMasterVolume (float volume, Slider musicSlider, Slider effectsSlider) {
		ChangeMusicVolume (volume, musicSlider);
		ChangeEffectsVolume (volume, effectsSlider);
	}

	public void PlayAttackSound() {
		attackSound.Play ();
		attackSound.SetScheduledEndTime(AudioSettings.dspTime+(2.0f));
	}

	public void PlayMovementSound() {
		movementSound.Play ();
		movementSound.SetScheduledEndTime(AudioSettings.dspTime+(2.0f));
	}
}