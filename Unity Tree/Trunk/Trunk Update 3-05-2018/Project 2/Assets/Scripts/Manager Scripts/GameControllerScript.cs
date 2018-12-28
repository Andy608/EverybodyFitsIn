using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    private static GameObject gameManager;
    private static GameObject gameManagerPrefab;

    private Canvas universalCanvasPrefab;
    private Canvas universalCanvas;

    private GameObject eventMangerPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            gameManager.GetComponent<LevelManagerScript>().goToNextLevel();
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        Instantiate(eventMangerPrefab);
        universalCanvas = Instantiate(universalCanvasPrefab);
        //Find the next turn button and pass it to the level manager script
        gameManager.GetComponent<LevelManagerScript>().setNextLevelButton(universalCanvas.transform.Find("NextTurnButton").GetComponent<Button>());
        gameManager.GetComponent<LevelManagerScript>().disableButton();
    }

    public static void compleateLevel()
    {
        gameManager.GetComponent<LevelManagerScript>().enableButton();
    }

    public static GameControllerScript getInstance()
    {
        if (gameManager == null)
        {
            gameManagerPrefab = Resources.Load<GameObject>("Prefabs/Game Manager") as GameObject;
            gameManager = Instantiate(gameManagerPrefab);
            ArtSelectionScript.populateSpritesFromResourceFolder();
        }

        return gameManager.GetComponent<GameControllerScript>();
    }

	// Use this for initialization
	void Awake ()
    {
        universalCanvasPrefab = Resources.Load<Canvas>("Prefabs/Global Canvas") as Canvas;
        eventMangerPrefab = Resources.Load<GameObject>("Prefabs/EventSystem") as GameObject;
        //Debug.Log("GAME CONTROLLER START");

        if (gameManager == null)
        {
            gameManager = gameObject;

            DontDestroyOnLoad(gameManager);
            DontDestroyOnLoad(gameManager.GetComponent<GameControllerScript>());
            DontDestroyOnLoad(gameManager.GetComponent<LevelManagerScript>());
            DontDestroyOnLoad(gameManager.GetComponent<LevelScript>());
            DontDestroyOnLoad(eventMangerPrefab);

            gameManager.GetComponent<LevelManagerScript>().goToNextLevel();
        }
    }
}
