using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitySpawnerManager : MonoBehaviour
{
    public GameObject City;
    public GameObject parentCity;

    void Awake()
    {
        PlaceBoxes(2000, 20000);
    }

    void PlaceBoxes(int numberOfBoxes, int maxTries)
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
            }
            count++;
        }
    }
}
