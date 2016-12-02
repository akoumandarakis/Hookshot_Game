using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Title screen script
/// </summary>
public class MenuScript : MonoBehaviour
{
    void OnGUI()
    {
		UnityEngine.Cursor.visible = true;

        const int buttonWidth = 84;
        const int buttonHeight = 60;

        if (GUI.Button(
                new Rect(
                    Screen.width / 2 - ( buttonWidth / 2),
                    (2 * Screen.height / 3) - (buttonHeight / 2),
                    buttonWidth,
                    buttonHeight
                    ),
                "Start"))
        {
            SceneManager.LoadScene("Level");
        }
    }
}
