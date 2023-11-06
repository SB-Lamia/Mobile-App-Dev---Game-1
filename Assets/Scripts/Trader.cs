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



}
