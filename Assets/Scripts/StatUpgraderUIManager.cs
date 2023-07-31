using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class StatUpgraderUIManager : MonoBehaviour
{
    public GameObject statUpgraderUI;

    [SerializeField]
    private string[] statDescriptions;

    private int getVisualItemCount;

    public TextMeshProUGUI EnduranceUI;
    public TextMeshProUGUI AgilityUI;
    public TextMeshProUGUI IntelligenceUI;
    public TextMeshProUGUI PerceptionUI;
    public TextMeshProUGUI CharismaUI;
    public TextMeshProUGUI LuckUI;

    public TextMeshProUGUI PointsToAssign;

    public static StatUpgraderUIManager instance;

    public int statPointChange;

    public TextMeshProUGUI descriptionStat;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {

            }
        }
    }

    public void OpenStatUpgradeMenu()
    {
        statUpgraderUI.SetActive(true);
        EnduranceUI.text = PlayerStatManager.instance.Endurance.ToString();
        LuckUI.text = PlayerStatManager.instance.Luck.ToString();
        PerceptionUI.text = PlayerStatManager.instance.Perception.ToString();
        CharismaUI.text = PlayerStatManager.instance.Charisma.ToString();
        AgilityUI.text = PlayerStatManager.instance.Agility.ToString();
        IntelligenceUI.text = PlayerStatManager.instance.Intelligence.ToString();

        statPointChange = PlayerStatManager.instance.PointsToAssign;

        PointsToAssign.text = PlayerStatManager.instance.PointsToAssign.ToString();

        PointsToAssign.text = PlayerStatManager.instance.PointsToAssign.ToString();
    }

    public void SaveStats()
    {
        Debug.Log("SavingStats");
        int tempNum;
        int.TryParse(EnduranceUI.text, out tempNum);
        PlayerStatManager.instance.Endurance = tempNum;
        int.TryParse(LuckUI.text, out tempNum);
        PlayerStatManager.instance.Luck = tempNum;
        int.TryParse(PerceptionUI.text, out tempNum);
        PlayerStatManager.instance.Perception = tempNum;
        int.TryParse(CharismaUI.text, out tempNum);
        PlayerStatManager.instance.Charisma = tempNum;
        int.TryParse(AgilityUI.text, out tempNum);
        PlayerStatManager.instance.Agility = tempNum;
        int.TryParse(IntelligenceUI.text, out tempNum);
        PlayerStatManager.instance.Intelligence = tempNum;
    }

    public void CloseStatUpgradeMenu()
    {
        statUpgraderUI.SetActive(false);
    }

    public void CloseAndSaveStats()
    {
        SaveStats();
        CloseStatUpgradeMenu();
    }

    public void TemporaryStatIncrease()
    {
        TemporaryChangeStat(1, false);
    }

    public void TemporaryStatDecrease()
    {
        TemporaryChangeStat(-1, true);
    }

    public void BringUpDescription()
    {
        GameObject statGameObject = EventSystem.current.currentSelectedGameObject;
        switch (statGameObject.transform.parent.parent.gameObject.name)
        {
            case "Endurance":
                descriptionStat.text = statDescriptions[0];
                break;
            case "Agility":
                descriptionStat.text = statDescriptions[5];
                break;
            case "Intelligence":
                descriptionStat.text = statDescriptions[4];
                break;
            case "Perception":
                descriptionStat.text = statDescriptions[1];
                break;
            case "Luck":
                descriptionStat.text = statDescriptions[3];
                break;
            case "Charisma":
                descriptionStat.text = statDescriptions[2];
                break;
        }
    }

    public void TemporaryChangeStat(int valueChange, bool statPointChangeChecker)
    {
        GameObject statGameObject = EventSystem.current.currentSelectedGameObject;
        int.TryParse(statGameObject.transform.parent.GetChild(2).GetComponent<TextMeshProUGUI>().text, out getVisualItemCount);
        int currentStatValue = 0;
        switch (statGameObject.transform.parent.parent.gameObject.name)
        {
            case "Endurance":
                currentStatValue = PlayerStatManager.instance.Endurance;
                break;
            case "Agility":
                currentStatValue = PlayerStatManager.instance.Agility;
                break;
            case "Intelligence":
                currentStatValue = PlayerStatManager.instance.Intelligence;
                break;
            case "Perception":
                currentStatValue = PlayerStatManager.instance.Perception;
                break;
            case "Luck":
                currentStatValue = PlayerStatManager.instance.Luck;
                break;
            case "Charisma":
                currentStatValue = PlayerStatManager.instance.Charisma;
                break;
        }
        if ((PlayerStatManager.instance.PointsToAssign > 0) &&
            (getVisualItemCount < 100) &&
            (getVisualItemCount > 5) &&
            (getVisualItemCount >= currentStatValue))
        {
            if (statPointChangeChecker == true)
            {
                statPointChange++;
            }
            else if (statPointChangeChecker == false)
            {
                statPointChange--;
            }
            getVisualItemCount += valueChange;
            PlayerStatManager.instance.PointsToAssign += valueChange;
            PointsToAssign.text = statPointChange.ToString();
            statGameObject.transform.parent.GetChild(2).GetComponent<TextMeshProUGUI>().text = getVisualItemCount.ToString();
        }
    }
}
