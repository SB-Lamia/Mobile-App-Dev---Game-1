using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    public List<Enemy> Enemies;
    public static EnemySpawnerManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public List<Enemy> SpawnRandomEnemy(int enemyCount)
    {
        List<Enemy> selectedEnemies = new List<Enemy>();
        List<Enemy> possibleEnemies = SortPossibleEnemies();

        for (int i = 0; i < enemyCount; i++)
        {
            Enemy newEnemy = possibleEnemies[Random.Range(0, possibleEnemies.Count)];
            selectedEnemies.Add(newEnemy);
        }

        return selectedEnemies;
    }

    public List<Enemy> SortPossibleEnemies()
    {
        List<Enemy> possibleEnemyEncounters = new List<Enemy>();
        foreach(Enemy enemy in Enemies)
        {
            //Will check for enemies above 5 levels or below 5 levels to give a fair fight.
            if (enemy.averageEnemyLevelEncounter > (PlayerStatManager.instance.Level - 10) &&
                enemy.averageEnemyLevelEncounter < (PlayerStatManager.instance.Level + 10))
            {
                possibleEnemyEncounters.Add(enemy);
            }
        }
        return possibleEnemyEncounters;
    }
}
