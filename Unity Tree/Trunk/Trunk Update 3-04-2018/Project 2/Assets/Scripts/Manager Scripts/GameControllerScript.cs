using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    private static GameObject gameManager;
    private static GameObject gameManagerPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            gameManager.GetComponent<LevelManagerScript>().goToNextLevel();
        }
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
