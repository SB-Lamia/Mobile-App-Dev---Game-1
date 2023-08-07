using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameObject defaultHud;
    public GameObject attackHud;
    public GameObject itemsHud;
    public GameObject escapeHud;
    public GameObject skillsHud;

    public List<GameObject> allHuds = new List<GameObject>();

    void Awake()
    {
        allHuds.Add(defaultHud);
        allHuds.Add(attackHud);
        allHuds.Add(itemsHud);
        allHuds.Add(escapeHud);
        allHuds.Add(skillsHud);
    }

    public void EnableCertainHud(string hudEnableOption)
    {
        foreach(GameObject hud in allHuds)
        {
            hud.SetActive(false);
        }

        switch (hudEnableOption)
        {
            case "Default":
                defaultHud.SetActive(true);
                break;
            case "Attack":
                attackHud.SetActive(true);
                break;
            case "Item":
                itemsHud.SetActive(true);
                break;
            case "Escape":
                escapeHud.SetActive(true);
                break;
            case "Skill":
                skillsHud.SetActive(true);
                break;
            default:
                defaultHud.SetActive(true);
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
