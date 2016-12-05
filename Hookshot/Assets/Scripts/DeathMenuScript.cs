using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeathMenuScript : MonoBehaviour {

	public AudioSource loseSound;
	public bool played = false;

	void OnGUI()
    {
        const int buttonWidth = 120;
        const int buttonHeight = 60;

		if (loseSound != null && !played) {
			loseSound.Play ();
			played = true;
		}

        if (GUI.Button(
                new Rect(
                    Screen.width / 2 - (buttonWidth / 2),
                    (1* Screen.height / 3) - (buttonHeight / 2),
                    buttonWidth,
                    buttonHeight),
                "Restart"))
        {
            SceneManager.LoadScene("Level");
        }

        if (GUI.Button(
                new Rect(
                    Screen.width / 2 - (buttonWidth / 2),
                    (2 * Screen.height / 3) - (buttonHeight / 2),
                    buttonWidth,
                    buttonHeight),
                "Back to Menu"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
