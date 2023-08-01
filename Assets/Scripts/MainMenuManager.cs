using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void NewGameButton()
    {
        CheckIfNewGame.newGame = true;
        SceneManager.LoadScene("GameScene");
    }

    public void LoadGameButton()
    {
        CheckIfNewGame.newGame = false;
        SceneManager.LoadScene("GameScene");
    }

    public void SettingsButton()
    {

    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
