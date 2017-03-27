using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterVolume : MonoBehaviour {

	public Slider mySlider;
	public Slider musicSlider;
	public Slider effectsSlider;

	void Start () {
		mySlider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
	}

	public void ValueChangeCheck() {
		MusicPlayer.instance.ChangeMasterVolume (mySlider.value, musicSlider, effectsSlider);

	}
}
