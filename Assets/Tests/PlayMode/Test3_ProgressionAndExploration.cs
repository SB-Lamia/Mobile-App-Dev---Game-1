using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Test3_ProgressionAndExploration
{
    [UnityTest]
    // Test to check if combat is initiated correctly
    public IEnumerator Test1_StartCombat()
    {
        // Equip weapons for the test
        InventoryManager.Instance.mainEquipedItem = GameManager.instance.items[3];
        InventoryManager.Instance.secondaryEquipedItem = GameManager.instance.items[2];

        // Resume the city manager and trigger a combat event
        CityManager.instance.Resume();
        CityManager.instance.currentCity.GetComponent<City>().cityEvents[1] = 1;
        CityManager.instance.Pause();
        CityManager.instance.TriggerCityEvent();

        // Ensure that the event count is incremented
        yield return null;
        Assert.AreEqual(2, CityManager.instance.currentCity.GetComponent<City>().eventCount);

        // Allow a frame to run for progress
        yield return null;
    }

    [UnityTest]
    // Test to check if the primary weapon is correctly displayed during combat
    public IEnumerator Test2_CheckPrimaryEquipDuringCombat()
    {
        // Assert that the primary equipped weapon matches the displayed image
        Assert.AreEqual(BattleManager.instance.primaryEquippedAttackButton.transform.GetChild(1).GetComponent<Image>().sprite,
                        InventoryManager.Instance.mainEquipedItem.itemSprite);

        // Allow a frame to run for progress
        yield return null;
    }

    [UnityTest]
    // Test to check if the secondary weapon is correctly displayed during combat
    public IEnumerator Test3_CheckSecondaryEquipDuringCombat()
    {
        // Assert that the secondary equipped weapon matches the displayed image
        Assert.AreEqual(BattleManager.instance.secondaryEquippedAttackButton.transform.GetChild(1).GetComponent<Image>().sprite,
                        InventoryManager.Instance.secondaryEquipedItem.itemSprite);

        // Allow a frame to run for progress
        yield return null;
    }

    [UnityTest]
    // Test to check if a basic attack reduces enemy health
    public IEnumerator Test4_DoBasicAttack()
    {
        // Set player endurance for the test
        PlayerStatManager.instance.Endurance = 1;

        // Simulate a basic attack and assert that enemy health is reduced
        BattleManager.instance.currentAttack = BattleManager.CurrentAttack.BasicAttack;
        BattleManager.instance.enemies[0].TakingDamageFromPlayer(PlayerStatManager.instance.Endurance * -1);
        Assert.Less(BattleManager.instance.enemies[0].currentHealth, BattleManager.instance.enemies[0].startingHealth);

        // Allow a frame to run for progress
        yield return null;
    }

    [UnityTest]
    // Test to check if a basic attack results in killing an enemy
    public IEnumerator Test5_DoBasicAttackKill()
    {
        // Store the initial enemy count
        int oldEnemyCount = BattleManager.instance.enemyMaxCount;

        // Set player endurance for the test
        PlayerStatManager.instance.Endurance = 50;

        // Simulate a basic attack resulting in enemy death and assert the enemy count decreases
        BattleManager.instance.currentAttack = BattleManager.CurrentAttack.BasicAttack;
        BattleManager.instance.enemies[0].TakingDamageFromPlayer(PlayerStatManager.instance.Endurance * -1);
        BattleManager.instance.MoveEnemiesAfterDeath(0);
        Assert.Less(BattleManager.instance.enemyMaxCount, oldEnemyCount);

        // Allow a frame to run for progress
        yield return null;
    }

    [UnityTest]
    // Test to check if a primary attack reduces enemy health
    public IEnumerator Test6_DoPrimaryAttack()
    {
        // Set player perception for the test
        PlayerStatManager.instance.Perception = 1;

        // Simulate a primary attack and assert that enemy health is reduced
        BattleManager.instance.currentAttack = BattleManager.CurrentAttack.PrimaryAttack;
        BattleManager.instance.enemies[0].TakingDamageFromPlayer(PlayerStatManager.instance.Perception * -1);
        Assert.Less(BattleManager.instance.enemies[0].currentHealth, BattleManager.instance.enemies[0].startingHealth);

        // Allow a frame to run for progress
        yield return null;
    }

    [UnityTest]
    // Test to check if a primary attack results in killing an enemy
    public IEnumerator Test7_DoPrimaryAttackKill()
    {
        // Store the initial enemy count
        int oldEnemyCount = BattleManager.instance.enemyMaxCount;

        // Set player perception for the test
        PlayerStatManager.instance.Perception = 50;

        // Simulate a primary attack resulting in enemy death and assert the enemy count decreases
        BattleManager.instance.currentAttack = BattleManager.CurrentAttack.PrimaryAttack;
        BattleManager.instance.enemies[0].TakingDamageFromPlayer(PlayerStatManager.instance.Perception * -1);
        BattleManager.instance.MoveEnemiesAfterDeath(0);
        Assert.Less(BattleManager.instance.enemyMaxCount, oldEnemyCount);

        // Allow a frame to run for progress
        yield return null;
    }

    [UnityTest]
    // Test to check if a secondary attack reduces enemy health
    public IEnumerator Test8_DoSecondaryAttack()
    {
        // Set player perception for the test
        PlayerStatManager.instance.Perception = 1;

        // Simulate a secondary attack and assert that enemy health is reduced
        BattleManager.instance.currentAttack = BattleManager.CurrentAttack.SecondaryAttack;
        BattleManager.instance.enemies[0].TakingDamageFromPlayer(PlayerStatManager.instance.Perception * -1);
        Assert.Less(BattleManager.instance.enemies[0].currentHealth, BattleManager.instance.enemies[0].startingHealth);

        // Allow a frame to run for progress
        yield return null;
    }

    [UnityTest]
    // Test to check if a secondary attack results in killing an enemy
    public IEnumerator Test9_DoSecondaryAttackKill()
    {
        // Store the initial enemy count
        int oldEnemyCount = BattleManager.instance.enemyMaxCount;

        // Set player perception for the test
        PlayerStatManager.instance.Perception = 50;

        // Simulate a secondary attack resulting in enemy death and assert the enemy count decreases
        BattleManager.instance.currentAttack = BattleManager.CurrentAttack.SecondaryAttack;
        BattleManager.instance.enemies[0].TakingDamageFromPlayer(PlayerStatManager.instance.Perception * -1);
        BattleManager.instance.MoveEnemiesAfterDeath(0);
        Assert.Less(BattleManager.instance.enemyMaxCount, oldEnemyCount);

        // Allow a frame to run for progress
        yield return null;
    }
}
