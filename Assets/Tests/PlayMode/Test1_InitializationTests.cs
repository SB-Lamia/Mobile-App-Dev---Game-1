using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class Test1_InitializationTests
{
    // Reference to the player movement component
    private PlayerMovement playerMovement;

    [UnityTest]
    // Test to check if the player's initial hunger is set correctly
    public IEnumerator Test1_StartingFood()
    {
        SceneManager.LoadScene("TestScene");

        // Wait for a frame to allow scene loading
        yield return null;

        // Assert that the player's initial hunger is 100
        Assert.AreEqual(100, PlayerStatManager.instance.currentHunger);
    }

    [UnityTest]
    // Test to check if the player's initial water is set correctly
    public IEnumerator Test2_StartingWater()
    {
        // Assert that the player's initial water is 100
        Assert.AreEqual(100, PlayerStatManager.instance.currentWater);
        yield return null;
    }

    [UnityTest]
    // Test to check if the player's initial health is set correctly
    public IEnumerator Test3_StartingHealth()
    {
        // Assert that the player's initial health is 100
        Assert.AreEqual(100, PlayerStatManager.instance.currentHealth);
        yield return null;
    }

    [UnityTest]
    // Test to check if the player's initial inventory is set correctly
    public IEnumerator Test4_StartingInventory()
    {
        // Assert various items and their quantities in the player's initial inventory
        Assert.AreEqual("Bread", GameManager.instance.items[0].itemName);
        Assert.AreEqual(5, GameManager.instance.itemNumbers[0]);
        // ... (similar assertions for other items)
        yield return null;
    }

    [UnityTest]
    // Test to check if the player's initial stats are set correctly
    public IEnumerator Test5_StartingStats()
    {
        // Assert initial values for different player stats
        Assert.AreEqual(5, PlayerStatManager.instance.Intelligence);
        // ... (similar assertions for other stats)
        yield return null;
    }

    // Reference to a city location object
    public GameObject cityLocation;

    // Helper function to wait for a condition to be true
    private IEnumerator WaitFor(System.Func<bool> condition)
    {
        while (!condition())
            yield return null;
    }

    [UnityTest]
    // Test to check if player stats are correctly updated after the first movement
    public IEnumerator Test6_FirstMovementCorrectValues()
    {
        // Allow one frame to run for progress
        yield return null;

        // Close the startup menu
        StatUpgraderUIManager.instance.CloseAndSaveStats();

        // Find and set the playerMovement component
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();

        // Spawn a city location
        cityLocation = CitySpawnerManager.instance.SpawnCity(10, 10);

        // Set playerMovement properties
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerMovement.touchedObject = cityLocation;
        playerMovement.CheckObjectTouched(new Vector2(5, 5));
        playerMovement.playerConfirmedMovementYes();

        // Wait for the movement to finish
        yield return WaitFor(() => !playerMovement.isMoving);

        // Check the player stats after the movement
        Assert.AreEqual(85, PlayerStatManager.instance.currentHunger); // (100 - 15)
        Assert.AreEqual(85, PlayerStatManager.instance.currentWater); // (100 - 15)
    }

    [UnityTest]
    // Test to check if player stats are correctly updated after the second movement
    public IEnumerator Test7_SecondMovementCorrectValues()
    {
        yield return null;

        // Spawn another city location
        cityLocation = CitySpawnerManager.instance.SpawnCity(30, 30);

        // Set playerMovement properties
        playerMovement.touchedObject = cityLocation;
        playerMovement.CheckObjectTouched(new Vector2(5, 5));
        playerMovement.playerConfirmedMovementYes();

        // Wait for the movement to finish
        yield return WaitFor(() => !playerMovement.isMoving);

        // Expected values after movement
        // Calculate the expected new values using the distance between vectors
        float newValues = (float)85 - (float)28.28427;

        // Check the player stats after the movement
        Assert.AreEqual(newValues, PlayerStatManager.instance.currentHunger); // (100 - 15)
        Assert.AreEqual(newValues, PlayerStatManager.instance.currentWater); // (100 - 15)
    }
}
