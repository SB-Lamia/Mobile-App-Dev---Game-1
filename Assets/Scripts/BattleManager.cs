using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    public bool isDialogueActive;
    public List<GameObject> allHuds = new List<GameObject>();

    public GameObject combatSystem;

    public List<GameObject> allEnemyLocations;

    public List<GameObject> currentlyUsedLocations;

    public static BattleManager instance;

    public bool playerTurn;

    public bool activeCombat;

    public int enemyMaxCount;
    public int currentEnemyTurn;

    public bool awaitingPlayerDialogue;

    public enum CurrentAttack
    {
        BasicAttack,
        PrimaryAttack,
        SecondaryAttack
    }

    public CurrentAttack currentAttack;

    //Enemy Scripts
    public List<Enemy> enemies = new List<Enemy>();

    public int selectedEnemy;

    void Awake()
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
        allHuds.Add(defaultHud);
        allHuds.Add(attackHud);
        allHuds.Add(itemsHud);
        allHuds.Add(escapeHud);
        allHuds.Add(skillsHud);
        allHuds.Add(dialogueHud);
        activeCombat = false;
    }

    public void StartCombat()
    {
        combatSystem.SetActive(true);
        enemies = new List<Enemy>();
        playerTurn = true;
        isDialogueActive = false;
        currentEnemyTurn = 0;
        EnableCertainHud("Default");
        enemies = EnemySpawnerManager.instance.SpawnRandomEnemy(2);
        Debug.Log(enemies.Count);
        enemyMaxCount = 2;
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].awaitingActionToResolve = true;
            currentlyUsedLocations.Add(allEnemyLocations[i]);
            currentlyUsedLocations[i].SetActive(true);
            currentlyUsedLocations[i].GetComponent<Image>().sprite = enemies[i].enemySprite;
            enemies[i].AdjustStatsToMatchPlayer();
        }
        activeCombat = true;
    }
    public void EndCombat()
    {
        EnableCertainHud("EndCombat");
        for (int i = 0; i > enemies.Count; i++)
        {
            enemies[i].awaitingActionToResolve = true;
            allEnemyLocations[i].SetActive(false);
        }
        combatSystem.SetActive(false);
        activeCombat = false;
    }
    // Player Enemy Swapper

    private void FixedUpdate()
    {
        if (activeCombat)
        {
            if (!awaitingPlayerDialogue)
            {
                if (!playerTurn)
                {
                    EnemyTurns();
                    DialogueChecker();
                }
            }
            else
            {
                DialogueChecker();
                EndPlayerTurn();
            }
        }
    }

    public void DialogueChecker()
    {
        if (isDialogueActive || awaitingPlayerDialogue)
        {
            if (currentDialogueGameObject.GetComponent<DialogueScript>().dialogueEnded)
            {
                enemies[currentEnemyTurn].awaitingActionToResolve = false;
                isDialogueActive = false;
                awaitingPlayerDialogue = false;
                Destroy(currentDialogueGameObject);
            }
        }
    }

    // PLAYER INPUTS

    public void EnableCertainHud(string hudEnableOption)
    {
        foreach (GameObject hud in allHuds)
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
            case "EndCombat":
                foreach (GameObject hud in allHuds)
                {
                    hud.SetActive(false);
                }
                break;
            default:
                defaultHud.SetActive(true);
                Debug.Log("Not a valid option for battle system. Please check if this is the correct input: " + hudEnableOption);
                break;
        }
    }

    public void AttackButton()
    {
        EnableCertainHud("Attack");
    }

    public void TriggerSelectingAttack()
    {
        if (playerTurn)
        {
            Debug.Log("triggering selecting");
            foreach (GameObject selectingObject in currentlyUsedLocations)
            {
                Debug.Log("selecting added to a enemy");
                selectingObject.transform.GetChild(0).gameObject.SetActive(true);
                StartCoroutine(selectingObject.GetComponentInChildren<SelectingEnemy>().EnemyBlinker());
            }
        }
    }

    public void BasicAttack()
    {
        Debug.Log("BasicAttack");
        if (playerTurn)
        {
            Debug.Log("Player Turn Accepted");
            currentAttack = BattleManager.CurrentAttack.BasicAttack;
            TriggerSelectingAttack();
        }
    }

    public void PrimaryAttack()
    {
        if (playerTurn)
        {
            currentAttack = BattleManager.CurrentAttack.PrimaryAttack;
            TriggerSelectingAttack();
        }
    }

    public void SecondaryAttack()
    {
        if (playerTurn)
        {
            currentAttack = BattleManager.CurrentAttack.SecondaryAttack;
            TriggerSelectingAttack();
        }
    }

    public void MoveEnemiesAfterDeath(int deadEnemyPosition)
    {
        for (int i = 0; i < enemies.Count - 1; i++)
        {
            if (deadEnemyPosition <= i)
            {
                enemies[i] = enemies[i + 1];
                currentlyUsedLocations[i].GetComponent<Image>().sprite = enemies[i + 1].enemySprite;
            }
        }
        enemies.RemoveAt(enemyMaxCount - 1);
        currentlyUsedLocations[enemyMaxCount - 1].SetActive(false);
        currentlyUsedLocations.RemoveAt(enemyMaxCount - 1);
        enemyMaxCount--;
        if (enemyMaxCount == 0)
        {
            EndCombat();
        }
    }

    public void EnemySelectionConfirmation()
    {
        GameObject dialogueObject = Instantiate(dialoguePrefab);
        dialogueObject.transform.parent = dialogueHud.transform;
        currentDialogueGameObject = dialogueObject;
        EnableCertainHud("Dialogue");
        switch (EventSystem.current.currentSelectedGameObject.name)
        {
            case "EnemyLocation1":
                LaunchingAttackAgainstSelectedEnemy(0);
                break;
            case "EnemyLocation2":
                LaunchingAttackAgainstSelectedEnemy(1);
                break;
            case "EnemyLocation3":
                LaunchingAttackAgainstSelectedEnemy(2);
                break;
            case "EnemyLocation4":
                LaunchingAttackAgainstSelectedEnemy(3);
                break;
            default:
                Debug.Log("Fuck");
                break;
        }

        foreach (GameObject selectingObject in currentlyUsedLocations)
        {
            StopCoroutine(selectingObject.GetComponentInChildren<SelectingEnemy>().EnemyBlinker());
            selectingObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        
        awaitingPlayerDialogue = true;
    }

    public void LaunchingAttackAgainstSelectedEnemy(int enemyPositionDialogue)
    {
        float currentDamageToEnemy = 0;
        switch (currentAttack)
        {
            case CurrentAttack.BasicAttack:
                currentDamageToEnemy = PlayerStatManager.instance.Endurance;
                break;
            case CurrentAttack.PrimaryAttack:
                currentDamageToEnemy = CalculateWeaponDamage(true);
                break;
            case CurrentAttack.SecondaryAttack:
                currentDamageToEnemy = CalculateWeaponDamage(false);
                break;
        }
        enemies[enemyPositionDialogue].TakingDamageFromPlayer(currentDamageToEnemy * -1);
        if (enemies[enemyPositionDialogue].currentHealth <= 0)
        {
            MoveEnemiesAfterDeath(0);
        }
        currentDialogueGameObject.GetComponent<DialogueScript>().ResetString(
        "Player Attacked " + enemies[enemyPositionDialogue].enemyName
        + " at position " + enemyPositionDialogue
        + " for " + PlayerStatManager.instance.Endurance + " damage.");
    }

    public float CalculateWeaponDamage(bool isPrimary)
    {
        float calculatedWeaponDamage = 0;
        Item currentWeapon = null;
        if (isPrimary)
        {
            currentWeapon = InventoryManager.Instance.mainEquipedItem;
        }
        else
        {
            currentWeapon = InventoryManager.Instance.secondaryEquipedItem;
        }

        calculatedWeaponDamage = PlayerStatManager.instance.Perception * currentWeapon.damageMultiplier;

        if (Random.Range(0, 101) < PlayerStatManager.instance.Luck)
        {
            calculatedWeaponDamage *= currentWeapon.critMultiplier;
        }

        return calculatedWeaponDamage;
    }

    public void Pass()
    {
        EndPlayerTurn();
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

    public void EndPlayerTurn()
    {
        currentEnemyTurn = 0;
        playerTurn = false;
        foreach (Enemy enemy in enemies)
        {
            enemy.awaitingActionToResolve = false;
        }
    }

    // ENEMY INPUTS / SCRIPTS

    public void EnemyTurns()
    {
        if (!enemies[currentEnemyTurn].awaitingActionToResolve)
        {
            switch (enemies[currentEnemyTurn].currentEnemyState)
            {
                case Enemy.EnemyState.DoAction:
                    enemies[currentEnemyTurn].awaitingActionToResolve = true;
                    DecideEnemyAttack(enemies[currentEnemyTurn]);
                    enemies[currentEnemyTurn].currentEnemyState = Enemy.EnemyState.UserFeedback;
                    break;
                case Enemy.EnemyState.UserFeedback:
                    enemies[currentEnemyTurn].awaitingActionToResolve = true;
                    EnableCertainHud("Dialogue");
                    isDialogueActive = true;
                    GameObject dialogueObject = Instantiate(dialoguePrefab);
                    dialogueObject.transform.parent = dialogueHud.transform;
                    dialogueObject.GetComponent<DialogueScript>().ResetString(enemies[currentEnemyTurn].currentDialogueAction);
                    currentDialogueGameObject = dialogueObject;
                    enemies[currentEnemyTurn].currentEnemyState = Enemy.EnemyState.ResolvingBattlePhase;
                    break;
                case Enemy.EnemyState.ResolvingBattlePhase:
                    enemies[currentEnemyTurn].awaitingActionToResolve = false;
                    enemies[currentEnemyTurn].currentEnemyState = Enemy.EnemyState.DoAction;
                    currentEnemyTurn++;
                    if (currentEnemyTurn >= enemyMaxCount)
                    {
                        EnableCertainHud("Default");
                        playerTurn = true;
                        currentEnemyTurn = 0;
                    }
                    break;
                default:
                    Debug.Log("Incorrect Enemy State. Please check your Enemy State: " + enemies[currentEnemyTurn].currentEnemyState);
                    break;
            }
        }
    }

    public void DecideEnemyAttack(Enemy currentEnemy)
    {
        //Attack
        //SpecialAbility
        //Defend

        //CHANGE TO DO SPECIAL ABILITIES (1) AND DEFENSE (2)
        switch (Random.Range(0, 1))
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
                Debug.Log("How?");
                break;
        }
    }
}
