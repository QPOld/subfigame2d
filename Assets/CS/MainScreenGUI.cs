using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScreenGUI : MonoBehaviour {

	// Private and Public Variables

	// Event Functions
	private void Awake ()
	{
		
	}
	private void OnGUI ()
	{
		if (GetComponent< MainScreenStats>().startGameFlag)
		{
			DisplayMainScreen();
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
		GameObject.Find("Game Text").GetComponent< Text >().text = "";
		GetComponent< MainScreenStats>().startGameFlag = true;
		GetComponent< MainScreenLogic >().LoadPreFab("Player");
	}

	/// <summary>
	/// Updates and Displays information for the main screen.
	/// </summary>
	private void DisplayMainScreen ()
	{
		GameObject.Find("Time Left").GetComponent< Text >().text = GetComponent< MainScreenStats >().timeLeft.ToString();
		GameObject.Find("Total Score").GetComponent< Text >().text = GetComponent< MainScreenStats >().totalScore.ToString();
	}

}