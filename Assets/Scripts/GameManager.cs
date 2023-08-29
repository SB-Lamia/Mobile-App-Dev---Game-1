using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isPaused;

    public List<GameObject> disabledHud;

    public GameObject[] slots;

    public GameObject parentSlots;

    //public Dictionary<Item, int> itemDict = new Dictionary<Item, int>();

    public List<Item> items = new List<Item>();
    public List<int> itemNumbers = new List<int>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {

            }
        }
    }

    public void DisplayItems()
    {
        slots = new GameObject[parentSlots.transform.childCount];
        slots = GrabSlots();
        Debug.Log(slots.Length);


        for (int i = 0; i < items.Count; i++)
        {
            slots[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].itemSprite;

            slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
            slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemNumbers[i].ToString();
        }
    }

    public GameObject[] GrabSlots()
    {
        Debug.Log("Grabbing Slots");
        GameObject[] slotsHolder = new GameObject[parentSlots.transform.childCount];

        int children = parentSlots.transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            slotsHolder[i] = parentSlots.transform.GetChild(i).gameObject;
        }

        return slotsHolder;
    }

    public void AddItem(Item newItem)
    {
        if (!items.Contains(newItem))
        {
            items.Add(newItem);
            itemNumbers.Add(1);
        }
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                if(newItem == items[i])
                {
                    if (items[i].isStackable == true)
                    {
                        itemNumbers[i]++;
                    }
                    else
                    {
                        items.Add(newItem);
                        itemNumbers.Add(1);
                    }
                }
            }
        }
    }

    public void AddItemMultiple(List<Item> newItems)
    {
        foreach(Item newItem in newItems)
        {
            AddItem(newItem);
        }
    }


    public void RemoveItem(Item oldItem)
    {
        if (items.Contains(oldItem))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (oldItem == items[i])
                {
                    itemNumbers[i]--;
                    if (itemNumbers[i] == 0)
                    {
                        items.Remove(oldItem);
                        itemNumbers.Remove(itemNumbers[i]);
                    }
                }
            }
        }
    }

    public void ToggleDefaultHud(bool toggleState)
    {
        foreach (GameObject hud in disabledHud)
        {
            hud.SetActive(toggleState);
        }
    }
}
