using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatBarManager : MonoBehaviour
{
    private const int maxValueBar = 200;

    public static StatBarManager instance;

    public float currentHealth;
    public float currentHunger;
    public float currentWater;

    public RectTransform healthBar;
    public RectTransform hungerBar;
    public RectTransform waterBar;

    public TextMeshProUGUI healthStatText;
    public TextMeshProUGUI hungerStatText;
    public TextMeshProUGUI waterStatText;

    private float calculatedCurrentStatPercentage;

    public Image xpBar;
    public TextMeshProUGUI levelText;


    public void Awake()
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

    public void SetupBaseStats(
        float defaultHunger,
        float defaultWater,
        float defaultHealth)
    {
        currentHealth = defaultHealth;
        currentHunger = defaultHunger;
        currentWater = defaultWater;
        SetupHuds();
    }

    public void SetupHuds()
    {
        UpdateBar(healthBar, currentHealth, healthStatText);
        UpdateBar(hungerBar, currentHunger, hungerStatText);
        UpdateBar(waterBar, currentWater, waterStatText);

        UpdateXPBar();
    }

    public void UpdateHealth(float valueChange)
    {
        currentHealth += valueChange;

        MaxMinValueChecker(ref currentHealth, valueChange);

        UpdateBar(healthBar, currentHealth, healthStatText);
    }

    public void UpdateHunger(float valueChange)
    {
        currentHunger += valueChange;

        MaxMinValueChecker(ref currentHunger, valueChange);

        UpdateBar(hungerBar, currentHunger, hungerStatText);
    }

    public void UpdateWater(float valueChange)
    {
        currentWater += valueChange;

        MaxMinValueChecker(ref currentWater, valueChange);

        UpdateBar(waterBar, currentWater, waterStatText);
    }

    public void MaxMinValueChecker(ref float currentStat, float valueChange)
    {
        if (currentStat <= 0)
        {
            currentStat = 0;
            return;
        }
        if (currentStat >= 100)
        {
            currentStat = 100;
            return;
        }
    }

    public void UpdateBar(RectTransform currentBar, float currentStat, TextMeshProUGUI currentTextStat)
    {
        currentTextStat.text = Mathf.Round(currentStat) + " / 100";
        calculatedCurrentStatPercentage = maxValueBar - (maxValueBar / 100 * currentStat);
        RectTransformExtensions.SetRight(currentBar, calculatedCurrentStatPercentage);
    }

    public void UpdateXPBar()
    {
        float currentXPPercentage = (float)PlayerStatManager.instance.Experience / (float)PlayerStatManager.instance.currentXPNeeded;

        xpBar.fillAmount = currentXPPercentage;

        levelText.text = PlayerStatManager.instance.Level.ToString();
    }
}
