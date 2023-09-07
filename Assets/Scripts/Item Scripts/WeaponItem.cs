using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item", fileName = "New Weapon")]
public class WeaponItem : Item
{
    public bool isEquippedMain;
    public bool isEquippedSecondary;

    public float damageMultiplier;
    public float isRanged;
    public float critMultiplier;
}
