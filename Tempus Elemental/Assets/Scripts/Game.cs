﻿using UnityEngine;

// This is the permanent source of information for our game modes
public class Game : MonoBehaviour 
{
    //Used to keep track of number players selected in menu
    public int numPlayers = 4;

    public GameController gameController
    {
        get;
        set;
    }

    // Singleton access point for class
    // Note that the implementation is different from traditional singleton
    // pattern, instead of limiting the access to constructor, we are 
    // checking and setting the instance in the Awake function
    public static Game Instance 
    {
        get;
        private set;
    }

	
	void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
        }
        if (Instance != this) {
            Debug.LogError("Breach of singleton pattern, " +
                           "we have two game controller objects!");
        }
		DontDestroyOnLoad(transform.gameObject);
	}

}