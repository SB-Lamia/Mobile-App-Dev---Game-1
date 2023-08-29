using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public GameObject selectedItemImage;
    public GameObject selectedItemText;
    public GameObject selectedButton1;
    public GameObject selectedButton2;
    public GameObject mainEquipment;
    public GameObject secondaryEquipment;
    public GameObject wearableEquipment;
    public bool mainEquipped;
    public bool secondaryEquipped;

    public Item currentlySelectedItem;

    public List<GameObject> selectedItemMenu = new List<GameObject>();

    public void Awake()
    {
        selectedItemMenu.Add(selectedButton1);
        selectedItemMenu.Add(selectedButton2);
        selectedItemMenu.Add(selectedItemImage);
        selectedItemMenu.Add(selectedItemText);
        foreach(GameObject gameObject in selectedItemMenu)
        {
            gameObject.SetActive(false);
        }
    }

    public void ItemPressed(int slot)
    {
        Debug.Log(slot);
        foreach (GameObject gameObject in selectedItemMenu)
        {
            gameObject.SetActive(true);
        }
        selectedItemImage.GetComponentInChildren<Image>().sprite = GameManager.instance.items[slot].itemSprite;
        selectedItemText.GetComponentInChildren<TextMeshProUGUI>().text = GameManager.instance.items[slot].itemDesc;
        currentlySelectedItem = GameManager.instance.items[slot];
        switch (currentlySelectedItem.itemType)
        {
            case Item.ItemType.Weapon:
            case Item.ItemType.Helmet:
            case Item.ItemType.ChestPlate:
            case Item.ItemType.Leggings:
            case Item.ItemType.Feet:
                if (currentlySelectedItem.isEquipped)
                {
                    selectedButton1.transform.GetComponentInChildren<TextMeshProUGUI>().text = "Unequip Item";
                }
                else
                {
                    selectedButton1.transform.GetComponentInChildren<TextMeshProUGUI>().text = "Equip Item";
                }
                break;
            case Item.ItemType.Consumable:
                selectedButton1.transform.GetComponentInChildren<TextMeshProUGUI>().text = "Consume Item";
                break;
        }
        selectedButton2.transform.GetComponentInChildren<TextMeshProUGUI>().text = "Drop Item";
    }

    public void Button1_Pressed()
    {
        switch (currentlySelectedItem.itemType)
        {
            case Item.ItemType.Weapon:
                if (currentlySelectedItem.isEquipped)
                {
                    currentlySelectedItem.isEquipped = false;
                    if (mainEquipped)
                    {
                        mainEquipment.transform.GetChild(0).gameObject.SetActive(false);
                        mainEquipped = false;
                    }
                    else if (!mainEquipment && secondaryEquipment)
                    {
                        mainEquipment.transform.GetChild(0).gameObject.SetActive(false);
                        secondaryEquipped = false;
                    }
                    else
                    {
                        Debug.Log("Max Equiped Items already");
                    }
                }
                else
                {
                    currentlySelectedItem.isEquipped = true;
                    if (!mainEquipped)
                    {
                        mainEquipment.transform.GetChild(0).gameObject.SetActive(true);
                        mainEquipment.transform.GetChild(0).GetComponent<Image>().sprite = currentlySelectedItem.itemSprite;
                        mainEquipped = true;
                    }
                    else if (mainEquipment && !secondaryEquipment)
                    {
                        mainEquipment.transform.GetChild(0).gameObject.SetActive(true);
                        secondaryEquipment.transform.GetChild(0).GetComponent<Image>().sprite = currentlySelectedItem.itemSprite;
                        secondaryEquipped = true;
                    }
                    else
                    {
                        Debug.Log("Max Equiped Items already");
                    }
                }
                break;
            case Item.ItemType.Helmet:
            case Item.ItemType.ChestPlate:
            case Item.ItemType.Leggings:
            case Item.ItemType.Feet:
                break;
            case Item.ItemType.Consumable:
                GameManager.instance.RemoveItem(currentlySelectedItem);
                break;
            case Item.ItemType.Material:
                selectedButton1.SetActive(false);
                break;
        }
    }

    public void Button2_Pressed()
    {
        if (currentlySelectedItem.isEquipped)
        {
            currentlySelectedItem.isEquipped = false;
            if (mainEquipped)
            {
                mainEquipment.transform.GetChild(0).gameObject.SetActive(false);
                mainEquipped = false;
            }
            else if (!mainEquipment && secondaryEquipment)
            {
                mainEquipment.transform.GetChild(0).gameObject.SetActive(false);
                secondaryEquipped = false;
            }
            else
            {
                Debug.Log("Max Equiped Items already");
            }
        }
        GameManager.instance.RemoveItem(currentlySelectedItem);
    }
}
