using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public GameObject selectedItemImage;
    public GameObject selectedItemText;
    public GameObject selectedItemBuffInfo;
    public GameObject selectedButton1;
    public GameObject selectedButton2;
    public GameObject mainEquipment;
    public GameObject secondaryEquipment;
    public GameObject wearableEquipment;
    public bool mainEquipped;
    public bool secondaryEquipped;

    public GameObject inventoryHud;

    public Item mainEquipedItem;
    public Item secondaryEquipedItem;

    public Item currentlySelectedItem;

    public List<GameObject> selectedItemMenu = new List<GameObject>();

    public void Awake()
    {
        mainEquipedItem = null;
        secondaryEquipedItem = null;
        selectedItemMenu.Add(selectedButton1);
        selectedItemMenu.Add(selectedButton2);
        selectedItemMenu.Add(selectedItemImage);
        selectedItemMenu.Add(selectedItemText);
        foreach(GameObject gameObject in selectedItemMenu)
        {
            gameObject.SetActive(false);
        }
        if (Instance == null)
        {
            Instance = this;
        }
        inventoryHud.SetActive(false);
    }


    public void ItemPressed(int slot)
    {
        foreach (GameObject gameObject in selectedItemMenu)
        {
            gameObject.SetActive(true);
        }
        selectedItemImage.GetComponentInChildren<Image>().sprite = GameManager.instance.items[slot].itemSprite;
        selectedItemText.GetComponentInChildren<TextMeshProUGUI>().text = GameManager.instance.items[slot].itemDesc;
        string FullBuffInfo = "";
        foreach (string buffInfo in GameManager.instance.items[slot].ConsumableDescription)
        {
            FullBuffInfo += buffInfo + "\n";
        }
        FullBuffInfo += "\n";
        selectedItemBuffInfo.GetComponentInChildren<TextMeshProUGUI>().text = FullBuffInfo;
        currentlySelectedItem = GameManager.instance.items[slot];
        switch (currentlySelectedItem.itemType)
        {
            case Item.ItemType.Equippable:
                //To Be Added Later

                if (currentlySelectedItem.isEquippedMain || currentlySelectedItem.isEquippedSecondary)
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
            case Item.ItemType.Equippable:
                if (currentlySelectedItem.isEquippedMain || currentlySelectedItem.isEquippedSecondary)
                {
                    if (mainEquipped && currentlySelectedItem.isEquippedMain)
                    {
                        mainEquipment.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                        mainEquipped = false;
                        currentlySelectedItem.isEquippedMain = false;
                        selectedButton1.transform.GetComponentInChildren<TextMeshProUGUI>().text = "Equip Item";
                        mainEquipedItem = null;
                    }
                    else if (secondaryEquipped && currentlySelectedItem.isEquippedSecondary)
                    {
                        secondaryEquipment.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                        secondaryEquipped = false;
                        currentlySelectedItem.isEquippedSecondary = false;
                        selectedButton1.transform.GetComponentInChildren<TextMeshProUGUI>().text = "Equip Item";
                        
                    }
                    else
                    {
                        Debug.Log("Cannot Unequip Item!");
                    }
                }
                else
                {
                    if (!mainEquipped)
                    {
                        mainEquipment.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                        mainEquipment.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = currentlySelectedItem.itemSprite;
                        mainEquipped = true;
                        currentlySelectedItem.isEquippedMain = true;
                        selectedButton1.transform.GetComponentInChildren<TextMeshProUGUI>().text = "Unequip Item";
                        
                        mainEquipedItem = currentlySelectedItem;
                    }
                    else if (mainEquipped && !secondaryEquipped)
                    {
                        secondaryEquipment.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                        secondaryEquipment.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = currentlySelectedItem.itemSprite;
                        secondaryEquipped = true;
                        currentlySelectedItem.isEquippedSecondary = true;
                        selectedButton1.transform.GetComponentInChildren<TextMeshProUGUI>().text = "Unequip Item";
                        
                        secondaryEquipedItem = currentlySelectedItem;
                    }
                    else
                    {
                        Debug.Log("Max Equip Items");
                    }
                }
                break;
                break;
            case Item.ItemType.Consumable:
                currentlySelectedItem.UseConsumableItem();
                GameManager.instance.RemoveItem(currentlySelectedItem);
                break;
            case Item.ItemType.Material:
                selectedButton1.SetActive(false);
                break;
        }
        GameManager.instance.ClearSlots();
    }

    public void Button2_Pressed()
    {
        if (mainEquipped || secondaryEquipped)
        {
            if (mainEquipped && currentlySelectedItem.isEquippedMain)
            {
                mainEquipment.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                mainEquipped = false;
                currentlySelectedItem.isEquippedMain = false;
                mainEquipedItem = null;
            }
            else if (secondaryEquipped && currentlySelectedItem.isEquippedSecondary)
            {
                secondaryEquipment.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                secondaryEquipped = false;
                currentlySelectedItem.isEquippedSecondary = false;
                secondaryEquipedItem = null;
            }
            else
            {
                Debug.Log("Max Equipped Items already");
            }
        }
        GameManager.instance.RemoveItem(currentlySelectedItem);
        GameManager.instance.ClearSlots();
    }
}
