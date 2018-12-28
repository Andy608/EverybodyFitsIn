using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagerScript : MonoBehaviour
{
    public bool autoStartLevels = true;

    public List<string> levelList = new List<string>();

    //public string levelPrefix;
    //private int amountOfLevels;
    private int currentLevelIndex = 0;


    public void goToNextLevel()
    {
        if (autoStartLevels && currentLevelIndex <= levelList.Count)
        {
            currentLevelIndex++;
            Debug.Log("GOING TO NEXT LEVEL: " + currentLevelIndex);

            if (currentLevelIndex == levelList.Count)
            {
                //To End scene
                goToEndScene();
            }
            else
            {
                //To Next Level
                SceneManager.LoadScene(levelList[currentLevelIndex]);
            }
        }
    }

    private void goToEndScene()
    {
        SceneManager.LoadScene("EndScene");
    }
}
