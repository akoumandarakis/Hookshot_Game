using UnityEngine;
using System.Collections;

public class OnBossDeathScript : MonoBehaviour {

	public GameObject Boss;
	public AudioSource music;

	private MusicPlayerScript musicPlayer;

	// Use this for initialization
	void Start () {
		musicPlayer = music.GetComponent<MusicPlayerScript> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Boss == null) {
			musicPlayer.VictoryThemeStart ();
		}
	
	}
}
