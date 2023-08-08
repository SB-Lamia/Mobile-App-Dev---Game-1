using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy", fileName = "New Enemy")]
public class Enemy : ScriptableObject
{
    public string enemyName;
    public Sprite enemySprite;
    public float startingHealth;
    public float currentHealth;
    public float currentAttack;
    public float currentDefense;

    void Awake()
    {
        startingHealth = startingHealth * (1 + (PlayerStatManager.instance.Level / 10));
        currentAttack = currentAttack * (1 + (PlayerStatManager.instance.Level / 10));
        currentDefense  = currentDefense * (1 + (PlayerStatManager.instance.Level / 10));
        currentHealth = startingHealth;
    }
}