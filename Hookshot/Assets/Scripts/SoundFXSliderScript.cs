using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class SoundFXSliderScript : MonoBehaviour {

    Slider soundSlider;

	// Use this for initialization
	void Start () {
        soundSlider = gameObject.GetComponent<Slider>();
        soundSlider.value = AudioListener.volume;
	}
	
	public void SoundSliderValueChange(Single value)
    {
        AudioListener.volume = value;
    }
}
