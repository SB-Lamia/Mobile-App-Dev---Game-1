using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    //Hud GameObjects
    public GameObject dialoguePrefab;
    public GameObject dialogueHud;
    public GameObject currentDialogueGameObject;
    public GameObject defaultHud;
    public GameObject attackHud;
    public GameObject itemsHud;
    public GameObject escapeHud;
    public GameObject skillsHud;
    public List<GameObject> allHuds = new List<GameObject>();

    public bool playerTurn;


    //Enemy Scripts
    public List<Enemy> enemies = new List<Enemy>();

    void Awake()
    {
        allHuds.Add(defaultHud);
        allHuds.Add(attackHud);
        allHuds.Add(itemsHud);
        allHuds.Add(escapeHud);
        allHuds.Add(skillsHud);
        allHuds.Add(dialogueHud);

        playerTurn = true;
    }

    // Player Enemy Swapper

    private void Update()
    {
        if (playerTurn)
        {
            //Check PlayerAction
        }
        else if (!playerTurn)
        {
            for (int i = 0; i > enemies.Count; i += 0) 
            {
                switch (enemies[i].currentEnemyState)
                {
                    case Enemy.EnemyState.PreparedToAction:
                        enemies[i].currentEnemyState = Enemy.EnemyState.DecidingAttack;
                        break;
                    case Enemy.EnemyState.DecidingAttack:
                        DecideEnemyAttack(enemies[i]);
                        break;
                    case Enemy.EnemyState.ChangingUserStats:
                        break;
                    case Enemy.EnemyState.UserFeedback:
                        break;
                    case Enemy.EnemyState.ResolvingBattlePhase:
                        break;
                    case Enemy.EnemyState.Idle:
                        enemies[i].currentEnemyState = Enemy.EnemyState.PreparedToAction; 
                        i++;
                        break;
                }
            }
        }
    }

    // PLAYER INPUTS / SCRIPTS

    public void EnableCertainHud(string hudEnableOption)
    {
        foreach(GameObject hud in allHuds)
        {
            hud.SetActive(false);
        }

        switch (hudEnableOption)
        {
            case "Default":
                defaultHud.SetActive(true);
                break;
            case "Attack":
                attackHud.SetActive(true);
                break;
            case "Item":
                itemsHud.SetActive(true);
                break;
            case "Escape":
                escapeHud.SetActive(true);
                break;
            case "Skill":
                skillsHud.SetActive(true);
                break;
            case "Dialogue":
                dialogueHud.SetActive(true);
                break;
            default:
                defaultHud.SetActive(true);
                Debug.Log("Not a valid option for battle system. Please check if this is the correct input: " + hudEnableOption);
                break;
        }
    }

    public void AttackButton()
    {
        //Open Attack Options
        //Depending on current weapon equip?
        //Default sword?
    }

    public void ItemsButton()
    {
        //Open Items Options?
        //Go to Inventory?
        //After use return and pass turn.
    }

    public void Skills()
    {
        //trigger skills from a list
        //added MUCH later on
    }

    public void Escape()
    {
        //have a chance to escape
        //percentage
    }


    // ENEMY INPUTS / SCRIPTS

    public void DecideEnemyAttack(Enemy currentEnemy)
    {
        //Attack
        //SpecialAbility
        //Defend

        switch(Random.Range(0, 3))
        {
            case 0:
                currentEnemy.DealDamageToPlayer();
                break;
            case 1:
                currentEnemy.ActivateSpecialAbility();
                break;
            case 2:
                currentEnemy.Defend();
                break;
            default:
                Debug.Log("Enemy broke");
                break;
        }
    }

    //decide enemy action

    //do enemy action

    //change user stats

    //tell user what they did

    //pass it to player

}
