using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isPaused;

    public List<GameObject> disabledHud;

    public GameObject parentSlots;
    public InventorySlot[] inventorySlots;
    //public Dictionary<Item, int> itemDict = new Dictionary<Item, int>();

    public List<Item> items = new List<Item>();
    public List<int> itemNumbers = new List<int>();

    public int money = 50;

    public TextMeshProUGUI shotgunAmmoText;
    public TextMeshProUGUI pistolAmmoText;
    public TextMeshProUGUI rifleAmmoText;

    public TextMeshProUGUI moneyVisual;

    public int shotgunAmmo;
    public int pistolAmmo;
    public int rifleAmmo;

    // Slot 1: stat affect 0-6
    // Slot 2: buff/debuff given
    // Slot 3: time left
    List<List<int>> tempStatusEffects = new List<List<int>>();

    public GameObject CurrentLocation;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void DisplayItems()
    {
        inventorySlots = parentSlots.GetComponentsInChildren<InventorySlot>();
        for (int i = 0; i < items.Count; i++)
        {
            inventorySlots[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            inventorySlots[i].transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            inventorySlots[i].AddItem(items[i]);
            inventorySlots[i].transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = itemNumbers[i].ToString();
        }

        shotgunAmmoText.text = "x " + shotgunAmmo.ToString();
        pistolAmmoText.text = "x " + pistolAmmo.ToString();
        rifleAmmoText.text = "x " + rifleAmmo.ToString();
        moneyVisual.text = "x " + money.ToString();

    }

    // Slot 1: stat affect 0-6
    // Slot 2: buff/debuff given
    // Slot 3: time left
    public void CheckTempStatBuffs()
    {
        for (int i = 0; i < tempStatusEffects.Count; i++)
        {
            if (tempStatusEffects[i][2] == 0)
            {
                //Remove Status Effect
                switch (tempStatusEffects[i][0])
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                }
            }
            else
            {
                tempStatusEffects[i][2]--;
            }
        }
    }

    // Slot 1: stat affect 0-6
    // Slot 2: buff/debuff given
    // Slot 3: time left
    public void AddBuff(int statAffected, int statValueChange, int duration)
    {
        List<int> storeStatChange = new List<int>(3);

        storeStatChange[0] = statAffected;
        storeStatChange[1] = statValueChange;
        storeStatChange[2] = duration;

        tempStatusEffects.Add(storeStatChange);

        //Remove Status Effect
        switch (statAffected)
        {
            case 0:
                PlayerStatManager.instance.Endurance += statValueChange;
                break;
            case 1:
                PlayerStatManager.instance.Perception += statValueChange;
                break;
            case 2:
                PlayerStatManager.instance.Charisma += statValueChange;
                break;
            case 3:
                PlayerStatManager.instance.Luck += statValueChange;
                break;
            case 4:
                PlayerStatManager.instance.Intelligence += statValueChange;
                break;
            case 5:
                PlayerStatManager.instance.Agility += statValueChange;
                break;
        }
    }

    public void ClearSlots()
    {
        inventorySlots = parentSlots.GetComponentsInChildren<InventorySlot>();
        for (int i = 0; i < inventorySlots.Count(); i++)
        {
            inventorySlots[i].ClearSlot();
        }
        DisplayItems();
    }

    public void AddItem(Item newItem)
    {
        if (!items.Contains(newItem))
        {
            items.Add(newItem);
            itemNumbers.Add(1);
        }
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (newItem == items[i])
                {
                    if (items[i].isStackable == true)
                    {
                        itemNumbers[i]++;
                    }
                    else
                    {
                        items.Add(newItem);
                        itemNumbers.Add(1);
                    }
                }
            }
        }
    }

    public void RemoveItem(Item oldItem)
    {
        if (items.Contains(oldItem))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (oldItem == items[i])
                {
                    itemNumbers[i]--;
                    if (itemNumbers[i] == 0)
                    {
                        items.Remove(oldItem);
                        itemNumbers.Remove(itemNumbers[i]);
                    }
                }
            }
        }
    }

    public void ToggleDefaultHud(bool toggleState)
    {
        foreach (GameObject hud in disabledHud)
        {
            hud.SetActive(toggleState);
        }
    }
}
