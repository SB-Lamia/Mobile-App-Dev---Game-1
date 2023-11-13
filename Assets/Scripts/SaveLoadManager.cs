using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager instance;
    public SaveState state;
    public GameObject testSaveState;

    public List<CityStorageInformation> cityList;
    public List<TraderStorageInformation> traderList;
    public List<Item> items;

    public bool ResetInUnityEditor;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        //if (CheckIfNewGame.newGame == true)
        //{
        //    state = new SaveState();
        //    SetupGameForSave();
        //    Save();
        //    Debug.Log("New Game Created");
        //}
        //else
        //{
        //    Load();
        //}

        state = new SaveState();
        SetupGameForSave();
        Save();
        Debug.Log("New Game Created");
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
        cityList = new List<CityStorageInformation>();

        //Trader Locations
        traderList = new List<TraderStorageInformation>();

        //Saving Inventory System
        items = GameManager.instance.items;
        state.itemNumbers = GameManager.instance.itemNumbers;

        foreach (GameObject city in CitySpawnerManager.instance.cityGenerated)
        {
            cityList.Add(new CityStorageInformation()
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
            traderList.Add(new TraderStorageInformation()
            {
                xPosition = trader.transform.position.x,
                yPosition = trader.transform.position.y,
                traderItems = itemIDs,
                traderItemNumbers = trader.GetComponent<Trader>().itemCount
            });
        }

        state.ConvertToJson(items, cityList, traderList);

        Debug.Log("Saved");
        //XmlSerializer serializer = new XmlSerializer(typeof(SaveState));
        //using (StringWriter sw = new StringWriter())
        //{
        //    serializer.Serialize(sw, state);
        //    Debug.Log(sw.ToString());
        //    PlayerPrefs.SetString("player save", sw.ToString());
        //}
        string json = JsonUtility.ToJson(state);
        PlayerPrefs.SetString("player save", json);
    }

    public void Load()
    {
        
        if (PlayerPrefs.HasKey("player save"))
        {
            string json = PlayerPrefs.GetString("player save", null);
            if (string.IsNullOrEmpty(json) == true) 
            {
                Debug.Log("No Save File");
            }
            else
            {
                SaveState state = new SaveState();
                state = JsonUtility.FromJson<SaveState>(json);
                Debug.Log(state);
            }

            cityList = JsonUtility.FromJson<List<CityStorageInformation>>(state.cityJson);
            traderList = JsonUtility.FromJson<List<TraderStorageInformation>>(state.traderJson);
            items = JsonUtility.FromJson<List<Item>>(state.itemJson);
            Debug.Log(state);
            Debug.Log(cityList);
            //CitySpawnerManager.instance.ReplaceCityLoad(cityList, traderList);
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
            
            GameManager.instance.items = items;
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
