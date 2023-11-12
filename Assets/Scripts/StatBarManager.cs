using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatBarManager : MonoBehaviour
{
    private const int maxValueBar = 200;

    public static StatBarManager instance;

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
    }

    public void SetupHuds()
    {
        UpdateBar();
        UpdateXPBar();
    }

    public void UpdateBar()
    {
        healthStatText.text = Mathf.Round(PlayerStatManager.instance.currentHealth) + " / 100";
        calculatedCurrentStatPercentage = maxValueBar - (maxValueBar / 100 * PlayerStatManager.instance.currentHealth);
        RectTransformExtensions.SetRight(healthBar, calculatedCurrentStatPercentage);
        hungerStatText.text = Mathf.Round(PlayerStatManager.instance.currentHunger) + " / 100";
        calculatedCurrentStatPercentage = maxValueBar - (maxValueBar / 100 * PlayerStatManager.instance.currentHunger);
        RectTransformExtensions.SetRight(hungerBar, calculatedCurrentStatPercentage);
        waterStatText.text = Mathf.Round(PlayerStatManager.instance.currentWater) + " / 100";
        calculatedCurrentStatPercentage = maxValueBar - (maxValueBar / 100 * PlayerStatManager.instance.currentWater);
        RectTransformExtensions.SetRight(waterBar, calculatedCurrentStatPercentage);
    }

    public void UpdateXPBar()
    {
        float currentXPPercentage = (float)PlayerStatManager.instance.Experience / (float)PlayerStatManager.instance.currentXPNeeded;

        xpBar.fillAmount = currentXPPercentage;

        levelText.text = PlayerStatManager.instance.Level.ToString();
    }
}
