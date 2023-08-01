using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CitySpawnerManager : MonoBehaviour
{
    public GameObject City;
    public GameObject parentCity;
    public List<GameObject> cityGenerated;
    public static CitySpawnerManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        cityGenerated = new List<GameObject>();
    }

    public void NewCityLoad(int numberOfBoxes, int maxTries)
    {
        float minXPos = -500;
        float maxXPos = 500;
        float minYPos = -500;
        float maxYPos = 500;

        float sideLength = 20;
       
        List<Vector2> placedBoxes = new List<Vector2>();
        int count = 0;

        while (count < maxTries && placedBoxes.Count < numberOfBoxes)
        {
            float xPos = Random.Range(minXPos, maxXPos);
            float yPos = Random.Range(minYPos, maxYPos);

            bool isGood = true;

            for (int i = 0; i < placedBoxes.Count && isGood; i++)
            {
                if (placedBoxes[i].x < xPos + sideLength && placedBoxes[i].x + sideLength > xPos &&
                    placedBoxes[i].y < yPos + sideLength && placedBoxes[i].y + sideLength > yPos)
                {
                    isGood = false;
                    Debug.Log("failed");
                }
            }
            if (isGood)
            {
                placedBoxes.Add(new Vector2(xPos, yPos));
                GameObject newCity = Instantiate(City, new Vector2(xPos, yPos), Quaternion.identity);
                newCity.transform.parent = parentCity.transform;
                newCity.GetComponent<City>().GenerateRandomCity();
                cityGenerated.Add(newCity);
            }
            count++;
        }
    }

    public void ReplaceCityLoad(List<CityStorageInformation> CityData)
    {
        foreach(CityStorageInformation cityData in CityData)
        {
            GameObject newCity = Instantiate(City, new Vector2(cityData.xPosition, cityData.yPosition), Quaternion.identity);
            newCity.transform.parent = parentCity.transform;
            newCity.GetComponent<City>().cityEvents = cityData.cityEvents;
            newCity.GetComponent<City>().eventCount = cityData.cityEventCount;
            cityGenerated.Add(newCity);
        }
    }
}
