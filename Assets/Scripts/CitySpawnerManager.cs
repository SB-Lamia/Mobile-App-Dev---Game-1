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
        // I"m assuming the following variables are globally defined somewhere
        // float minXPos, maxXPos, minYPos, maxYPos
        // float sideLength -- this is the length of the cube you need to find out

        List<Vector2> placedBoxes = new List<Vector2>(); // list of all our boxes we successfully placed
        int count = 0;  // how many times have we tried to place a box

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

    //public void CityLocationSetup()
    //{
    //    for (int currentCityCount = 0; currentCityCount < cityCount; currentCityCount += radiusGrowthCount)
    //    {
    //        for (int radiusCount = 0; radiusCount < radiusGrowthCount; radiusCount++)
    //        {
    //            do
    //            {
    //                randomX = GenerateRandomCord();
    //                regenCord = false;
    //                foreach (Vector2 tempVector in citySpawnLocations)
    //                {
    //                    if ((randomX >= tempVector.x + cityRadiusRange) &&
    //                        (randomX <= tempVector.x - cityRadiusRange))
    //                    {
    //                        regenCord = true;
    //                        Debug.Log("Regen X");
    //                    }
    //                }
    //            } while (regenCord == true);
    //            do
    //            {
    //                randomY = GenerateRandomCord();
    //                regenCord = false;
    //                foreach (Vector2 tempVector in citySpawnLocations)
    //                {
    //                    if ((randomY >= tempVector.y + cityRadiusRange) &&
    //                        (randomY <= tempVector.y - cityRadiusRange))
    //                    {
    //                        regenCord = true;
    //                        Debug.Log("Regen Y");
    //                    }
    //                }
    //            } while (regenCord == true);


    //            Vector2 cityLocationHolder = new Vector2(randomX, randomY);
    //            citySpawnLocations[currentCityCount + radiusCount] = cityLocationHolder;
    //            //Debug.Log(currentCityCount + radiusCount);
    //            //Debug.Log("randomX: " + randomX + " randomY: " + randomY);
    //        }
    //        currentRadius += radiusIncrease;
    //    }
    //    CitySpawner();
    //}

    //public float GenerateRandomCord()
    //{
    //    float minRange = currentRadius - cityRadiusRange;
    //    float maxRange = currentRadius + cityRadiusRange;
    //    //Positive cord
    //    if (Random.Range(0, 2) == 0)
    //    {
    //        return Random.Range(minRange, maxRange);
    //    }
    //    //Negative cord
    //    else
    //    {
    //        return Random.Range(minRange, maxRange) * -1;
    //    }
    ////}
    //public void CitySpawner()
    //{
    //    foreach(Vector2 location in citySpawnLocations)
    //    {
    //        GameObject newCity = Instantiate(City, location, Quaternion.identity);
    //        newCity.transform.parent = parentCity.transform;
    //    }
    //}
}
