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
        PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey("player save"))
        {
            areYouSure.SetActive(true);
        }
        else
        {
            CreateNewGame();
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
        Debug.Log(PlayerPrefs.HasKey("player save"));
        if (PlayerPrefs.HasKey("player save"))
        {
            CheckIfNewGame.newGame = false;
            SceneManager.LoadScene("GameScene");
        }
        else 
        {
            areYouSure.SetActive(true);
            CheckIfNewGame.newGame = true;
            SceneManager.LoadScene("GameScene");
        }
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
