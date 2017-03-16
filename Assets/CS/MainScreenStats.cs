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

    private void Awake()
    {
        timeLeft = 99;
        totalScore = 0;
        timeLeftDelayMax = 99;
        maxNumberOfObjects = 25;
        startGameFlag = false;
        endGameFlag = false;
        movementSpeed = 15.5f;
        jumpSpeed = 600.0f;
        rotateSpeed = 2.0f;
        height = Camera.main.orthographicSize * 2.0f;
        width = height * Camera.main.aspect;
    }
}
