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
    public IEnumerator Test1_StartCombat()
    {
        InventoryManager.Instance.mainEquipedItem = GameManager.instance.items[3];
        InventoryManager.Instance.secondaryEquipedItem = GameManager.instance.items[2];
        CityManager.instance.Resume();
        CityManager.instance.currentCity.GetComponent<City>().cityEvents[2] = 1;
        CityManager.instance.Pause();
        CityManager.instance.TriggerCityEvent();
        Assert.AreEqual(3, CityManager.instance.currentCity.GetComponent<City>().eventCount);
        yield return null;
    }

    // Additional tests for other actions like PrimaryAttack, SecondaryAttack, Dodging, etc.

    [UnityTest]
    public IEnumerator Test2_CheckPrimaryEquipDuringCombat()
    {
        Assert.AreEqual(BattleManager.instance.primaryEquippedAttackButton.transform.GetChild(1).GetComponent<Image>().sprite, 
                        InventoryManager.Instance.mainEquipedItem.itemSprite);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Test3_CheckSecondaryEquipDuringCombat()
    {
        Assert.AreEqual(BattleManager.instance.secondaryEquippedAttackButton.transform.GetChild(1).GetComponent<Image>().sprite,
                        InventoryManager.Instance.secondaryEquipedItem.itemSprite);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Test4_DoBasicAttack()
    {
        PlayerStatManager.instance.Endurance = 1;
        BattleManager.instance.currentAttack = BattleManager.CurrentAttack.BasicAttack;
        BattleManager.instance.enemies[0].TakingDamageFromPlayer(PlayerStatManager.instance.Endurance * -1);
        Assert.Less(BattleManager.instance.enemies[0].currentHealth, BattleManager.instance.enemies[0].startingHealth);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Test5_DoBasicAttackKill()
    {
        int oldEnemyCount = BattleManager.instance.enemyMaxCount;
        BattleManager.instance.MoveEnemiesAfterDeath(0);
        PlayerStatManager.instance.Endurance = 50;
        BattleManager.instance.currentAttack = BattleManager.CurrentAttack.BasicAttack;
        BattleManager.instance.enemies[0].TakingDamageFromPlayer(PlayerStatManager.instance.Endurance * -1);
        Assert.Less(oldEnemyCount, BattleManager.instance.enemyMaxCount);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Test6_DoPrimaryAttack()
    {
        PlayerStatManager.instance.Perception = 1;
        BattleManager.instance.currentAttack = BattleManager.CurrentAttack.PrimaryAttack;
        BattleManager.instance.enemies[0].TakingDamageFromPlayer(PlayerStatManager.instance.Perception * -1);
        Assert.Less(BattleManager.instance.enemies[0].currentHealth, BattleManager.instance.enemies[0].startingHealth);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Test7_DoPrimaryAttackKill()
    {
        int oldEnemyCount = BattleManager.instance.enemyMaxCount;
        BattleManager.instance.MoveEnemiesAfterDeath(0);
        PlayerStatManager.instance.Perception = 50;
        BattleManager.instance.currentAttack = BattleManager.CurrentAttack.PrimaryAttack;
        BattleManager.instance.enemies[0].TakingDamageFromPlayer(PlayerStatManager.instance.Perception * -1);
        Assert.Less(oldEnemyCount, BattleManager.instance.enemyMaxCount);
        yield return null;
    }


    [UnityTest]
    public IEnumerator Test8_DoSecondaryAttack()
    {
        PlayerStatManager.instance.Perception = 1;
        BattleManager.instance.currentAttack = BattleManager.CurrentAttack.SecondaryAttack;
        BattleManager.instance.enemies[0].TakingDamageFromPlayer(PlayerStatManager.instance.Perception * -1);
        Assert.Less(BattleManager.instance.enemies[0].currentHealth, BattleManager.instance.enemies[0].startingHealth);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Test9_DoSecondaryAttackKill()
    {
        int oldEnemyCount = BattleManager.instance.enemyMaxCount;
        BattleManager.instance.MoveEnemiesAfterDeath(0);
        PlayerStatManager.instance.Perception = 50;
        BattleManager.instance.currentAttack = BattleManager.CurrentAttack.SecondaryAttack;
        BattleManager.instance.enemies[0].TakingDamageFromPlayer(PlayerStatManager.instance.Perception * -1);
        Assert.Less(oldEnemyCount, BattleManager.instance.enemyMaxCount);
        yield return null;
    }
}
