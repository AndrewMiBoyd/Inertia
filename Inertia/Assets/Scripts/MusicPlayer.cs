using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour {

	public AudioSource musicSource;
	//public AudioSource soundEffects;
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
		//soundEffects.volume = volume;
		effectsSlider.value = volume;
	}
		
	public void ChangeMasterVolume (float volume, Slider musicSlider, Slider effectsSlider) {
		ChangeMusicVolume (volume, musicSlider);
		ChangeEffectsVolume (volume, effectsSlider);
	}
}