using UnityEngine;
using System.Collections;

public class OnBossRoomExit : MonoBehaviour {

    public GameObject musicPlayer;
    private MusicPlayerScript musicScript;

    // Use this for initialization
    void Start()
    {
        if (musicPlayer != null)
        {
            musicScript = musicPlayer.GetComponent<MusicPlayerScript>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            musicScript.BossExit();
        }
    }
}
