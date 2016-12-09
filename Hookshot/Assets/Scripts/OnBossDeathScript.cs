using UnityEngine;
using System.Collections;

public class OnBossDeathScript : MonoBehaviour {

	public GameObject Boss;
	public AudioSource music;
    public GameObject UIContainer;

	private MusicPlayerScript musicPlayer;
    private PauseMenuScript UI;
    private HealthScript bossHP;

	// Use this for initialization
	void Start () {
		musicPlayer = music.GetComponent<MusicPlayerScript> ();
        UI = UIContainer.GetComponent<PauseMenuScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Boss == null) {
			musicPlayer.VictoryThemeStart ();
            UI.OnVictory();
        }
	}
}
