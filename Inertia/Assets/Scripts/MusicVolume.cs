using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolume : MonoBehaviour {

	public Slider mySlider;

	// Update is called once per frame
	void Update () {
		MusicPlayer.instance.ChangeMusicVolume (mySlider.value, mySlider);
	}

//	public void OnValueChanged(Slider mySlider) {
//		
//	}
}
