using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManagerScript : MonoBehaviour
{
    private Button nextLevelBtn;

    public List<string> levelList = new List<string>();

    //public string levelPrefix;
    //private int amountOfLevels;
    private int currentLevelIndex = -1;

    public void disableButton()
    {
        nextLevelBtn.enabled = false;
        nextLevelBtn.image.enabled = false;
    }

    public void enableButton()
    {
        nextLevelBtn.enabled = true;
        nextLevelBtn.image.enabled = true;
    }

    public void setNextLevelButton(Button button)
    {
        nextLevelBtn = button;
        nextLevelBtn = nextLevelBtn.GetComponent<Button>();
        nextLevelBtn.onClick.AddListener(goToNextLevel);
    }

    public void goToNextLevel()
    {
        if (currentLevelIndex < levelList.Count - 1)
        {
            currentLevelIndex++;
            //Debug.Log("GOING TO NEXT LEVEL: " + currentLevelIndex);

            //To Next Level
            SceneManager.LoadScene(levelList[currentLevelIndex]);
        }
    }

    public void reset()
    {
        currentLevelIndex = 0;
    }
}
