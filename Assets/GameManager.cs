using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isPaused;

    public GameObject[] slots;

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
        Debug.Log("DisplayItems");
        for (int i = 0; i < items.Count; i++)
        {
            Debug.Log(items[i]);
            Debug.Log(itemNumbers[i]);
            slots[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].itemSprite;

            slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
            slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemNumbers[i].ToString();
        }
    }

    private void AddItem(Item newItem)
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
                    itemNumbers[i]++;
                }
            }
        }
    }
    private void RemoveItem(Item oldItem)
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


}
