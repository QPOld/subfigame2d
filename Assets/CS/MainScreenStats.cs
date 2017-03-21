using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreenStats : MonoBehaviour {

	// Private and Public Variables
	public int timeLeft;
	public int totalScore;
	public int timeLeftDelayMax;
    public int maxNumberOfObjects;
    public bool startGameFlag;
	public bool endGameFlag;
	public float movementSpeed;
	public float jumpSpeed;
    public float rotateSpeed;
    public float height;
    public float width;

    // This allows for the inspector to auto update upon starting the game.
    // To be honest the fact that it doesn't is annoying.
    private void Awake()
    {
        totalScore = 0; // Total score for the player.
        timeLeft = 99; // Current time left.
        timeLeftDelayMax = 99; // The delay counter for the game timer. This gets smaller as the game progresses.
        maxNumberOfObjects = 25; // Max number of objects that will be loaded at once.

        // Off and On Flags.
        startGameFlag = false;
        endGameFlag = false;

        // Fiddle with these to make it smooth.
        movementSpeed = 150.5f;
        jumpSpeed = 100.0f;
        rotateSpeed = 2.0f;

        // In 2D, the height and width must be calculated from the camera view.
        height = Camera.main.orthographicSize * 2.0f;
        width = height * Camera.main.aspect;
    }
}
