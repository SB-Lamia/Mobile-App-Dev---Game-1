using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void NewGameButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadGameButton()
    {

    }

    public void SettingsButton()
    {

    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
