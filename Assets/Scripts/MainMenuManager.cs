using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject areYouSure;
    public GameObject noSavedGame;

    public void NewGameButton()
    {
        if (PlayerPrefs.HasKey("save"))
        {
            areYouSure.SetActive(true);
        }
    }

    public void CreateNewGame()
    {
        CheckIfNewGame.newGame = true;
        SceneManager.LoadScene("GameScene");
    }

    public void ClosePopupAreYouSure()
    {
        areYouSure.SetActive(false);
    }

    public void CloseNoSavedGame()
    {
        noSavedGame.SetActive(false);
    }

    public void LoadGameButton()
    {
        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey("save"))
        {
            areYouSure.SetActive(true);
        }
        else {
            CheckIfNewGame.newGame = false;
            SceneManager.LoadScene("GameScene");
        }
    }

    public void SettingsButton()
    {

    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
