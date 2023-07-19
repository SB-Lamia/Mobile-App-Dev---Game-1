using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBarManager : MonoBehaviour
{
    private const int maxValueBar = 240;

    public float currentHealth;
    public float currentHunger;
    public float currentWater;

    public RectTransform healthBar;
    public RectTransform hungerBar;
    public RectTransform waterBar;

    private float calculatedCurrentStatPercentage;

    public void Awake()
    {
        SetupHuds();
    }

    public void SetupHuds()
    {
        UpdateBar(healthBar, currentHealth);
        UpdateBar(hungerBar, currentHunger);
        UpdateBar(waterBar, currentWater);
    }

    public void UpdateHealth(int valueChange)
    {
        currentHealth += valueChange;

        MaxMinValueChecker(ref currentHealth, valueChange);

        UpdateBar(healthBar, currentHealth);
    }

    public void UpdateHunger(int valueChange)
    {
        currentHunger += valueChange;

        MaxMinValueChecker(ref currentHunger, valueChange);

        UpdateBar(hungerBar, currentHunger);
    }

    public void UpdateWater(int valueChange)
    {
        currentWater += valueChange;

        MaxMinValueChecker(ref currentWater, valueChange);

        UpdateBar(waterBar, currentWater);
    }

    public void MaxMinValueChecker(ref float currentStat, int valueChange)
    {
        if (currentStat - valueChange >= 0)
        {
            currentStat = 0;
            return;
        }
        if (currentStat - valueChange <= 100)
        {
            currentStat = 100;
            return;
        }
    }

    public void UpdateBar(RectTransform currentBar, float currentStat)
    {
        calculatedCurrentStatPercentage = maxValueBar - (maxValueBar / 100 * currentStat);
        RectTransformExtensions.SetRight(currentBar, calculatedCurrentStatPercentage);
    }
}
