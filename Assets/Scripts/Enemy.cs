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
    public int averageEnemyLevelEncounter;

    public EnemyState currentEnemyState;

    public float lastDamageDealt;

    public enum EnemyState
    {
        PreparedToAction,
        DecidingAttack,
        ChangingUserStats,
        UserFeedback,
        ResolvingBattlePhase,
        Idle
    }

    void Awake()
    {
        startingHealth = startingHealth * (1 + (PlayerStatManager.instance.Level / 10));
        currentAttack = currentAttack * (1 + (PlayerStatManager.instance.Level / 10));
        currentDefense  = currentDefense * (1 + (PlayerStatManager.instance.Level / 10));
        currentHealth = startingHealth;

        currentEnemyState = EnemyState.Idle;
    }

    public void DealDamageToPlayer()
    {

    }

    public void ActivateSpecialAbility()
    {

    }

    public void Defend()
    {

    }
}