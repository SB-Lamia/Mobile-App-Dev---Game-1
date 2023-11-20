using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
public class CitySpawnerTestScript
{
    // Variables for testing
    public Enemy enemy;
    public TestPlayerStatManager testPlayerStatManager;
    public GameObject obj = new GameObject();

    // Test 1: CitySpawnerManager creates cities with enough placed boxes
    [Test]
    public void Test1_CitySpawnerTestScriptSimplePasses()
    {
        CitySpawnerManager cityspawnerManager = new CitySpawnerManager();
        cityspawnerManager.NewCityLoad(200, 2000);
        Assert.Greater(cityspawnerManager.placedBoxes.Count, 150);
    }

    // Setup: Creating a sample enemy for testing
    [SetUp]
    public void Test2_SetupEnemy()
    {
        enemy = ScriptableObject.CreateInstance<Enemy>();
        enemy.enemyName = "Zombie";
        enemy.startingHealth = 20;
        enemy.currentAttack = 20;
        enemy.currentDefense = 20;
        enemy.averageEnemyLevelEncounter = 20;
        enemy.enemyXP = 5;
    }

    // Test 3: Check if enemy stats are set correctly
    [Test]
    public void Test3_EnemyStats()
    {
        Assert.AreEqual(enemy.currentAttack, 20);
        Assert.AreEqual(enemy.currentDefense, 20);
        Assert.AreEqual(enemy.averageEnemyLevelEncounter, 20);
        Assert.AreEqual(enemy.enemyXP, 5);
        Assert.AreEqual(enemy.startingHealth, 20);
    }

    // Test 4: Check if enemy stats are adjusted correctly
    [Test]
    public void Test4_AdjustedEnemyStats()
    {
        float tempHealth = enemy.startingHealth;
        float tempAttack = enemy.currentAttack;
        float tempDefense = enemy.currentDefense;

        enemy.AdjustStatsToMatchPlayer(50);

        Assert.Greater(enemy.currentHealth, tempHealth);
        Assert.Greater(enemy.currentAttack, tempAttack);
        Assert.Greater(enemy.currentDefense, tempDefense);
        Assert.AreEqual(enemy.currentEnemyState, Enemy.EnemyState.DoAction);
    }

    // Test 5: Check if TestPlayerStatManager is created and properties are set correctly
    [Test]
    public void Test5_CreatePlayerStatManager()
    {
        testPlayerStatManager = obj.AddComponent<TestPlayerStatManager>();
        testPlayerStatManager.Level = 1;
        Assert.AreEqual(testPlayerStatManager.Level, 1);
        testPlayerStatManager.currentXPNeeded = 20;
        Assert.AreEqual(testPlayerStatManager.currentXPNeeded, 20);
        testPlayerStatManager.Experience = 5;
        Assert.AreEqual(testPlayerStatManager.Experience, 5);
        testPlayerStatManager.Intelligence = 1;
        Assert.AreEqual(testPlayerStatManager.Intelligence, 1);
    }

    // Test 6: Check if XP is increased correctly
    [Test]
    public void Test6_UpdateXP()
    {
        testPlayerStatManager.IncreaseEXP(5);
        Assert.AreEqual(testPlayerStatManager.Experience, 10);
    }

    // Test 7: Check if leveling up works
    [Test]
    public void Test7_TestLevelUp()
    {
        testPlayerStatManager.IncreaseEXP(30);
        Assert.AreEqual(testPlayerStatManager.Level, 2);
    }

    // Test 8: Check if XP requirement increases after leveling up
    [Test]
    public void Test8_CheckIncreasedXPRequirement()
    {
        float oldCurrentXPNeeded = testPlayerStatManager.currentXPNeeded;
        testPlayerStatManager.IncreaseEXP(50);
        Assert.Greater(testPlayerStatManager.currentXPNeeded, oldCurrentXPNeeded);
    }
}

// TestPlayerStatManager class extending PlayerStatManager for testing
public class TestPlayerStatManager : PlayerStatManager, IMonoBehaviourTest
{
    public bool IsTestFinished
    {
        get { return true; }
    }
}