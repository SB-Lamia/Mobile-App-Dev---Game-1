using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TradingSystemManager : MonoBehaviour
{
    public static TradingSystemManager Instance;
    public GameObject selectedItemImage;
    public GameObject selectedItemText;
    public GameObject selectedItemBuffInfo;
    public GameObject selectedButton1;
    public GameObject selectedButton2;
    public GameObject tradingHudMenu;
    public Item currentlySelectedItem;

    public List<GameObject> selectedItemMenu = new List<GameObject>();

    public GameObject playerParentSlot;
    public GameObject traderParentSlot;

    public Trader currentTrader;

    public void Awake()
    {
        selectedItemMenu.Add(selectedItemImage);
        selectedItemMenu.Add(selectedItemText);
        foreach (GameObject gameObject in selectedItemMenu)
        {
            gameObject.SetActive(false);
        }
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void OnTraderOpen()
    {
        SpawnInventory(playerParentSlot, GameManager.instance.items, GameManager.instance.itemNumbers);
        SpawnInventory(traderParentSlot, currentTrader.itemsForTrader, currentTrader.itemCount);

    }

    public void SpawnInventory(GameObject parentSlots, List<Item> items, List<int> itemCount)
    {
        InventorySlot[] inventorySlots = parentSlots.GetComponentsInChildren<InventorySlot>();
        for (int i = 0; i < items.Count; i++)
        {
            inventorySlots[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            inventorySlots[i].transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            inventorySlots[i].AddItem(items[i]);
            inventorySlots[i].transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = itemCount[i].ToString();
        }
    }


    public void ItemPressed(int slot)
    {
        Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        string FullBuffInfo = "";
        foreach (GameObject gameObject in selectedItemMenu)
        {
            gameObject.SetActive(true);
        }
        
        if (EventSystem.current.currentSelectedGameObject.transform.parent.parent.name == "TraderItems")
        {
            currentlySelectedItem = currentTrader.itemsForTrader[slot];
        }
        else if (EventSystem.current.currentSelectedGameObject.transform.parent.parent.name == "PlayerItems")
        {
            currentlySelectedItem = GameManager.instance.items[slot];
        }
        
        selectedItemImage.GetComponentInChildren<Image>().sprite = GameManager.instance.items[slot].itemSprite;
        selectedItemText.GetComponentInChildren<TextMeshProUGUI>().text = GameManager.instance.items[slot].itemDesc;
        foreach (string buffInfo in GameManager.instance.items[slot].ConsumableDescription)
        {
            FullBuffInfo += buffInfo + "\n";
        }
        selectedItemBuffInfo.GetComponentInChildren<TextMeshProUGUI>().text = FullBuffInfo;
        
    }

    public void Button1_Pressed()
    {
        GameManager.instance.ClearSlots();
    }

    public void Button2_Pressed()
    {
        GameManager.instance.RemoveItem(currentlySelectedItem);
        GameManager.instance.ClearSlots();
    }

    public void Resume()
    {
        tradingHudMenu.gameObject.SetActive(false);
        GameManager.instance.ToggleDefaultHud(true);
        Time.timeScale = 1.0f;
        GameManager.instance.isPaused = false;
    }

    public void Pause()
    {
        tradingHudMenu.gameObject.SetActive(true);
        GameManager.instance.ToggleDefaultHud(false);
        Time.timeScale = 0.0f;
        GameManager.instance.isPaused = true;
        OnTraderOpen();
    }
}
