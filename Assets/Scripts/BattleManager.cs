using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

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
    public GameObject primaryEquippedAttackButton;
    public GameObject secondaryEquippedAttackButton;
    public GameObject combatSystem;
    public List<GameObject> allEnemyLocations;
    public List<GameObject> currentlyUsedLocations;
    public static BattleManager instance;
    public bool playerTurn;
    public bool activeCombat;
    public int enemyMaxCount;
    public int currentEnemyTurn;
    public bool awaitingPlayerDialogue;
    public int XPGainAfterCombat;
    public List<Item> droppedItems;
    public GameObject combatUILootElement;
    public Sprite unequipedIcon;
    public RectTransform playerHealthBar;
    public TextMeshProUGUI playerHealthText;

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
        allHuds.Add(defaultHud);
        allHuds.Add(attackHud);
        allHuds.Add(itemsHud);
        allHuds.Add(escapeHud);
        allHuds.Add(skillsHud);
        allHuds.Add(dialogueHud);
        activeCombat = false;
    }

    public void UpdateBattleHealth(RectTransform healthBar, float currentHealth, float MaxHealth, TextMeshProUGUI healthText, bool isPlayer)
    {
        
        float calculatedCurrentStatPercentage = currentHealth / MaxHealth * 100;
        if (isPlayer)
        {
            healthText.text = Mathf.Round(currentHealth) + " / 100";
            calculatedCurrentStatPercentage = 400 - calculatedCurrentStatPercentage * 4;
        }
        else
        {
            calculatedCurrentStatPercentage = 550 - calculatedCurrentStatPercentage * (float)5.5;
        }
        Debug.Log(calculatedCurrentStatPercentage);
        RectTransformExtensions.SetRight(healthBar, calculatedCurrentStatPercentage);
    }

    public void StartCombat()
    {
        combatSystem.SetActive(true);
        enemies = new List<Enemy>();
        playerTurn = true;
        isDialogueActive = false;
        UpdateBattleHealth(playerHealthBar, PlayerStatManager.instance.currentHealth, 100f, playerHealthText, true);
        currentEnemyTurn = 0;
        EnableCertainHud("Default");
        enemies = EnemySpawnerManager.instance.SpawnRandomEnemy(3);
        enemyMaxCount = 3;
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].awaitingActionToResolve = true;
            currentlyUsedLocations.Add(allEnemyLocations[i]);
            currentlyUsedLocations[i].SetActive(true);
            currentlyUsedLocations[i].GetComponent<Image>().sprite = enemies[i].enemySprite;
            enemies[i].AdjustStatsToMatchPlayer(PlayerStatManager.instance.Level);
        }
        //Main Equipped Item
        if (InventoryManager.Instance.mainEquipedItem != null)
        {
            primaryEquippedAttackButton.transform.GetChild(1).GetComponent<Image>().sprite = InventoryManager.Instance.mainEquipedItem.itemSprite;
        }
        else
        {
            primaryEquippedAttackButton.transform.GetChild(1).GetComponent<Image>().sprite = unequipedIcon;
            primaryEquippedAttackButton.GetComponent<Button>().interactable = false;
        }
        //Secondary Equipped Item
        if (InventoryManager.Instance.secondaryEquipedItem != null)
        {
            secondaryEquippedAttackButton.transform.GetChild(1).GetComponent<Image>().sprite = InventoryManager.Instance.secondaryEquipedItem.itemSprite;
        }
        else
        {
            secondaryEquippedAttackButton.transform.GetChild(1).GetComponent<Image>().sprite = unequipedIcon;
            secondaryEquippedAttackButton.GetComponent<Button>().interactable = false;
        }

        droppedItems = new List<Item>();
        XPGainAfterCombat = 0;
        activeCombat = true;
    }
    public void EndCombat()
    {
        EnableCertainHud("EndCombat");
        if (currentDialogueGameObject != null)
        {
            Destroy(currentDialogueGameObject);
        }
        for (int i = 0; i > enemies.Count; i++)
        {
            enemies[i].awaitingActionToResolve = true;
            allEnemyLocations[i].SetActive(false);
        }
        combatSystem.SetActive(false);
        LootManager.instance.ShowUserLoot(combatUILootElement, droppedItems, XPGainAfterCombat);
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
                Debug.Log("Player Turn Ended");
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
            foreach (GameObject selectingObject in currentlyUsedLocations)
            {
                selectingObject.transform.GetChild(0).gameObject.SetActive(true);
                StartCoroutine(selectingObject.GetComponentInChildren<SelectingEnemy>().EnemyBlinker());
            }
        }
    }

    public void BasicAttack()
    {
        if (playerTurn)
        {
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
        foreach (GameObject selectingObject in currentlyUsedLocations)
        {
            StopCoroutine(selectingObject.GetComponentInChildren<SelectingEnemy>().EnemyBlinker());
            selectingObject.transform.GetChild(0).gameObject.SetActive(false);
        }
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
        }
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
        UpdateBattleHealth(currentlyUsedLocations[enemyPositionDialogue].transform.GetChild(1).GetChild(0).GetComponent<RectTransform>(),
                           enemies[enemyPositionDialogue].currentHealth,
                           enemies[enemyPositionDialogue].startingHealth,
                           null,
                           false);
        currentDialogueGameObject.GetComponent<DialogueScript>().ResetString(
        "Player Attacked " + enemies[enemyPositionDialogue].enemyName
        + " at position " + (enemyPositionDialogue + 1)
        + " for " + PlayerStatManager.instance.Endurance + " damage.");
        EnableCertainHud("Dialogue"); 
        awaitingPlayerDialogue = true;
        if (enemies[enemyPositionDialogue].currentHealth <= 0)
        {
            droppedItems.Add(enemies[enemyPositionDialogue].droppedItem);
            XPGainAfterCombat += enemies[enemyPositionDialogue].enemyXP;
            MoveEnemiesAfterDeath(0);
        }
        
    }

    public float CalculateWeaponDamage(bool isPrimary)
    {
        float calculatedWeaponDamage;
        Item currentWeapon;
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

    public void Escape()
    {
        EndCombat();
    }

    public void EndPlayerTurn()
    {
        currentEnemyTurn = 0;
        playerTurn = false;
        foreach (Enemy enemy in enemies)
        {
            enemy.awaitingActionToResolve = false;
        }
        Debug.Log("EndPlayerTurn");
    }

    // ENEMY INPUTS / SCRIPTS

    public void EnemyTurns()
    {
        if (!enemies[currentEnemyTurn].awaitingActionToResolve)
        {
            Debug.Log("Enemy Turns Started");
            switch (enemies[currentEnemyTurn].currentEnemyState)
            {
                case Enemy.EnemyState.DoAction:
                    enemies[currentEnemyTurn].awaitingActionToResolve = true;
                    enemies[currentEnemyTurn].DealDamageToPlayer();
                    enemies[currentEnemyTurn].currentEnemyState = Enemy.EnemyState.UserFeedback;
                    Debug.Log("EnemyDoingAction");
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
                    UpdateBattleHealth(playerHealthBar, PlayerStatManager.instance.currentHealth, 100f, playerHealthText, true);
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
}
