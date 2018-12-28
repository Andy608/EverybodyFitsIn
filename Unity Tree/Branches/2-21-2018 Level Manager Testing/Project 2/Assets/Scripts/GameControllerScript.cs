using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    int numOfPieces = 0;
    int numOfCorrectPieces = 0;
    //Get win state from the scene
    //Keep level tracker

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
	void Start ()
    {
        Debug.Log("GAME CONTROLLER START");

        if (gameManager == null)
        {
            gameManager = gameObject;
        }

        numOfPieces = 0;
        numOfCorrectPieces = 0;

        DontDestroyOnLoad(gameManager);
        DontDestroyOnLoad(gameManager.GetComponent<GameControllerScript>());
        DontDestroyOnLoad(gameManager.GetComponent<LevelManagerScript>());
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void addNumOfPieces()
    {
        numOfPieces++;
        Debug.Log("There are: " + numOfPieces);
    }

    public void addNumOfCorrectPieces()
    {
        numOfCorrectPieces++;
        Debug.Log("There are: " + numOfCorrectPieces + " Correct peices");

        if (checkWin())
        {
            preformWin();
        }
    }

    bool checkWin()
    {
        if (numOfPieces == numOfCorrectPieces)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void preformWin()
    {
        Debug.Log("GAME DONE");

        //Should be a coroutine for like a second and then switch levels.
        GameObject.Find("Win State").GetComponent<SpriteRenderer>().enabled = true;

        gameManager.GetComponent<LevelManagerScript>().goToNextLevel();
    }
}
