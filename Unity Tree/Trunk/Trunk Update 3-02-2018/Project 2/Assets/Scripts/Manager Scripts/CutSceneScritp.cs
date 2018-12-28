using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneScritp : MonoBehaviour {

    public List<GameObject> scenes = new List<GameObject>();
    //public string nextSceneName = "Level_1";
    private int currentScene = 0;
    private Vector3 scenePos = new Vector3(0, 0, 0);
    private GameObject sceneGameobject;
    private static GameObject gameManager;
    private string gameManagerName = "Game Manager";

    // Use this for initialization
    void Start () {
        displayScene(); //Displays the start scene
        Debug.Log("There are " + scenes.Count + " in the current cutscene");
        gameManager = GameObject.Find(gameManagerName);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            nextScene();
        }
	}
    
    void nextScene()
    {
        currentScene++;

        if (currentScene < scenes.Count)
            displayScene();
        else
        {
            Debug.Log("FINISHED");
            gameManager.GetComponent<LevelManagerScript>().goToNextLevel();
        }
    }

    void displayScene()
    {
        Debug.Log("Displaying scene: " + currentScene);
        Destroy(sceneGameobject);
        sceneGameobject = Instantiate(scenes[currentScene], scenePos, Quaternion.identity);
    }



}
