﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterVolume : MonoBehaviour {

	public Slider mySlider;
	public Slider musicSlider;
	public Slider effectsSlider;

	// Update is called once per frame
	void Update () {
		MusicPlayer.instance.ChangeMasterVolume (mySlider.value, musicSlider, effectsSlider);
	}
}