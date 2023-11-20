using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class Test2_MenuAndInteractions
{
    [UnityTest]
    // Test to check if the inventory UI is correctly toggled
    public IEnumerator Test1_InventoryUI()
    {
        // Pause the inventory UI
        InventoryUIManager.instance.Pause();

        // Find the inventory HUD in the scene
        GameObject inventoryHud = GameObject.Find("Inventory system Hud");

        // Assert that the inventory HUD is active
        Assert.AreEqual(true, inventoryHud.activeSelf);

        // Allow a frame to run for progress
        yield return null;

        // Resume the inventory UI
        InventoryUIManager.instance.Resume();

        // Assert that the inventory HUD is inactive after resuming
        Assert.AreEqual(false, inventoryHud.activeSelf);

        // Allow a frame to run for progress
        yield return null;
    }

    [UnityTest]
    // Test to check if the stat menu UI is correctly toggled
    public IEnumerator Test2_StatMenuUI()
    {
        // Open the stat upgrade menu
        StatUpgraderUIManager.instance.OpenStatUpgradeMenu();

        // Find the stat menu UI in the scene
        GameObject StatMenuUI = GameObject.Find("Skill system");

        // Assert that the stat menu UI is active
        Assert.AreEqual(true, StatMenuUI.activeSelf);

        // Allow a frame to run for progress
        yield return null;

        // Close the stat upgrade menu
        StatUpgraderUIManager.instance.CloseStatUpgradeMenu();

        // Assert that the stat menu UI is inactive after closing
        Assert.AreEqual(false, StatMenuUI.activeSelf);

        // Allow a frame to run for progress
        yield return null;
    }

    [UnityTest]
    // Test to check if the city location menu UI is correctly toggled
    public IEnumerator Test3_CityLocationMenu()
    {
        // Pause the city manager
        CityManager.instance.Pause();

        // Find the city location menu in the scene
        GameObject CityLocationMenu = GameObject.Find("CitySystem");

        // Assert that the city location menu is active
        Assert.AreEqual(true, CityLocationMenu.activeSelf);

        // Allow a frame to run for progress
        yield return null;

        // Resume the city manager
        CityManager.instance.Resume();

        // Assert that the city location menu is inactive after resuming
        Assert.AreEqual(false, CityLocationMenu.activeSelf);

        // Allow a frame to run for progress
        yield return null;
    }

    [UnityTest]
    // Test to check if the city has been explored and event count is updated
    public IEnumerator Test4_CityHasBeenExplored()
    {
        // Resume the city manager
        CityManager.instance.Resume();

        // Set city event to 0 (not explored)
        CityManager.instance.currentCity.GetComponent<City>().cityEvents[0] = 0;

        // Pause the city manager and trigger a city event
        CityManager.instance.Pause();
        CityManager.instance.TriggerCityEvent();

        // Assert that the city event count is updated to 1
        Assert.AreEqual(1, CityManager.instance.currentCity.GetComponent<City>().eventCount);

        // Allow a frame to run for progress
        yield return null;
    }

    [UnityTest]
    // Test to check if the pause menu UI is correctly toggled
    public IEnumerator Test6_TestPauseMenu()
    {
        // Resume the game using PauseMenuManager
        GameObject.Find("PauseMenuManager").GetComponent<PauseMenuManager>().ResumeGame();

        // Find the pause menu UI in the scene
        GameObject pauseMenuUI = GameObject.Find("PauseMenuUI");

        // Assert that the pause menu UI is active
        Assert.AreEqual(true, pauseMenuUI.activeSelf);

        // Allow a frame to run for progress
        yield return null;

        // Resume the game again using PauseMenuManager
        GameObject.Find("PauseMenuManager").GetComponent<PauseMenuManager>().ResumeGame();

        // Assert that the pause menu UI is inactive after resuming
        Assert.AreEqual(false, pauseMenuUI.activeSelf);

        // Allow a frame to run for progress
        yield return null;
    }
}
