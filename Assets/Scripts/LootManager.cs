using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LootManager : MonoBehaviour
{
    public List<Item> allItems;
    public const int MinCommon = 5;
    public const int MinUncommon = 3;
    public const int MinRare = 1;
    public const int MinEpic = 1;
    public const int MinLegendary = 1;

    public const int MaxCommon = 10;
    public const int MaxUncommon = 5;
    public const int MaxRare = 3;
    public const int MaxEpic = 2;
    public const int MaxLegendary = 1;

    public List<Item> commons = new List<Item>();
    public List<Item> uncommons = new List<Item>();
    public List<Item> rares = new List<Item>();
    public List<Item> epics = new List<Item>();
    public List<Item> legendarys = new List<Item>();

    public static LootManager instance;

    public GameObject LootUIElement;
    public GameObject LootVisualPrefab;
    public List<Item> recentlyAddedItems = new List<Item>();


    void Awake()
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

        foreach (Item item in allItems)
        {
            switch (item.rarity)
            {
                case "Common":
                    commons.Add(item);
                    break;
                case "Uncommon":
                    uncommons.Add(item);
                    break;
                case "Rare":
                    rares.Add(item);
                    break;
                case "Epic":
                    epics.Add(item);
                    break;
                case "Legendary":
                    legendarys.Add(item);
                    break;
                default:
                    Debug.Log("Error: invalid rarity please check item: " + item.name);
                    break;
            }
        }
    }

    public void PickRarity(string rarity)
    {
        recentlyAddedItems = new List<Item>();
        switch (rarity)
        {
            case "Common":
                GiveItem(commons, MinCommon, MaxCommon);
                break;
            case "Uncommon":
                GiveItem(uncommons, MinUncommon, MaxUncommon);
                break;
            case "Rare":
                GiveItem(rares, MinRare, MaxRare);
                break;
            case "Epic":
                GiveItem(epics, MinEpic, MaxEpic);
                break;
            case "Legendary":
                GiveItem(legendarys, MinLegendary, MaxLegendary);
                break;
            default:
                Debug.Log("Error: invalid rarity please check rarity inputed: " + rarity);
                break;
        }
    }

    private void GiveItem(List<Item> rarityItemList, int minAmmount, int maxAmmount)
    {
        Item randomNewItem;
        int givingItemAmmount = Random.Range(minAmmount, maxAmmount + 1);
        for(int i = 0; i < givingItemAmmount; i++)
        {
            randomNewItem = rarityItemList[Random.Range(0, rarityItemList.Count)];
            GameManager.instance.AddItem(randomNewItem);
            recentlyAddedItems.Add(randomNewItem);
        }

        ShowUserLoot();
    }

    private void ShowUserLoot()
    {
        bool checkIfDuplicate;
        int getVisualItemCount = 0;
        LootUIElement.SetActive(true);
        for (int i = 0; i < recentlyAddedItems.Count; i++)
        {
            checkIfDuplicate = true;
            int children = LootUIElement.transform.GetChild(0).transform.childCount;
            Debug.Log(children);
            for (int k = 0; k < children; k++)
            {
                if(LootUIElement.transform.GetChild(0).GetChild(k).GetComponentInChildren<Image>().sprite.name == recentlyAddedItems[i].itemSprite.name)
                {
                    int.TryParse(LootUIElement.transform.GetChild(0).GetChild(k).GetComponentInChildren<TextMeshProUGUI>().text, out getVisualItemCount);
                    getVisualItemCount++;
                    LootUIElement.transform.GetChild(0).GetChild(k).GetComponentInChildren<TextMeshProUGUI>().text = getVisualItemCount.ToString();
                    checkIfDuplicate = false;

                    Debug.Log("Item Adding Number: " + i + "Item Increased: " + recentlyAddedItems[i].itemName + ". To Value: " + getVisualItemCount++);
                }
            }
            if (checkIfDuplicate)
            {
                Debug.Log("Item Adding Number: " + i + " Item Added: " + recentlyAddedItems[i].itemName);
                GameObject lootVisualGameObject = Instantiate(LootVisualPrefab);
                
                lootVisualGameObject.GetComponentInChildren<Image>().sprite = recentlyAddedItems[i].itemSprite;
                lootVisualGameObject.GetComponentInChildren<TextMeshProUGUI>().text = "1";
                lootVisualGameObject.transform.SetParent(LootUIElement.transform.GetChild(0).transform);
                lootVisualGameObject.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    public void CloseUserLootVisual()
    {
        LootUIElement.SetActive(false);
        int children = LootUIElement.transform.GetChild(0).transform.childCount;
        for (int i = 0; i < children; i++)
        {
            Destroy(LootUIElement.transform.GetChild(0).transform.GetChild(i).gameObject);
        }
    }
}
