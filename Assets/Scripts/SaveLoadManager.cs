using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager instance;
    public SaveState state;

    public bool ResetInUnityEditor;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (CheckIfNewGame.newGame == true)
        {
            state = new SaveState();
            SetupGameForSave();
            Save();
            Debug.Log("New Game Created");
        }
        else
        {
            Load();
        }
    }

    public void Save()
    {
        state = new SaveState();
       
        //Level and Experience
        state.Level = PlayerStatManager.instance.Level;
        state.Experience = PlayerStatManager.instance.Experience;
        state.currentXPNeeded = PlayerStatManager.instance.currentXPNeeded;
        state.PointsToAssign = PlayerStatManager.instance.PointsToAssign;


        //Player Stats
        state.Luck = PlayerStatManager.instance.Luck;
        state.Perception = PlayerStatManager.instance.Perception;
        state.Agility = PlayerStatManager.instance.Agility;
        state.Intelligence = PlayerStatManager.instance.Intelligence;
        state.Endurance = PlayerStatManager.instance.Endurance;
        state.Charisma = PlayerStatManager.instance.Charisma;

        //Player Food, Water and Health
        state.currentHealth = PlayerStatManager.instance.currentHealth;
        state.currentHunger = PlayerStatManager.instance.currentHunger;
        state.currentWater = PlayerStatManager.instance.currentWater;

        //Player Position
        state.playerPosition = GameObject.Find("Player").transform.position;

        //City Locations
        state.cityList = new List<CityStorageInformation>();

        //Trader Locations
        state.traderList = new List<TraderStorageInformation>();

        //Saving Inventory System
        state.items = GameManager.instance.items;
        state.itemNumbers = GameManager.instance.itemNumbers;

        foreach (GameObject city in CitySpawnerManager.instance.cityGenerated)
        {
            state.cityList.Add(new CityStorageInformation()
            {
                xPosition = city.transform.position.x,
                yPosition = city.transform.position.y,
                cityEventCount = city.GetComponent<City>().eventCount,
                cityEvents = city.GetComponent<City>().cityEvents
            });
        }
        foreach (GameObject trader in CitySpawnerManager.instance.traderGenerated)
        {
            List<int> itemIDs = new List<int>();
            foreach (Item item in trader.GetComponent<Trader>().itemsForTrader)
            {
                itemIDs.Add(item.ID);
            }
            state.traderList.Add(new TraderStorageInformation()
            {
                xPosition = trader.transform.position.x,
                yPosition = trader.transform.position.y,
                traderItems = itemIDs,
                traderItemNumbers = trader.GetComponent<Trader>().itemCount
            });
        }
        Debug.Log("Saved");
        PlayerPrefs.SetString("save", JsonUtility.ToJson(state));
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("save"))
        {
            state = JsonUtility.FromJson<SaveState>(PlayerPrefs.GetString("save"));

            CitySpawnerManager.instance.ReplaceCityLoad(state.cityList, state.traderList);
            PlayerStatManager.instance.SetupStarterStats(
                state.Level,
                state.Experience,
                state.PointsToAssign,
                state.currentXPNeeded,
                state.Endurance,
                state.Agility,
                state.Charisma,
                state.Perception,
                state.Luck,
                state.Intelligence);

            PlayerStatManager.instance.SetupBaseStats(
                state.currentHunger,
                state.currentWater,
                state.currentHealth
                );
            GameObject.Find("Player").transform.position = new Vector2(state.playerPosition.x, state.playerPosition.y);
            
            GameManager.instance.items = state.items;
            GameManager.instance.itemNumbers = state.itemNumbers;
        }
    }

    public void SetupGameForSave()
    {
        PlayerStatManager.instance.SetupStarterStats(1, 5, 10, 20, 5, 5, 5, 5, 5, 5);
        CitySpawnerManager.instance.NewCityLoad(2000, 20000);
        PlayerStatManager.instance.SetupBaseStats(100.0f, 100.0f, 100.0f);
        StatUpgraderUIManager.instance.OpenStatUpgradeMenu();
    }
}
