using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    public int[] cityEvents;
    public int currentDanger;
    public int eventCount;
    public const int MaximumEventCount = 5;

    private void Awake()
    {
        currentDanger = 0;
        eventCount = 0;
    }

    public void GenerateRandomCity()
    {
        cityEvents = new int[MaximumEventCount];
        for (int i = 0; i < cityEvents.Length; i++)
        {
            cityEvents[i] = Random.Range(0, 2);
        }
    }

    public void TriggerNextExplore()
    {
        if (eventCount < MaximumEventCount)
        {
            switch (cityEvents[eventCount])
            {
                case 0:
                    TriggerLootingEvent();
                    break;
                case 1:
                    TriggerEnemyEvent();
                    break;
                case 2:
                    TriggerSpecialEvent();
                    break;
                default:
                    Debug.Log("Not a valid City Event, please check city: " + this.gameObject.name + ". With the event number: " + cityEvents[eventCount]);
                    break;
            }

            eventCount++;
        }
        else
        {
            Debug.Log("Error: Maximum amount of events triggered. please check city: " + this.gameObject.name + ". With the event number: " + cityEvents[eventCount]);
        }

        CityManager.instance.SetupVisualCity();
    }

    public void TriggerLootingEvent()
    {
        int cityXP = 10 * PlayerStatManager.instance.Level / 2;
        LootManager.instance.ShowUserLoot(LootManager.instance.cityUILootElement, LootManager.instance.LuckCalculationRarity(3, PlayerStatManager.instance.Luck), cityXP);
        PlayerStatManager.instance.IncreaseEXP(cityXP);
    }

    public void TriggerEnemyEvent()
    {
        BattleManager.instance.StartCombat();
    }

    public void TriggerSpecialEvent()
    {
        //To Be Implemented
    }

}
