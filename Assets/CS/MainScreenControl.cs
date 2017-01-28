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
	private void Update ()
	{
		
	}
	private void FixedUpdate ()
	{
		try
		{
			GetUserKeyBoardInput();
		}
		catch (Exception)
		{
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

        if (Input.GetKeyDown (KeyCode.UpArrow))
		{
			player.GetComponent< Rigidbody2D >().AddForce(player.transform.up * speed);
		}
		else if (Input.GetKeyDown (KeyCode.LeftArrow))
		{
			player.GetComponent< SpriteRenderer >().flipX = true;
			player.GetComponent< Rigidbody2D >().AddForce(-player.transform.right * speed);
		}
		else if (Input.GetKeyDown (KeyCode.RightArrow))
		{
			player.GetComponent< SpriteRenderer >().flipX = false;
			player.GetComponent< Rigidbody2D >().AddForce(player.transform.right * speed);
		}
		else if (Input.GetKeyDown (KeyCode.Space))
		{
            if (!GameObject.Find("Spell"))
            {
                GetComponent< MainScreenLogic >().SpellCast();
            }
        }

	}
}
