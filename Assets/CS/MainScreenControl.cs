using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Main screen control.
/// Create an extensive boundary function.
/// </summary>
public class MainScreenControl : MonoBehaviour {
	// Private and Public Variables

	// Event Functions
	private void Update () {
		try {
			GetUserKeyBoardInput();

		} catch (Exception) {
			// Maybe put something here.
		}
	}

	// Private and Public Functions

	/// <summary>
	/// Gets the user key board input.
	/// </summary>
	private void GetUserKeyBoardInput ()
	{
		GameObject player = GameObject.Find("Player");
		Vector3 position = player.transform.position;
		float speed = GetComponent< MainScreenStats >().movementSpeed;
		if (Input.GetKeyDown (KeyCode.DownArrow))
		{
			GameObject.Find("Player").transform.position = new Vector3 (position.x, position.y - speed, position.z);
		}
		else if (Input.GetKeyDown (KeyCode.UpArrow))
		{
			GameObject.Find("Player").transform.position = new Vector3 (position.x, position.y + speed, position.z);
		}
		else if (Input.GetKeyDown (KeyCode.LeftArrow))
		{
			player.GetComponent< SpriteRenderer >().flipX = true;
			GameObject.Find("Player").transform.position = new Vector3 (position.x - speed, position.y, position.z);
		}
		else if (Input.GetKeyDown (KeyCode.RightArrow))
		{
			GameObject.Find("Player").GetComponent< SpriteRenderer >().flipX = false;
			GameObject.Find("Player").transform.position = new Vector3 (position.x + speed, position.y, position.z);
		}
		else if (Input.GetKeyDown (KeyCode.Space))
		{
			print("Space Bar Attack");
		}

	}
}
