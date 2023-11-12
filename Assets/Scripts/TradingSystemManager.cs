using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TradingSystemManager : MonoBehaviour
{
    public static TradingSystemManager Instance;
    public GameObject selectedItemTitle;
    public GameObject selectedItemImage;
    public GameObject selectedItemText;
    public GameObject selectedItemBuffInfo;
    public GameObject selectedButton1;
    public GameObject selectedButton2;
    public GameObject tradingHudMenu;
    public Item currentlySelectedItem;

    public TextMeshProUGUI moneyVisual;

    //If false trader is buying from player
    //If true trader is selling to player
    public bool traderSelling = false;

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
        gameObject.SetActive(false);
    }

    public void OnTraderOpen()
    {
        moneyVisual.text = GameManager.instance.money.ToString();
        SpawnInventory(playerParentSlot, GameManager.instance.items, GameManager.instance.itemNumbers);
        SpawnInventory(traderParentSlot, currentTrader.itemsForTrader, currentTrader.itemCount);

    }

    public void SpawnInventory(GameObject parentSlots, List<Item> items, List<int> itemCount)
    {
        InventorySlot[] inventorySlots = parentSlots.GetComponentsInChildren<InventorySlot>();
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].ClearSlot();
        }
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < items.Count)
            {
                inventorySlots[i].transform.GetChild(0).GetComponent<Button>().interactable = true;
                inventorySlots[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                inventorySlots[i].transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                inventorySlots[i].AddItem(items[i]);
                inventorySlots[i].transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = itemCount[i].ToString();
            }
            else
            {
                inventorySlots[i].transform.GetChild(0).GetComponent<Button>().interactable = false;
            }
        }
        buttonBuySell.GetComponentInChildren<TextMeshProUGUI>().text = "Pick Trader/Player";

    }


    public void ItemPressed(int slot)
    {
        Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        string FullBuffInfo = "";
        foreach (GameObject gameObject in selectedItemMenu)
        {
            gameObject.SetActive(true);
        }
        
        if (EventSystem.current.currentSelectedGameObject.transform.parent.parent.parent.name == "TraderItems")
        {
            currentlySelectedItem = currentTrader.itemsForTrader[slot];
        }
        else if (EventSystem.current.currentSelectedGameObject.transform.parent.parent.parent.name == "PlayerItems")
        {
            currentlySelectedItem = GameManager.instance.items[slot];
        }
        selectedItemTitle.GetComponent<TextMeshProUGUI>().text = currentlySelectedItem.name;
        selectedItemImage.GetComponentInChildren<Image>().sprite = currentlySelectedItem.itemSprite;
        selectedItemText.GetComponentInChildren<TextMeshProUGUI>().text = currentlySelectedItem.itemDesc;
        for(int i = 0; i < currentlySelectedItem.ConsumableDescription.Length; i++)
        {
            if (i < currentlySelectedItem.ConsumableDescription.Length-1)
            {
                FullBuffInfo += currentlySelectedItem.ConsumableDescription[i] + "\n";
            }
            else
            {
                if (traderSelling)
                {
                    FullBuffInfo += "Value: " + (Mathf.RoundToInt((float)currentlySelectedItem.value * (float)1.25)).ToString() + "$";
                    buttonBuySell.GetComponentInChildren<TextMeshProUGUI>().text = "Buy";
                }
                else
                {
                    FullBuffInfo += "Value: " + (Mathf.RoundToInt((float)currentlySelectedItem.value * (float)(1 + PlayerStatManager.instance.Charisma / 100))).ToString() + "$";
                    buttonBuySell.GetComponentInChildren<TextMeshProUGUI>().text = "Sell";
                }
            }
        }
        FullBuffInfo += "\n";
        selectedItemBuffInfo.GetComponentInChildren<TextMeshProUGUI>().text = FullBuffInfo;
    }

    public void SwapActiveTraderPlayer(bool newState)
    {
        traderSelling = newState;
        buttonBuySell.GetComponentInChildren<TextMeshProUGUI>().text = "Pick Item";
    }

    public GameObject buttonBuySell;

    public void Button1_Pressed()
    {
        if (traderSelling)
        {
            if (GameManager.instance.money >= currentlySelectedItem.value)
            {
                GameManager.instance.money -= currentlySelectedItem.value;
                GameManager.instance.AddItem(currentlySelectedItem);
                currentTrader.RemoveItem(currentlySelectedItem);
            }
        }
        else
        {
            GameManager.instance.money += currentlySelectedItem.value;
            GameManager.instance.RemoveItem(currentlySelectedItem);
            currentTrader.AddItem(currentlySelectedItem);
        }
        OnTraderOpen();
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
