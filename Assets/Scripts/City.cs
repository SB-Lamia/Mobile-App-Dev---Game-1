using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    public int[] cityEvents;
    public int currentDanger;

    private void Awake()
    {
        currentDanger = 0;
    }

    public void GenerateRandomCity()
    {
        cityEvents = new int[5];
        for (int i = 0; i < cityEvents.Length; i++)
        {
            cityEvents[i] = Random.Range(0, 3);
        }
    }

    public void triggerLootingEvent()
    {

    }

    public void triggerEnemyEvent()
    {

    }

    public void triggerSpecialEvent()
    {

    }

}
