﻿using UnityEngine;
using UnityEngine.SceneManagement;

// This is the permanent source of information for our game modes
public class Game : MonoBehaviour 
{
    //Used to keep track of number players selected in menu
    public int numPlayers = 4;
    //Used to change map 1 is mageCity and 2 is dummy
    public int mapSelected = 1;
    //Use to see which game mode was selected in other scripts
    public int gameModeSelected = 0;
    //Use to change players' starting time (default at 90 sec)
    public int playersStartingTime = 90;
    //Use to change music volume (default at full)
    public int musicVolume = 100;
	public int soundEffectsVolume = 100;
    //Used to change players' colors
    public Color p1Color = new Color(144f / 256f, 208f / 256f, 1, 1);
    public Color p2Color = Color.red;
    public Color p3Color = Color.green;
    public Color p4Color = Color.yellow;

    public GameController GameController
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
        if (Instance != this) 
        {
            Debug.LogError("Breach of singleton pattern, " +
                           "we have two game controller objects!");
        }
		DontDestroyOnLoad(transform.gameObject);
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

    void Update()
    {
        if (GameController == null || !GameController.isStarted) {
            return;
        }

        GameController.OnUpdate();
    }


	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{        
        if (scene.name == "Main")
        {
            this.GameController.OnStart();
        }		
	}

}
