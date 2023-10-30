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
        Equippable,
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
    public string[] somethingConsumable;
    public int plusHealth;
    public int plusHunger;
    public int plusWater;
    public int plusEnd;     //0
    public int plusPer;     //1
    public int plusChar;    //2
    public int plusLuck;    //3
    public int plusInt;     //4
    public int plusAgil;    //5
    public int duration;    

    public void UseConsumableItem()
    {
        ConsumableItemChecker(0, plusEnd);
        ConsumableItemChecker(1, plusPer);
        ConsumableItemChecker(2, plusChar);
        ConsumableItemChecker(3, plusLuck);
        ConsumableItemChecker(4, plusInt);
        ConsumableItemChecker(5, plusAgil);
    }

    private void ConsumableItemChecker(int status, int buff)
    {
        if (buff != 0)
        {
            GameManager.instance.AddBuff(status, buff, duration);
        }
    }

    [Header("Equippable Item")]
    public string[] somethingEquippable;


    [Header("Material Item")]
    public string[] somethingMaterial;

}
