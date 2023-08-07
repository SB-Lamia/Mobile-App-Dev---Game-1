using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject defaultHud;
    public GameObject attackHud;
    public GameObject itemsHud;
    public GameObject escapeHud;
    public GameObject skillsHud;

    

    public void EnableCertainHud(string hudEnableOption)
    {
        switch (hudEnableOption)
        {
            case "Default":
                break;
            case "Attack":
                break;
            case "Item":
                break;
            case "Escape":
                break;
            case "Skill":
                break;
            default:
                Debug.Log("Not a valid option for battle system. Please check if this is the correct input: " + hudEnableOption);
                break;
        }
    }

    public void AttackButton()
    {
        //Open Attack Options
        //Depending on current weapon equip?
        //Default sword?
    }

    public void ItemsButton()
    {
        //Open Items Options?
        //Go to Inventory?
        //After use return and pass turn.
    }

    public void Skills()
    {
        //trigger skills from a list
        //added MUCH later on
    }

    public void Escape()
    {
        //have a chance to escape
        //percentage
    }


}
