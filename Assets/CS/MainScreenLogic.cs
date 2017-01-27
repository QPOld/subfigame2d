using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if (spellMovementFlag)
        {
            SpellMovement();
            interpolant += Time.deltaTime;
            if (interpolant >= 1.0f)
            {
                interpolant = 0.0f;
                Destroy(GameObject.Find("Spell"));
                spellMovementFlag = false;
            }
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
        spellPosition = spell.transform.position;
        spell.transform.position = player.transform.position; // Set position to the player position.

        if (direction)
        {
            finalPosition = spellPosition + new Vector3(-Screen.width,0,0);
        }
        else
        {
            finalPosition = spellPosition + new Vector3(Screen.width, 0, 0);
        }
        spellMovementFlag = true;
    }

    /// <summary>
    /// This moves the spell sprite when the space bar is pressed.
    /// FIX THIS
    /// </summary>
    public void SpellMovement()
    {
        Vector3.Lerp(spellPosition, finalPosition, interpolant);        
    }
}
