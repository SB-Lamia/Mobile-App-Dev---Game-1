using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CitySpawnerManager : MonoBehaviour
{
    public GameObject city;
    public GameObject trader;
    public GameObject parentCity;
    public List<GameObject> cityGenerated;
    public List<GameObject> traderGenerated;
    public static CitySpawnerManager instance;
    public Sprite cityIcon;
    public Sprite traderIcon;

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
        GameObject newLocation = null;
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
                switch (Random.Range(0,5))
                {
                    //City Location
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        newLocation = Instantiate(city, new Vector2(xPos, yPos), Quaternion.identity);
                        newLocation.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = cityIcon;
                        newLocation.GetComponent<City>().GenerateRandomCity();
                        cityGenerated.Add(newLocation);
                        break;
                    //Trader Location
                    case 4:
                        newLocation = Instantiate(trader, new Vector2(xPos, yPos), Quaternion.identity);
                        newLocation.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = traderIcon;
                        newLocation.GetComponent<Trader>().GenerateItemsForTrade();
                        traderGenerated.Add(newLocation);
                        break;
                }

                newLocation.transform.parent = parentCity.transform;
            }
            count++;
        }
    }

    public void ReplaceCityLoad(List<CityStorageInformation> CityData, List<TraderStorageInformation> TraderData)
    {
        foreach(CityStorageInformation cityData in CityData)
        {
            GameObject newCity = Instantiate(city, new Vector2(cityData.xPosition, cityData.yPosition), Quaternion.identity);
            newCity.transform.parent = parentCity.transform;
            newCity.GetComponent<City>().cityEvents = cityData.cityEvents;
            newCity.GetComponent<City>().eventCount = cityData.cityEventCount;
            cityGenerated.Add(newCity);
        }

        foreach (TraderStorageInformation traderdata in TraderData)
        {
            GameObject newTrader = Instantiate(trader, new Vector2(traderdata.xPosition, traderdata.yPosition), Quaternion.identity);
            newTrader.transform.parent = parentCity.transform;
            newTrader.GetComponent<Trader>().itemCount = traderdata.traderItemNumbers;
            for (int i = 0; i < traderdata.traderItems.Count; i++)
            {
                for (int k = 0; k < LootManager.instance.allItems.Count; k++)
                {
                    if (LootManager.instance.allItems[k].ID == traderdata.traderItems[i])
                    {
                        newTrader.GetComponent<Trader>().itemsForTrader.Add(LootManager.instance.allItems[k]);
                    }
                }
            }
        }
    }
}
