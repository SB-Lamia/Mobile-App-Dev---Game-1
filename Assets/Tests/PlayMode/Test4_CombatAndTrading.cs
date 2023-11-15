using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;


public class Test4_CombatAndTrading
{
    [UnityTest]
    public IEnumerator Test1_StatMenuUI()
    {
        StatUpgraderUIManager.instance.OpenStatUpgradeMenu();

        GameObject StatMenuUI = GameObject.Find("Skill system");

        Assert.AreEqual(true, StatMenuUI.activeSelf);

        yield return null;
    }

    [UnityTest]
    public IEnumerator Test2_UpgradeStatValue()
    {
        int oldCharisma = PlayerStatManager.instance.Charisma;
        StatUpgraderUIManager.instance.TemporaryChangeStat(1, false, "Charisma");
        StatUpgraderUIManager.instance.CloseAndSaveStats();
        Assert.Greater(oldCharisma, PlayerStatManager.instance.Charisma);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Test3_CancelStatValueChange()
    {
        StatUpgraderUIManager.instance.OpenStatUpgradeMenu();

        int oldCharisma = PlayerStatManager.instance.Charisma;
        StatUpgraderUIManager.instance.TemporaryChangeStat(1, false, "Charisma");
        StatUpgraderUIManager.instance.CloseAndSaveStats();
        Assert.AreEqual(oldCharisma, PlayerStatManager.instance.Charisma);
        yield return null;
    }
}
