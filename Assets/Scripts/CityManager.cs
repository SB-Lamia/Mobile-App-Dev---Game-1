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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {

            }
        }

        openCityButton.interactable = false;
    }

    public void SetupVisualCity()
    {
        for (int i = 0; i < locationMaxCount; i++)
        {
            locations.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = locationImages[currentCity.GetComponent<City>().cityEvents[i]];
            if (currentCity.GetComponent<City>().eventCount > i)
            {
                crossedOutLocation[i].SetActive(true);
            }
        }
    }

    public void CloseVisualCity()
    {
        foreach(GameObject cross in crossedOutLocation)
        {
            cross.SetActive(false);
        }
    }

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

    public void TriggerCityEvent()
    {
        currentCity.GetComponent<City>().TriggerNextExplore();
    }
}
