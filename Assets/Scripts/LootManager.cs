using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    public List<Item> allItems;

    public List<Item> commons = new List<Item>();
    public List<Item> uncommons = new List<Item>();
    public List<Item> rares = new List<Item>();
    public List<Item> epics = new List<Item>();
    public List<Item> legendarys = new List<Item>();

    // Start is called before the first frame update
    void Awake()
    {
        foreach(Item item in allItems)
        {
            switch (item.rarity)
            {
                case "Common":
                    commons.Add(item);
                    break;
                case "Uncommon":
                    uncommons.Add(item);
                    break;
                case "Rare":
                    rares.Add(item);
                    break;
                case "Epic":
                    epics.Add(item);
                    break;
                case "Legendary":
                    legendarys.Add(item);
                    break;
                default:
                    Debug.Log("Error: not a valid rarity please check item: " + item.name);
                    break;
            }
        }
    }

}
