using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    public static PlayerStatManager instance;

    public int Level;
    public int Experience;
    public int Endurance;
    public int Agility;
    public int Charisma;
    public int Perception;
    public int Intelligence;
    public int Luck;

    public const int MaxLevel = 10;
    public const int MaxStat = 100;

    public const int StarterXPNeeded = 20;

    public int currentXPNeeded;
    public int overFlowXP;

    public int PointsToAssign;


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

    public void SetupStarterStats(
        int defaultLevel,
        int defaultExpereience,
        int defaultPointsToAssign,
        int defaultXPNeeded,
        int defaultEndurance,
        int defaultAgility,
        int defaultCharisma,
        int defaultPerception,
        int defaultLuck,
        int defaultIntelligence)
    {
        Level = defaultLevel;
        Experience = defaultExpereience;
        PointsToAssign = defaultPointsToAssign;
        currentXPNeeded = defaultXPNeeded;
        Endurance = defaultEndurance;
        Agility = defaultAgility;
        Charisma = defaultCharisma;
        Perception = defaultPerception;
        Intelligence = defaultIntelligence;
        Luck = defaultLuck;
    }

    public void UpdateLevel()
    {
        if (Level < 100)
        {
            Level++;
            PlayerStatManager.instance.PointsToAssign += (5 + Level);
        }
    }

    public void IncreaseEXP(int increaseAmmount)
    {
        Experience += Mathf.RoundToInt(increaseAmmount * (1 + (Intelligence / 100)));

        if (Experience >= currentXPNeeded)
        {
            overFlowXP = Experience - currentXPNeeded;
            UpdateLevel();
            Experience = 0 + overFlowXP;
            currentXPNeeded = Mathf.RoundToInt((float)currentXPNeeded * 1.5f);
        }

        StatBarManager.instance.UpdateXPBar();
    }

}
