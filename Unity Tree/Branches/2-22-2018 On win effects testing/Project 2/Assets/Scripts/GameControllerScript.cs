using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    int numOfPieces = 0;
    int numOfCorrectPieces = 0;
    private GameObject winstate;
    //Get win state from the scene
    //Keep level tracker

    private static GameObject gameManager;
    private static GameObject gameManagerPrefab;

    //private List<GameObject> deactivatedAOE = new List<GameObject>();

    public static GameControllerScript getInstance()
    {
        if (gameManager == null)
        {
            gameManagerPrefab = Resources.Load<GameObject>("Prefabs/Game Manager") as GameObject;
            gameManager = Instantiate(gameManagerPrefab);
            gameManager.AddComponent<GameControllerScript>();
            DontDestroyOnLoad(gameManager);
        }

        return gameManager.GetComponent<GameControllerScript>();
    }

	// Use this for initialization
	void Start ()
    {
        winstate = GameObject.Find("Win State");
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void addNumOfPeices()
    {
        numOfPieces++;
        Debug.Log("There are: " + numOfPieces);
    }

    public void removeNumOfCorrect()
    {
        numOfCorrectPieces--;
        Debug.Log("There are: " + numOfCorrectPieces + " Correct peices");
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
        winstate.GetComponent<SpriteRenderer>().enabled = true;
    }
}
