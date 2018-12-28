using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUIScript : MonoBehaviour
{
    public void PlayOnClicked()
    {
        GameControllerScript.getInstance().GetComponent<LevelManagerScript>().goToNextLevel();
    }

    public void HowToPlayOnClicked()
    {
        SceneManager.LoadScene("Scenes/Game Scenes/Menu Scenes/HowToScene");
    }

    public void ExitOnClicked()
    {
        Application.Quit();
    }

    public void ToTitleOnClicked()
    {
        GameControllerScript.getInstance().GetComponent<LevelManagerScript>().reset();
        SceneManager.LoadScene("Scenes/Game Scenes/Menu Scenes/TitleScene");
    }
}
