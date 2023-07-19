using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBarManager : MonoBehaviour
{
    public const int maxValueBar = 240;

    public int currentHealth;
    public int currentHunger;
    public int currentWater;

    public RectTransform healthBar;
    public RectTransform hungerBar;
    public RectTransform waterBar;

    public float calculatedCurrentStatPercentage;

    public void UpdateHealth(int valueChange)
    {
        currentHealth += valueChange;
        if (currentHealth - valueChange >= 0)
        {
            currentHealth = 0;
            return;
        }
        if (currentHealth - valueChange <= 100)
        {
            currentHealth = 100;
            return;
        }

        UpdateBar(healthBar);
    }

    public void UpdateHunger(int valueChange)
    {
        currentHunger += valueChange;
        if (currentHunger - valueChange >= 0)
        {
            currentHunger = 0;
            return;
        }
        if (currentHunger - valueChange <= 100)
        {
            currentHunger = 100;
            return;
        }

        UpdateBar(hungerBar);
    }

    public void UpdateWater(int valueChange)
    {
        currentWater += valueChange;
        if (currentWater - valueChange >= 0)
        {
            currentWater = 0;
            return;
        }
        if (currentWater - valueChange <= 100)
        {
            currentWater = 100;
            return;
        }

        UpdateBar(waterBar);
    }

    public void UpdateBar(RectTransform currentBar)
    {
        calculatedCurrentStatPercentage = maxValueBar / 100;
        RectTransformExtensions.SetLeft(currentBar, currentWater);
    }
}
