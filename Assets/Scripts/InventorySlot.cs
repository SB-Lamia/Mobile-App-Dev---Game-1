using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    //Knows which slot it is in in the inventory.
    public int childIndex;

    Item item;

    //Checks if it is in the players inventory or the traders inventory
    public enum InventoryLocation
    {
        Trader,
        InventorySystem
    }
    public InventoryLocation inventoryLocation;

    //Sets the inventory slot to be an item
    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.itemSprite;
        icon.enabled = true;
    }

    //Clears the inventory slot back to its default state.
    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
    }


    //Tells the inventory and trading managers that the inventory slot has been pressed.
    public void UserItem()
    {
        if (inventoryLocation == InventoryLocation.InventorySystem)
        {
            InventoryManager.Instance.ItemPressed(childIndex);
        }
        else if (inventoryLocation == InventoryLocation.Trader)
        {
            TradingSystemManager.Instance.ItemPressed(childIndex);
        }

    }
}
