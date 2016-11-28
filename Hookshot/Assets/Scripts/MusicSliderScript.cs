using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class MusicSliderScript : MonoBehaviour {

    public GameObject Music;
    private AudioSource MusicPlayer;

    private Slider MusicSlider;

	// Use this for initialization
	void Start () {
        MusicPlayer = Music.GetComponent<AudioSource>();
        MusicSlider = gameObject.GetComponent<Slider>();

        if (MusicPlayer != null && MusicSlider != null)
        {
            MusicSlider.value = MusicPlayer.volume;
        }
	}
	
	// Update is called once per frame
	public void SliderUpdate(Single value) {
        MusicPlayer.volume = value;
	}
}
