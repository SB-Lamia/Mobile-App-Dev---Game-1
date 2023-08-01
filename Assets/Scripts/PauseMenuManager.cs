using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    

    public void SaveGame()
    {
        SaveLoadManager.instance.Save();
    }

    public void LoadGame()
    {
        SaveLoadManager.instance.Load();
    }

    public void Credits()
    {
        //To Be Implemented
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

}
