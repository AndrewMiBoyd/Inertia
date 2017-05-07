using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour {

	public AudioSource musicSource;
	public AudioSource attackSound;
	public AudioSource movementSound;
	public static MusicPlayer instance = null;
	private float maxVolume = 1.0f;

	void Awake ()
	{
		if (instance == null)
			instance = this;
		else
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
	}
		
	public void ChangeMusicVolume (float volume, Slider musicSlider) {
		if (volume <= maxVolume) {
			musicSource.volume = volume;
			musicSlider.value = volume;
		} else {
			musicSource.volume = maxVolume;
			musicSlider.value = maxVolume;
		}
	}

	public void ChangeEffectsVolume (float volume, Slider effectsSlider) {

		if (volume <= maxVolume) {
			attackSound.volume = volume;
			movementSound.volume = volume;
			effectsSlider.value = volume;
		} else {
			attackSound.volume = maxVolume;
			movementSound.volume = maxVolume;
			effectsSlider.value = maxVolume;
		}
	}
		
	public void ChangeMasterVolume (float volume, Slider musicSlider, Slider effectsSlider) {
		maxVolume = volume;
		ChangeMusicVolume (maxVolume, musicSlider);
		ChangeEffectsVolume (maxVolume, effectsSlider);
	}

	public void PlayAttackSound() {
		attackSound.Play ();
		attackSound.SetScheduledEndTime(AudioSettings.dspTime+(2.0f));
	}

	public void PlayMovementSound() {
		movementSound.Play ();
		movementSound.SetScheduledEndTime(AudioSettings.dspTime+(2.0f));
	}

	private void SetMaxVolume() {

	}
}