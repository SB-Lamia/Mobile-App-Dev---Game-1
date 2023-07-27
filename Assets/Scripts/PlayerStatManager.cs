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
        SetupStarterStats(10, 10, 10, 10, 10, 10);
    }

    public void SetupStarterStats(
        int defaultEndurance,
        int defaultAgility,
        int defaultCharisma,
        int defaultPerception,
        int defaultLuck,
        int defaultIntelligence)
    {
        Endurance = defaultEndurance;
        Agility = defaultAgility;
        Charisma = defaultCharisma;
        Perception = defaultPerception;
        Intelligence = defaultIntelligence;
        Luck = defaultLuck;
    }
}
