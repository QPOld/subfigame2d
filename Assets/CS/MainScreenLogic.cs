using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreenLogic : MonoBehaviour {

	// Private and Public Variables

	private int timeLeftDelayCounter = 0;

	// Event Functions

	private void Update () {
		DecreaseTime();
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
}
