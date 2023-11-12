using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trader : MonoBehaviour
{
    public List<Item> itemsForTrader = new List<Item>();
    public List<int> itemCount = new List<int>();
    public int traderLevel;
    public int traderItemCount;

    public void GenerateItemsForTrade()
    {
        traderLevel = Random.Range(1, 100);
        traderItemCount = Random.Range(4, 10);


        (itemsForTrader, itemCount) = LootManager.instance.GenerateTraderLootTables(traderItemCount, traderLevel);
    }


    public void AddItem(Item newItem)
    {
        if (!itemsForTrader.Contains(newItem))
        {
            itemsForTrader.Add(newItem);
            itemCount.Add(1);
        }
        else
        {
            for (int i = 0; i < itemsForTrader.Count; i++)
            {
                if (newItem == itemsForTrader[i])
                {
                    if (itemsForTrader[i].isStackable == true)
                    {
                        itemCount[i]++;
                    }
                    else
                    {
                        itemsForTrader.Add(newItem);
                        itemCount.Add(1);
                    }
                }
            }
        }
    }

    public void RemoveItem(Item oldItem)
    {
        if (itemsForTrader.Contains(oldItem))
        {
            for (int i = 0; i < itemsForTrader.Count; i++)
            {
                if (oldItem == itemsForTrader[i])
                {
                    itemCount[i]--;
                    if (itemCount[i] == 0)
                    {
                        itemsForTrader.Remove(oldItem);
                        itemCount.Remove(itemCount[i]);
                    }
                }
            }
        }
    }

}
