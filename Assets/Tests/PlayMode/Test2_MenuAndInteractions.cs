using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class Test2_MenuAndInteractions
{
    [UnityTest]
    public IEnumerator Test1_InventoryUI()
    {
        InventoryUIManager.instance.Pause();

        GameObject inventoryHud = GameObject.Find("Inventory system Hud");

        Assert.AreEqual(true, inventoryHud.activeSelf);

        yield return null;

        InventoryUIManager.instance.Resume();

        Assert.AreEqual(false, inventoryHud.activeSelf);

        yield return null;
    }
    [UnityTest]
    public IEnumerator Test2_StatMenuUI()
    {
        StatUpgraderUIManager.instance.OpenStatUpgradeMenu();

        GameObject StatMenuUI = GameObject.Find("Skill system");

        Assert.AreEqual(true, StatMenuUI.activeSelf);

        yield return null;

        StatUpgraderUIManager.instance.CloseStatUpgradeMenu();

        Assert.AreEqual(false, StatMenuUI.activeSelf);

        yield return null;
    }
    [UnityTest]
    public IEnumerator Test3_CityLocationMenu()
    {
        CityManager.instance.Pause();

        GameObject CityLocationMenu = GameObject.Find("CitySystem");

        Assert.AreEqual(true, CityLocationMenu.activeSelf);

        yield return null;

        CityManager.instance.Resume();

        Assert.AreEqual(false, CityLocationMenu.activeSelf);

        yield return null;
    }
    [UnityTest]
    public IEnumerator Test4_CityHasBeenExplored()
    {
        CityManager.instance.Resume();
        CityManager.instance.currentCity.GetComponent<City>().cityEvents[0] = 0;
        CityManager.instance.Pause();
        CityManager.instance.TriggerCityEvent();
        Assert.AreEqual(1, CityManager.instance.currentCity.GetComponent<City>().eventCount);
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test5_CityLootingHasAddedItems()
    {
        List<Item> oldPlayerItems = GameManager.instance.items;
        List<int> oldPlayerItemNumbers = GameManager.instance.itemNumbers;
        CityManager.instance.currentCity.GetComponent<City>().cityEvents[1] = 0;
        CityManager.instance.TriggerCityEvent();

        for(int i = 0; i < LootManager.instance.recentlyAddedItems.Count; i++)
        {
            if (GameManager.instance.items.Contains(LootManager.instance.recentlyAddedItems[i]))
            {
                for (int k = 0; k < GameManager.instance.items.Count; k++)
                {
                    if (GameManager.instance.items[k].itemName == LootManager.instance.recentlyAddedItems[i].itemName)
                    {
                        Assert.AreEqual(oldPlayerItemNumbers[k] + 1, GameManager.instance.itemNumbers[k]);
                    }
                }
            }
            else
            {
                Assert.AreEqual(LootManager.instance.recentlyAddedItems[i], GameManager.instance.items[GameManager.instance.items.Count- LootManager.instance.recentlyAddedItems.Count+i]);
            }
        }
        CityManager.instance.Resume();
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test6_TestPauseMenu()
    {
        GameObject.Find("PauseMenuManager").GetComponent<PauseMenuManager>().ResumeGame();

        GameObject pauseMenuUI = GameObject.Find("PauseMenuUI");

        Assert.AreEqual(true, pauseMenuUI.activeSelf);

        yield return null;

        GameObject.Find("PauseMenuManager").GetComponent<PauseMenuManager>().ResumeGame();

        Assert.AreEqual(false, pauseMenuUI.activeSelf);

        yield return null;
    }
}
