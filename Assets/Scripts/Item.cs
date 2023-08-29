using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item", fileName = "New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public string itemDesc;

    public Sprite itemSprite;

    public bool isStackable;
    public string rarity;

    public ItemType itemType;

    public bool isEquippedMain;
    public bool isEquippedSecondary;

    public enum ItemType
    {
        Weapon,
        Helmet,
        ChestPlate,
        Leggings,
        Feet,
        Consumable,
        Material
    }
}
