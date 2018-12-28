using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagerScript : MonoBehaviour
{
    public string levelPrefix;
    public int amountOfLevels;
    private int currentLevelIndex = 0;

    public void Start()
    {
        Debug.Log("LEVEL MANAGER START");

        if (amountOfLevels <= 0)
        {
            Debug.Log("Invalid amount of Levels!");
            goToEndScene();
        }
        else
        {
            //Go to Level 1.
            goToNextLevel();
        }
    }

    public void goToNextLevel()
    {
        currentLevelIndex++;
        Debug.Log("GOING TO NEXT LEVEL: " + currentLevelIndex);

        if (amountOfLevels < currentLevelIndex)
        {
            //To End scene
            goToEndScene();
        }
        else
        {
            Debug.Log("Ye hee");
            //To Next Level
            SceneManager.LoadScene(levelPrefix + "_" + currentLevelIndex);
        }
    }

    public void goToEndScene()
    {
        SceneManager.LoadScene("EndScene");
    }
}
