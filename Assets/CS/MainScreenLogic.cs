using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


public class MainScreenLogic : MonoBehaviour {

	// Private and Public Variables
	private int timeLeftDelayCounter = 0;

    // Event Functions
    private void Update () {
		DecreaseTime();
        try
        {
            CheckBoundaries();
        }
        catch (Exception)
        {
            //Put something here.
        }
    }

    // Private and Public Functions
    /// <summary>
    /// Decreases the time. The global time left variable is decreased with a delay
    ///	given in <see cref="MainScreenStats"/>. If the timer reaches zero then
    /// the startGameFlag is false and the end game false is true;
    /// </summary>
    private void DecreaseTime()
	{
		int timeLeft = GetComponent< MainScreenStats >().timeLeft;
		if (timeLeft <= 0)
		{
			GetComponent< MainScreenStats >().startGameFlag = false;
			GetComponent< MainScreenStats >().endGameFlag = true;
		}
		else
		{
			if (timeLeftDelayCounter == GetComponent< MainScreenStats >().timeLeftDelayMax)
			{
				timeLeftDelayCounter = 0;
				GetComponent< MainScreenStats >().timeLeft --;
			}
			else
			{
				timeLeftDelayCounter ++;
			}
		}
	}

    /// <summary>
    /// This will be a generic level generation tool for the game. As the player progress it will have to fill
    /// in the gaps of the level. Right now it just creates a bottom floor for the player to stand on.
    /// </summary>
    public void levelGeneration()
    {
        LoadPreFab("Player", "Player", new Vector3(0, -4, 0));
        int maxN = GetComponent<MainScreenStats>().maxNumberOfObjects;
        for (int i = 0; i < maxN; i++)
        {
            LoadPreFab("Floor_1", "Platform_" + i.ToString(), new Vector3( (20.0f/maxN) * i - 10, -5, 0));
        }
    }

    /// <summary>
    /// All floor prefabs get a typical name like Floor_23, etc. This function just loops through all possible prefabs
    /// and destroys them. This destroys the entire level. It is only used when time runs out or the player dies.
    /// </summary>
    public void levelDestruction()
    {
        
        int maxN = GetComponent<MainScreenStats>().maxNumberOfObjects;
        for (int i = 0; i < maxN; i++)
        {
            Destroy(GameObject.Find("Floor_" + i.ToString()));
        }
    }

    /// <summary>
    /// A rect is created around the camera view box. If the player leaves the box then the end game screen appears.
    /// Since the game will involve lots of vertical jumping the height of the allowed space needs to be higher than
    /// the actual camera viewport.
    /// </summary>
    public void CheckBoundaries()
    {
        float height = GetComponent<MainScreenStats>().height; // 
        float width = GetComponent<MainScreenStats>().width;
        Vector3 playerPosition = GameObject.Find("Player").transform.position; // Current player position.
        int scale = 2; // Make the player be able to jump veritcally out of the screen.
        Rect rect = new Rect(-width/2, -height/2 , width, scale * height);
        if (!rect.Contains(playerPosition)) // Game over if not inside box.
        {
            GetComponent<MainScreenStats>().startGameFlag = false;
            GetComponent<MainScreenStats>().endGameFlag = true;
        }
            
    }

	/// <summary>
	/// Loads the pre fab with a name given by prefabName.
	/// </summary>
	/// <param name="prefabName">Prefab name.</param>
	public void LoadPreFab(string prefabName, string uniqueID, Vector3 prefabPosition)
    {
        GameObject prefabObject = GameObject.Instantiate ( (GameObject)Resources.Load (prefabName) );
        prefabObject.name = uniqueID;
        prefabObject.transform.position = prefabPosition;
    }

    /// <summary>
    /// Exits the game.
    /// </summary>
    public void ExitGame()
    {
    	Application.Quit();
    }
}
