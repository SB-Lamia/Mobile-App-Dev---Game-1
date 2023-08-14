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

    public string currentDialogueAction;
    public bool awaitingActionToResolve;

    public bool playerDecicidingAttack;

    public enum EnemyState
    {
        DoAction,
        UserFeedback,
        ResolvingBattlePhase
    }

    void Awake()
    {
        startingHealth = startingHealth * (1 + (PlayerStatManager.instance.Level / 10));
        currentAttack = currentAttack * (1 + (PlayerStatManager.instance.Level / 10));
        currentDefense  = currentDefense * (1 + (PlayerStatManager.instance.Level / 10));
        currentHealth = startingHealth;

        currentEnemyState = EnemyState.DoAction;
    }

    public void DealDamageToPlayer()
    {
        StatBarManager.instance.UpdateHealth(currentAttack * -1);
        currentDialogueAction = $"{enemyName} has dealt {Mathf.Round(currentAttack)} damage to you!";
        awaitingActionToResolve = false;
    }

    public void ActivateSpecialAbility()
    {
        awaitingActionToResolve = false;
    }

    public void Defend()
    {
        awaitingActionToResolve = false;
    }
}