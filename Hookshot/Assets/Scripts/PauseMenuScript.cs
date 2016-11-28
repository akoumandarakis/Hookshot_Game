using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour {

    public GameObject PausedUI;
    public GameObject Music;    

    private bool paused = false;

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

        if (paused)
        {
            PausedUI.SetActive(true);
            Time.timeScale = 0;
        }
        if (!paused)
        {
            PausedUI.SetActive(false);
            Time.timeScale = 1;
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

    public void Quit() { }//unimplimented
}
