using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour {

    public GameObject PausedUI;
    public GameObject DeadUI;

    public GameObject Music;

	public GameObject player;

    public AudioSource loseSound;

    private bool paused = false;
    private bool dead = false;

    void Start()
    {
        PausedUI.SetActive(false);

    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
        }

        if (paused && !dead)
        {
            PausedUI.SetActive(true);
            Time.timeScale = 0;
			if (player != null) {
				player.GetComponentInChildren<WeaponScript> ().enabled = false;
				player.GetComponent<PlayerScript> ().enabled = false;
			}
        }
        if (!paused)
        {
            PausedUI.SetActive(false);
            Time.timeScale = 1;
			if (player != null) {
				player.GetComponentInChildren<WeaponScript> ().enabled = true;
				player.GetComponent<PlayerScript> ().enabled = true;
			}
        }
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void Resume()
    {
        paused = false;
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OnPlayerDeath()
    {
        if (loseSound != null)
        {
			loseSound.Play ();
        }

        dead = true;
        Time.timeScale = 0f;
        if (DeadUI != null)
        {
            DeadUI.SetActive(true);
        }
    }
}
