using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<Item> allItems;
    private void Awake()
    {
        foreach(Item item in allItems)
        {
            item.isEquippedMain = false;
            item.isEquippedSecondary = false;
        }
    }
}
