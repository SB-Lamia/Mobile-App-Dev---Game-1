using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LootManager : MonoBehaviour
{
    public List<Item> allItems;

    public List<Item> commons = new List<Item>();
    public List<Item> uncommons = new List<Item>();
    public List<Item> rares = new List<Item>();
    public List<Item> epics = new List<Item>();
    public List<Item> legendarys = new List<Item>();

    public static LootManager instance;

    public GameObject cityUILootElement;
    public GameObject lootVisualPrefab;
    public List<Item> recentlyAddedItems = new List<Item>();

    private int rowCount;
    public Sprite XPIcon;

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

    public void LuckCalculationRarity(int baseNumberOfNewLoot)
    {
        recentlyAddedItems = new List<Item>();

        int currentLuck = PlayerStatManager.instance.Luck;

        int maxLootCount = baseNumberOfNewLoot + Mathf.FloorToInt(currentLuck / 20);

        for (int i = 0; i < maxLootCount; i++)
        {

            int luckPercentage = Random.Range(0, currentLuck);

            switch (luckPercentage)
            {
                case <= 20:
                    GiveItem(commons);
                    break;
                case >= 21 and <= 40:
                    GiveItem(uncommons);
                    break;
                case >= 41 and <= 60:
                    GiveItem(rares);
                    break;
                case >= 61 and <= 80:
                    GiveItem(epics);
                    break;
                case >= 81 and <= 100:
                    GiveItem(legendarys);
                    break;
                default:
                    Debug.Log("Error: invalid rarity please check luck percentage inputed: " + luckPercentage);
                    break;
            }
        }

        ShowUserLoot(cityUILootElement, recentlyAddedItems);
    }

    private void GiveItem(List<Item> rarityItemList)
    {
        Item randomNewItem;
        randomNewItem = rarityItemList[Random.Range(0, rarityItemList.Count)];
        GameManager.instance.AddItem(randomNewItem);
        recentlyAddedItems.Add(randomNewItem);
    }

    public void ShowUserLoot(GameObject LootUIElement, List<Item> ListedItems)
    {
        rowCount = 0;
        bool checkIfDuplicate;
        int getVisualItemCount = 0;
        LootUIElement.SetActive(true);
        GameObject lootVisualGameObject;

        for (int i = 0; i < ListedItems.Count; i++)
        {
            checkIfDuplicate = true;
            int children = LootUIElement.transform.GetChild(1).transform.childCount;
            for (int k = 0; k < children; k++)
            {
                if(LootUIElement.transform.GetChild(1).GetChild(k).GetComponentInChildren<Image>().sprite.name == ListedItems[i].itemSprite.name)
                {
                    int.TryParse(LootUIElement.transform.GetChild(1).GetChild(k).GetComponentInChildren<TextMeshProUGUI>().text, out getVisualItemCount);
                    getVisualItemCount++;
                    LootUIElement.transform.GetChild(1).GetChild(k).GetComponentInChildren<TextMeshProUGUI>().text = getVisualItemCount.ToString();
                    checkIfDuplicate = false;
                }
            }
            if (checkIfDuplicate)
            {
                lootVisualGameObject = Instantiate(lootVisualPrefab);
                
                lootVisualGameObject.GetComponentInChildren<Image>().sprite = ListedItems[i].itemSprite;
                lootVisualGameObject.GetComponentInChildren<TextMeshProUGUI>().text = BattleManager.instance.XPGainAfterCombat.ToString();
                lootVisualGameObject.transform.SetParent(LootUIElement.transform.GetChild(1).transform);
                lootVisualGameObject.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        lootVisualGameObject = Instantiate(lootVisualPrefab);

        lootVisualGameObject.GetComponentInChildren<Image>().sprite = XPIcon;
        lootVisualGameObject.GetComponentInChildren<TextMeshProUGUI>().text = "1";
        lootVisualGameObject.transform.SetParent(LootUIElement.transform.GetChild(1).transform);
        lootVisualGameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    public void CloseUserLootVisual(GameObject LootUIElement)
    {
        LootUIElement.SetActive(false);
        int children = LootUIElement.transform.GetChild(1).transform.childCount;
        for (int i = 0; i < children; i++)
        {
            Destroy(LootUIElement.transform.GetChild(1).transform.GetChild(i).gameObject);
        }
    }
}
