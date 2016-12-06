using UnityEngine;
using System.Collections;

public class OnBossRoomEnter : MonoBehaviour {

    public GameObject musicPlayer;
    private MusicPlayerScript musicScript;
	public Camera mainCamera;
	private CameraFollow2D cameraControl;
	public GameObject Boss;
	private BossScript bossControl;
	private MoveScript bossEnter;
	private PolygonCollider2D bossCollider;

	public GameObject BossHPBar;

	private bool entered = false;

	private float entranceCountdown = -11;
	public float entranceTimer = 6;

	// Use this for initialization
	void Start () {
        if (musicPlayer != null)
        {
            musicScript = musicPlayer.GetComponent<MusicPlayerScript>();
        }
		if (mainCamera != null) {
			cameraControl = mainCamera.GetComponent<CameraFollow2D> ();
		}
		if (Boss != null) {
			bossControl = Boss.GetComponent<BossScript> ();
			bossEnter = Boss.GetComponent<MoveScript> ();
			bossCollider = Boss.GetComponent<PolygonCollider2D> ();
			bossControl.enabled = false;
			bossEnter.enabled = false;
			bossCollider.enabled = false;
		}
		entranceCountdown = -11;
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
		if (collider.gameObject.tag == "Player" && !entered)
        {
            musicScript.BossEnter();
			bossEnter.enabled = true;
			//cameraControl.ZoomOut = true;
			entranceCountdown = entranceTimer;
			entered = true;
			BossHPBar.SetActive (true);
        }



    }

	void Update()
	{
		//Count down until next shot
		if (entranceCountdown > 0)
		{
			entranceCountdown -= Time.deltaTime;
		}

		if (entranceCountdown <= 0 && entranceCountdown >= -10) {
			bossEnter.enabled = false;
			bossControl.enabled = true;
			bossCollider.enabled = true;
		}
	
	}
}
