using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MainScreenLogic : MonoBehaviour {

	// Private and Public Variables

	private int timeLeftDelayCounter = 0;
    private float interpolant = 0.0f;
    private bool spellMovementFlag = false;
    private bool direction;
    private Vector3 spellPosition;
    private Vector3 finalPosition;

    // Event Functions

    private void Update () {

		DecreaseTime();

		try
		{
			CheckBoundaries();

		}
		catch (Exception)
		{
			// Maybe put something here.
		}

        if (spellMovementFlag)
        {
            SpellMovement();
        }


	}

	// Private and Public Functions

	/// <summary>
	/// Decreases the time. The global time left variable is decreased with a delay
	///	given in <see cref="MainScreenStats"/>. If the timer reaches zero then
	/// the startGameFlag is false and the end game false is true;
	/// </summary>
	public void DecreaseTime()
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
	/// Loads the pre fab with a name given by prefabName.
	/// </summary>
	/// <param name="prefabName">Prefab name.</param>
	public void LoadPreFab(string prefabName)
    {
        GameObject prefabObject = GameObject.Instantiate ( (GameObject)Resources.Load (prefabName) );
        prefabObject.name = prefabName;
    }

    /// <summary>
    /// Cast the spell when the space bar is hit.
    /// </summary>
    public void SpellCast()
    {
        LoadPreFab("Spell"); // Load in the prefab for the spell sprite.

        GameObject player = GameObject.Find("Player");
        GameObject spell = GameObject.Find("Spell");

        direction = player.GetComponent<SpriteRenderer>().flipX;

        Vector3 playerPosition = player.transform.position;
        
        spell.transform.position = player.transform.position; // Set position to the player position.
        spellPosition = spell.transform.position; // private variable

        float spellDistance = GetComponent< MainScreenStats >().spellDistance;
        if (direction)
        {
            finalPosition = new Vector3(-spellDistance, playerPosition.y, playerPosition.z);
        }
        else
        {
			finalPosition = new Vector3(spellDistance, playerPosition.y, playerPosition.z);
        }
        spellMovementFlag = true;
    }

    /// <summary>
    /// This moves the spell sprite when the space bar is pressed.
    /// FIX THIS
    /// </summary>
    public void SpellMovement()
    {
		GameObject.Find("Spell").GetComponent< Transform >().position =  Vector3.Lerp(spellPosition, finalPosition, interpolant);        
		float spellSpeed = GetComponent < MainScreenStats >().spellSpeed;
        interpolant += Time.deltaTime / spellSpeed;
        if (interpolant >= 1.0f)
        {
            interpolant = 0.0f;
            Destroy(GameObject.Find("Spell"));
            spellMovementFlag = false;
        }
    }

    /// <summary>
    /// Checks the boundaries for the game so the player does not fly out into infinity.
    /// </summary>
    public void CheckBoundaries()
    {
		Vector3 playerPosition = GameObject.Find("Player").transform.position;
		Vector3 floorPosition = GameObject.Find("Floor").transform.position;
		int worldHeight = GetComponent< MainScreenStats >().worldHeight;

		if (playerPosition.y <= -worldHeight)
		{
			GetComponent< MainScreenStats >().startGameFlag = false;
			GetComponent< MainScreenStats >().endGameFlag = true;
		}

		if( floorPosition.y <= -worldHeight)
		{
			int width = GetComponent< MainScreenStats >().worldWidth;
    		int height = GetComponent< MainScreenStats >().worldHeight;
			int x = UnityEngine.Random.Range(-width,width);
			int y = UnityEngine.Random.Range(-height,height);
			GameObject.Find("Floor").transform.position = new Vector3(x, y, 0);
		}
    }

    /// <summary>
    /// Generates the platform at a random position.
    /// </summary>
    public void GenerateRandomPlatform(string prefabName)
    {
    	LoadPreFab(prefabName);
		int width = GetComponent< MainScreenStats >().worldWidth;
    	int height = GetComponent< MainScreenStats >().worldHeight;
		int x = UnityEngine.Random.Range(-width,width);
		int y = UnityEngine.Random.Range(-height,height);
		GameObject.Find(prefabName).transform.position = new Vector3(x, y, 0);
		print(GameObject.Find(prefabName).transform.position + "real");
    }

    /// <summary>
    /// Exits the game.
    /// </summary>
    public void ExitGame()
    {
    	Application.Quit();
    }
}
