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

    public int enemyXP;
    public Item droppedItem;

    public enum EnemyState
    {
        DoAction,
        UserFeedback,
        ResolvingBattlePhase
    }

    public void AdjustStatsToMatchPlayer(int playerLevel)
    {
        startingHealth = startingHealth * (1 + (playerLevel / 10));
        currentAttack = currentAttack * (1 + (playerLevel / 10));
        currentDefense = currentDefense * (1 + (playerLevel / 10));
        currentHealth = startingHealth;

        currentEnemyState = EnemyState.DoAction;
    }

    public void DealDamageToPlayer()
    {
        if (Random.Range(0,100) <= PlayerStatManager.instance.Agility / 2)
        {
            currentDialogueAction = $"{enemyName} attempted to hit you, but you dodged out of the way!";
        }
        else
        {
            PlayerStatManager.instance.UpdateHealth(currentAttack * -1);
            currentDialogueAction = $"{enemyName} has dealt {Mathf.Round(currentAttack)} damage to you!";
        }
        awaitingActionToResolve = false;
    }

    //Please send Negative Values
    public void TakingDamageFromPlayer(float damage)
    {
        currentHealth += damage;
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