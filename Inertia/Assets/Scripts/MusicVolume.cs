using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolume : MonoBehaviour {

	public Slider mySlider;

	void Start () {
		mySlider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
	}

	public void ValueChangeCheck() {
		MusicPlayer.instance.ChangeMusicVolume (mySlider.value, mySlider);
	}
}
