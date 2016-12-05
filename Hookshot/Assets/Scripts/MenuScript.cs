using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Title screen script
/// </summary>
public class MenuScript : MonoBehaviour
{

	public GameObject InstructionsImage;

    public void Quit()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level");
    }
	public void DisplayInstructions()
	{
		InstructionsImage.SetActive (true);
	}
	public void HideInstructions()
	{
		InstructionsImage.SetActive (false);
	}
}
