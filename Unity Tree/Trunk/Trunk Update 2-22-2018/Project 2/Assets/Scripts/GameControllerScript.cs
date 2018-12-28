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

    private List<GameObject> pieces = new List<GameObject>();

    public Material endState;

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

    public void addNumOfPieces(GameObject toBeAdded)
    {
        numOfPieces++;
        Debug.Log("There are: " + numOfPieces);
        Debug.Log(toBeAdded.name + " has been added");
        pieces.Add(toBeAdded);
    }

    public void addToRainbowList(GameObject toBeAdded)
    {
        pieces.Add(toBeAdded);
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

    public void removeNumOfCorrect()
    {
        numOfCorrectPieces--;
        Debug.Log("There are: " + numOfCorrectPieces + " Correct peices");
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
        //GameObject.Find("Win State").GetComponent<SpriteRenderer>().enabled = true;

        setPeiceEndstate();

        //Commented out to test rainbow winstate
        //gameManager.GetComponent<LevelManagerScript>().goToNextLevel();
    }

    //Will set all gameobjects in the list to their apporiate endstate
    void setPeiceEndstate()
    {
        foreach (GameObject tmp in pieces)
        {
            if(tmp != null)
            {
                Debug.Log(tmp.name + " is now a rainbow");
                tmp.GetComponent<Renderer>().material = endState; //Applies the rainbow effect

                if(tmp.tag != "AnchorSpot" && tmp.tag != "RectangleBit") //Stops pieces without the script from crashing loop
                    tmp.GetComponent<PieceScript>().isEnabled = false; //Sets the pieces to disabled

            }

        }
    }
}
