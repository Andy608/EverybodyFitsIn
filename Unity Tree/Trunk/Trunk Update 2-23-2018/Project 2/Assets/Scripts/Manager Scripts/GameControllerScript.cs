﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    private static GameObject gameManager;
    private static GameObject gameManagerPrefab;

    public static GameControllerScript getInstance()
    {
        if (gameManager == null)
        {
            gameManagerPrefab = Resources.Load<GameObject>("Prefabs/Game Manager") as GameObject;
            gameManager = Instantiate(gameManagerPrefab);
        }

        return gameManager.GetComponent<GameControllerScript>();
    }

	// Use this for initialization
	void Awake ()
    {
        Debug.Log("GAME CONTROLLER START");

        if (gameManager == null)
        {
            gameManager = gameObject;

            DontDestroyOnLoad(gameManager);
            DontDestroyOnLoad(gameManager.GetComponent<GameControllerScript>());
            DontDestroyOnLoad(gameManager.GetComponent<LevelManagerScript>());
            DontDestroyOnLoad(gameManager.GetComponent<LevelScript>());

            gameManager.GetComponent<LevelManagerScript>().goToNextLevel();
        }
    }
}