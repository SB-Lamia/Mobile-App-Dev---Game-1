using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityManager : MonoBehaviour
{
    public Sprite[] locationImages;
    public GameObject currentCity;
    public GameObject locations;
    public const int locationMaxCount = 5;
    public Button openCityButton;
    public GameObject cityHudMenu;
    public static CityManager instance;
    public List<GameObject> crossedOutLocation;
    public Sprite unknownLocation;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        openCityButton.interactable = false;
    }

    //Sets up the visuals once a city is entered.
    public void SetupVisualCity()
    {
        for (int i = 0; i < locationMaxCount; i++)
        {
            //Checks if a player can see future locations.
            if (PlayerStatManager.instance.Perception > i * 20)
            {
                locations.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = locationImages[currentCity.GetComponent<City>().cityEvents[i]];
            }
            //Otherwise hides them as an unknown location
            else
            {
                locations.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = unknownLocation;
            }
            //checks if the locations have already been explored.
            if (currentCity.GetComponent<City>().eventCount > i)
            {
                locations.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = locationImages[currentCity.GetComponent<City>().cityEvents[i]];
                crossedOutLocation[i].SetActive(true);
            }
        }
    }

    //Closes all crossed out locations to setup next city.
    public void CloseVisualCity()
    {
        foreach(GameObject cross in crossedOutLocation)
        {
            cross.SetActive(false);
        }
    }

    //Used to open and close the city.
    public void Resume()
    {
        cityHudMenu.gameObject.SetActive(false);
        GameManager.instance.ToggleDefaultHud(true);
        Time.timeScale = 1.0f;
        GameManager.instance.isPaused = false;
        CloseVisualCity();
    }
    public void Pause()
    {
        cityHudMenu.gameObject.SetActive(true);
        GameManager.instance.ToggleDefaultHud(false);
        Time.timeScale = 0.0f;
        GameManager.instance.isPaused = true;
        SetupVisualCity();
    }

    //Triggers the city event when exploring.
    public void TriggerCityEvent()
    {
        currentCity.GetComponent<City>().TriggerNextExplore();
    }
}
