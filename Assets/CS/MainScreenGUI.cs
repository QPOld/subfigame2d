using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainScreenGUI : MonoBehaviour {

    // Private and Public Variables
    GUIContent restartButtonContent = new GUIContent();
    // Event Functions
    private void Awake()
    {
        restartButtonContent.image = Resources.Load("/Texture/Sf Button.psd") as Texture2D;
        restartButtonContent.text = "Restart";
    }
    private void OnGUI ()
	{
		if (GetComponent< MainScreenStats >().startGameFlag)
		{
			DisplayMainScreen();
		}
		else if (GetComponent< MainScreenStats >().endGameFlag)
		{
			DisplayEndScreen();
		}
	}

	// Private and Public Functions

	/// <summary>
	/// Start button for the game. Destorys the game objects for the button 
	/// and turns the flag to start the game to true <see cref="MainScreenStats"/>.
	/// </summary>
	public void StartButton ()
	{
		Destroy(GameObject.Find("Start Button"));
		Destroy(GameObject.Find("Exit Button"));
		GameObject.Find("Game Text").GetComponent< Text >().text = "";
		GetComponent< MainScreenStats>().startGameFlag = true; // Show the main screen.
		GetComponent< MainScreenLogic >().levelGeneration(); // Generate the level.
    }

	/// <summary>
	/// Updates and Displays information for the main screen.
	/// </summary>
	private void DisplayMainScreen ()
	{
		GameObject.Find("Time Left").GetComponent< Text >().text = GetComponent< MainScreenStats >().timeLeft.ToString();
		GameObject.Find("Total Score").GetComponent< Text >().text = GetComponent< MainScreenStats >().totalScore.ToString();
	}

    /// <summary>
    /// This screen is shown when the time runs out or the player falls off.
    /// It destroys the dynamically generated prefabs and displays a restart button.
    /// </summary>
	private void DisplayEndScreen()
	{
		Destroy(GameObject.Find("Player"));
		Destroy(GameObject.Find("Time Left"));
		Destroy(GameObject.Find("Total Score"));
        GetComponent<MainScreenLogic>().levelDestruction(); // Destroy all of the levels.
        GameObject.Find("Game Text").GetComponent< Text >().text = "End Game";
		if (GUI.Button(new Rect(Screen.width/2, Screen.height/2, 60, 30), restartButtonContent)) // Maybe make this has a prefab then have a seperate function for it.
		{
			SceneManager.LoadScene("MainScreen"); // Reloads the screen.
		}
	}
}