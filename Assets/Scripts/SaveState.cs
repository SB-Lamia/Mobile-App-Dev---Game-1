
using System.Collections.Generic;
using UnityEngine;

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
    public List<CityStorageInformation> cityList;
    public List<Item> items;
    public List<int> itemNumbers;
}
