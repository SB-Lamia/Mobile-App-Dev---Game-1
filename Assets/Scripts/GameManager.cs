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

    public TextMeshProUGUI shotgunAmmoText;
    public TextMeshProUGUI pistolAmmoText;
    public TextMeshProUGUI rifleAmmoText;

    public int shotgunAmmo;
    public int pistolAmmo;
    public int rifleAmmo;

    private void Awake()
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
                if(newItem == items[i])
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
