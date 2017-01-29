using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreenStats : MonoBehaviour {

	// Private and Public Variables
	public int timeLeft = 99;
	public int totalScore = 0;
	public int timeLeftDelayMax = 99;
	public bool startGameFlag = false;
	public bool endGameFlag = false;
	public float movementSpeed = 20.0f;
	public float jumpSpeed = 75.0f;
    public float spellDistance = 25.0f;
	public float spellSpeed = 25.0f;
	public int worldHeight = 5;
	public int worldWidth = 5;
}
