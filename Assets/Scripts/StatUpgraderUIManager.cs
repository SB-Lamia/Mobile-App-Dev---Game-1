using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class StatUpgraderUIManager : MonoBehaviour
{
    public GameObject statUpgraderUI;

    private int getVisualItemCount;

    public TextMeshProUGUI EnduranceUI;
    public TextMeshProUGUI AgilityUI;
    public TextMeshProUGUI IntelligenceUI;
    public TextMeshProUGUI PerceptionUI;
    public TextMeshProUGUI CharismaUI;
    public TextMeshProUGUI LuckUI;

    public TextMeshProUGUI PointsToAssign;

    public void OpenStatUpgradeMenu()
    {
        statUpgraderUI.SetActive(true);
        EnduranceUI.text = PlayerStatManager.instance.Endurance.ToString();
        LuckUI.text = PlayerStatManager.instance.Luck.ToString();
        PerceptionUI.text = PlayerStatManager.instance.Perception.ToString();
        CharismaUI.text = PlayerStatManager.instance.Charisma.ToString();
        AgilityUI.text = PlayerStatManager.instance.Agility.ToString();
        IntelligenceUI.text = PlayerStatManager.instance.Intelligence.ToString();

        PointsToAssign.text = PlayerStatManager.instance.PointsToAssign.ToString();
    }

    public void SaveStats()
    {

    }

    public void CloseStatUpgradeMenu()
    {
        statUpgraderUI.SetActive(false);
    }

    public void TemporaryStatIncrease()
    {
        TemporaryChangeStat(1);
    }

    public void TemporaryStatDecrease()
    {
        TemporaryChangeStat(-1);
    }

    public void TemporaryChangeStat(int valueChange)
    {
        GameObject statGameObject = EventSystem.current.currentSelectedGameObject;
        int.TryParse(statGameObject.transform.parent.GetChild(4).GetComponent<TextMeshProUGUI>().text, out getVisualItemCount);
        if ((PlayerStatManager.instance.PointsToAssign > 0) &&
            (getVisualItemCount < 100) &&
            (getVisualItemCount > 0))
        {
            getVisualItemCount += valueChange;
            PlayerStatManager.instance.PointsToAssign += valueChange;
            statGameObject.transform.parent.GetChild(4).GetComponent<TextMeshProUGUI>().text = getVisualItemCount.ToString();
        }
    }
}
