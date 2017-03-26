using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectVolume : MonoBehaviour {

	public Slider mySlider;

	// Update is called once per frame
	void Update () {
		MusicPlayer.instance.ChangeEffectsVolume (mySlider.value, mySlider);
	}
}
