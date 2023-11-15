using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class Test1_InitializationTests
{
    private PlayerMovement playerMovement;

    [UnityTest]
    public IEnumerator Test1_StartingFood()
    {
        SceneManager.LoadScene("TestScene");

        yield return null;

        Assert.AreEqual(100, PlayerStatManager.instance.currentHunger);
    }
    [UnityTest]
    public IEnumerator Test2_StartingWater()
    {
        Assert.AreEqual(100, PlayerStatManager.instance.currentWater);
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test3_StartingHealth()
    {
        Assert.AreEqual(100, PlayerStatManager.instance.currentHealth);
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test4_StartingInventory()
    {
        Assert.AreEqual("Bread", GameManager.instance.items[0].itemName);
        Assert.AreEqual(5, GameManager.instance.itemNumbers[0]);
        Assert.AreEqual("Purified Water", GameManager.instance.items[1].itemName);
        Assert.AreEqual(5, GameManager.instance.itemNumbers[1]);
        Assert.AreEqual("Steel Machete", GameManager.instance.items[2].itemName);
        Assert.AreEqual(1, GameManager.instance.itemNumbers[2]);
        Assert.AreEqual("Remington 870 Shotgun", GameManager.instance.items[3].itemName);
        Assert.AreEqual(1, GameManager.instance.itemNumbers[3]);
        yield return null;
    }
    [UnityTest]
    public IEnumerator Test5_StartingStats()
    {
        Assert.AreEqual(5, PlayerStatManager.instance.Intelligence);
        Assert.AreEqual(5, PlayerStatManager.instance.Endurance);
        Assert.AreEqual(5, PlayerStatManager.instance.Agility);
        Assert.AreEqual(5, PlayerStatManager.instance.Perception);
        Assert.AreEqual(5, PlayerStatManager.instance.Charisma);
        Assert.AreEqual(5, PlayerStatManager.instance.Luck);
        yield return null;
    }
    public GameObject cityLocation;

    // Helper function to wait for a condition to be true
    private IEnumerator WaitFor(System.Func<bool> condition)
    {
        while (!condition())
            yield return null;
    }

    [UnityTest]
    public IEnumerator Test6_FirstMovementCorrectValues()
    {
        //Lets one frame run to progress.
        yield return null;

        // Close the startup menu
        StatUpgraderUIManager.instance.CloseAndSaveStats();

        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();

        cityLocation = CitySpawnerManager.instance.SpawnCity(10, 10);

        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerMovement.touchedObject = cityLocation;
        playerMovement.CheckObjectTouched(new Vector2(5, 5));
        playerMovement.playerConfirmedMovementYes();

        // Wait for the movement to finish
        yield return WaitFor(() => !playerMovement.isMoving);

        // Check the player stats
        Assert.AreEqual(85, PlayerStatManager.instance.currentHunger); // (100 - 15)
        Assert.AreEqual(85, PlayerStatManager.instance.currentWater); // (100 - 15)
    }

    [UnityTest]
    public IEnumerator Test7_SecondMovementCorrectValues()
    {
        yield return null;

        cityLocation = CitySpawnerManager.instance.SpawnCity(30, 30);

        playerMovement.touchedObject = cityLocation;
        playerMovement.CheckObjectTouched(new Vector2(5, 5));
        playerMovement.playerConfirmedMovementYes();

        // Wait for the movement to finish
        yield return WaitFor(() => !playerMovement.isMoving);

        //Expected values after movement
        //28.28427 is calculated using the distance between 2 vectors
        //from position 10,10 to position 30,30
        float newValues = (float)85 - (float)28.28427;

        // Check the player stats
        Assert.AreEqual(newValues, PlayerStatManager.instance.currentHunger); // (100 - 15)
        Assert.AreEqual(newValues, PlayerStatManager.instance.currentWater); // (100 - 15)
    }

}
