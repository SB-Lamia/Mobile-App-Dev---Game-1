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

    [Header("Weapon Item")]
    public bool isEquippedMain;
    public bool isEquippedSecondary;

    public float damageMultiplier;
    public bool isRanged;
    public enum RangeDistance
    {
        firstSlot,
        secondSlot,
        thirdSlot,
        allSlots
    }
    public RangeDistance rangeDistance;
    public enum WeaponSpread
    {
        Single,
        adjacent,
        All
    }
    public WeaponSpread weaponSpread;
    public enum WeaponPenertration
    {
        None,
        One,
        Two,
        All
    }
    public WeaponPenertration weaponPenertration;
    
    public float critMultiplier;

    [Header("Consumable Item")]
    public string somethingConsumable;

    [Header("Armour Item")]
    public string somethingArmour;

    [Header("Material Item")]
    public string somethingMaterial;
}
