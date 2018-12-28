using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagerScript : MonoBehaviour
{
    public bool autoStartLevels = false;

    public string levelPrefix;
    public int amountOfLevels;
    private int currentLevelIndex = 0;

    public void goToNextLevel()
    {
        if (autoStartLevels)
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
    }

    public void goToEndScene()
    {
        SceneManager.LoadScene("EndScene");
    }
}
