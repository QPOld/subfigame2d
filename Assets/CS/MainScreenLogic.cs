using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


public class MainScreenLogic : MonoBehaviour {

	// Private and Public Variables
	private int timeLeftDelayCounter = 0; // Temp var to count.

    // Event Functions
    private void Update () {
		DecreaseTime();
        try // The player doesn't exist at all times so try it.
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
		if (timeLeft <= 0) // Player ran out of time.
		{
			GetComponent< MainScreenStats >().startGameFlag = false; // Turn main screen off.
			GetComponent< MainScreenStats >().endGameFlag = true; // Turn end screen on.
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
    /// This will be a generic level generation tool for the game Right now it just creates a bottom floor for the player to stand on.
    /// Walkable floors need a platform ID, Spikes need damage ID, flowers need boost ID, food/drink need energy ID, etc.
    /// </summary>
    public void levelGeneration()
    {
        LoadPreFab("Player", "Player", new Vector3(0, -4, 0));
        int maxN = GetComponent<MainScreenStats>().maxNumberOfObjects;
        // Floor
        for (int i = 0; i < maxN; i++)
        {
            LoadPreFab("Floor_1", "Platform_" + i.ToString(), new Vector3( (20.0f/maxN) * i - 10, -5, 0));
        }
    }

    /// <summary>
    /// As the player progress the game will need to be able to dynamically change the world around the player. Instead of creating
    /// and destory then recreating the game will move the game objects. Unless there is some bottleneck with multiple lerp happening
    /// at once it should be much faster and smoother. 
    /// </summary>
    public void levelEvolution()
    {
        int maxN = GetComponent<MainScreenStats>().maxNumberOfObjects;

    }

    /// <summary>
    /// All floor prefabs get a name like Platform_##, etc. This function just loops through all possible prefabs
    /// and destroys them. This destroys the entire level. It is only used when time runs out or the player dies.
    /// </summary>
    public void levelDestruction()
    {
        
        int maxN = GetComponent<MainScreenStats>().maxNumberOfObjects;
        for (int i = 0; i < maxN; i++)
        {
            Destroy(GameObject.Find("Platform_" + i.ToString()));
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
        Rect rect = new Rect(-width/2, -height/2 , width, scale * height); // A rectangle made of the actual view.
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
    /// Finds the prefab with the name uniqueID and translate to the position to prefabPosition.
    /// </summary>
    /// <param name="uniqueID">Name of the prefab.</param>
    /// <param name="newPrefabPosition">The new prefab Position.</param>
    /// <param name="time">The time it takes to do the LERP.</param>
    /// <returns></returns>
    private IEnumerator TranslateAndRotatePrefab(string uniqueID, Vector3 newPrefabPosition, Quaternion newPrefabRotation, float time)
    {
        float elapsedTime = 0;
        Vector3 oldPrefabPosition = GameObject.Find(uniqueID).transform.position;
        Quaternion oldPrefabRotation = GameObject.Find(uniqueID).transform.rotation;

        while (elapsedTime < time)
        {
            GameObject.Find(uniqueID).transform.position = Vector3.Lerp(oldPrefabPosition, newPrefabPosition, (elapsedTime / time));
            if (newPrefabRotation != Quaternion.identity)
            {
                GameObject.Find(uniqueID).transform.rotation = Quaternion.Lerp(oldPrefabRotation, newPrefabRotation, (elapsedTime / time));
            }
            elapsedTime += Time.deltaTime;
            yield return time;
        }
    }

    /// <summary>
    /// Wrapper for TranslatePrefab. Call it to translate any gameobject to any position in time t.
    /// </summary>
    /// <param name="uniqueID">Name of the prefab</param>
    /// <param name="newPrefabPosition">Vector3 position for the prefab</param>
    /// <param name="time">The time to move the gameobject.</param>
    public void MoveAndRotatePrefabTo(string uniqueID, Vector3 newPrefabPosition, Quaternion newPrefabRotation, float time)
    {
        StartCoroutine(TranslateAndRotatePrefab(uniqueID, newPrefabPosition, newPrefabRotation, time));
    }

    /// <summary>
    /// Exits the game.
    /// </summary>
    public void ExitGame()
    {
    	Application.Quit();
    }
}
