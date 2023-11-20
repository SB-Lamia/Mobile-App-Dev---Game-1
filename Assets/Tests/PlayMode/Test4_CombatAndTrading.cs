using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class Test4_CombatAndTrading
{
    [UnityTest]
    // Test to check if the Stat Upgrade menu is opened correctly
    public IEnumerator Test1_StatMenuUI()
    {
        // Open the Stat Upgrade menu
        StatUpgraderUIManager.instance.OpenStatUpgradeMenu();

        // Find the Stat Menu UI object
        GameObject StatMenuUI = GameObject.Find("Skill system");

        // Assert that the Stat Menu UI is active
        Assert.AreEqual(true, StatMenuUI.activeSelf);

        // Allow a frame to run for progress
        yield return null;
    }

    [UnityTest]
    // Test to check if a stat value can be upgraded
    public IEnumerator Test2_UpgradeStatValue()
    {
        // Store the initial value of Charisma
        int oldCharisma = PlayerStatManager.instance.Charisma;

        // Temporarily change and save the Charisma stat
        StatUpgraderUIManager.instance.TemporaryChangeStat(1, false, "Charisma");
        StatUpgraderUIManager.instance.CloseAndSaveStats();

        // Assert that Charisma has increased
        Assert.Greater(PlayerStatManager.instance.Charisma, oldCharisma);

        // Allow a frame to run for progress
        yield return null;
    }

    [UnityTest]
    // Test to check if a stat value change can be canceled
    public IEnumerator Test3_CancelStatValueChange()
    {
        // Open the Stat Upgrade menu
        StatUpgraderUIManager.instance.OpenStatUpgradeMenu();

        // Store the initial value of Charisma
        int oldCharisma = PlayerStatManager.instance.Charisma;

        // Temporarily change the Intelligence stat (which shouldn't affect Charisma)
        StatUpgraderUIManager.instance.TemporaryChangeStat(1, false, "Intelligence");

        // Allow a frame to run for progress
        yield return null;

        // Close the Stat Upgrade menu without saving
        StatUpgraderUIManager.instance.CloseStatUpgradeMenu();

        // Assert that Charisma remains unchanged
        Assert.AreEqual(oldCharisma, PlayerStatManager.instance.Charisma);

        // Allow a frame to run for progress
        yield return null;
    }
}