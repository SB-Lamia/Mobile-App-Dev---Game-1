
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveState
{
    public int Level;
    public int Experience;
    public int Endurance;
    public int Agility;
    public int Charisma;
    public int Perception;
    public int Intelligence;
    public int Luck;
    public int currentXPNeeded;
    public int PointsToAssign;
    public Vector2 playerPosition;
    public float currentHealth;
    public float currentHunger;
    public float currentWater;
    public List<int> itemNumbers;
    public string cityJson;
    public string traderJson;
    public string itemJson;


    public void ConvertToJson(List<Item> items, List<CityStorageInformation> cityList, List<TraderStorageInformation> traderList)
    {
        cityJson = JsonUtility.ToJson(cityList);
        traderJson = JsonUtility.ToJson(traderJson);
        itemJson = JsonUtility.ToJson(items);
    }
}
