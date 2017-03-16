using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

/// <summary>
/// Main screen control. Movement is forced related so fixed update must be used.
/// </summary>
public class MainScreenControl : MonoBehaviour {
	// Private and Public Variables

	// Event Functions
	private void Update()
	{
		StartScreenController();
	}
	private void FixedUpdate ()
	{
		try
		{
			GetUserKeyBoardInput();
		}
		catch (Exception) // At times the player character will not be on screen.
		{
			// Maybe put something here.
		}
	}

	// Private and Public Functions

	/// <summary>
	/// Gets the user key board input. The player can move left and right
	/// with the arrow keys and jump with the space bar. The forces are just constants
    /// multiplied by normal vectors to the sprite. When moving left or right a small amount
    /// of torque is applied.
	/// </summary>
	private void GetUserKeyBoardInput ()
	{
		GameObject player = GameObject.Find("Player"); // Get the player game object.

		Vector3 position = player.transform.position; // Current player position.
		Rigidbody2D body = player.GetComponent< Rigidbody2D >(); // Player's rigid body.
		SpriteRenderer sprite = player.GetComponent< SpriteRenderer >(); // The players sprite.

		float speed = GetComponent< MainScreenStats >().movementSpeed; // Player's movement speed.
		float height = GetComponent< MainScreenStats >().jumpSpeed; // Player's jump speed.
        float rotate = GetComponent<MainScreenStats>().rotateSpeed;// Player's rotate speed.

        if (Input.GetKey (KeyCode.LeftArrow))
		{
			sprite.flipX = true; // Make sprite face to the left.
            body.AddTorque(rotate);
			body.AddForce(-player.transform.right * speed); // Applies a force in the -x direction.
		}
		else if (Input.GetKey (KeyCode.RightArrow))
		{
			sprite.flipX = false; // Make sprite face to the right.
            body.AddTorque(-rotate);
            body.AddForce(player.transform.right * speed); // Applies a force in the x direction.
		}
		else if (Input.GetKeyDown (KeyCode.Space))
		{
			body.AddForce(player.transform.up * height); // Applies a force in the y direction.
		}
	}

	/// <summary>
	/// Starts the screen controller. This allows for a better flow when starting a new game.
	/// </summary>
	private void StartScreenController()
	{
		bool startFlag = GetComponent< MainScreenStats >().startGameFlag;
		bool endFlag = GetComponent< MainScreenStats >().endGameFlag;

		if (!startFlag && !endFlag) // Start Screen only.
		{
			if (Input.GetKeyDown (KeyCode.Space))
			{
				GetComponent< MainScreenGUI >().StartButton();
			}
		}
	}
}
