using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    

    public void SaveGame()
    {
        //To Be Implemented
    }

    public void LoadGame()
    {
        //To Be Implemented
    }

    public void Credits()
    {
        //To Be Implemented
    }

    public void LoadMainMenu()
    {
        //To Be Implemented
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

}
