using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyMovement enemy1 = null;
    [Range(0.1f,120f)] [SerializeField] float timeBetweenSpawns = 1f;
    [Range(0.1f, 120f)] [SerializeField] float timeBetweenWaves = 3f;
    [Range(0, 1)] float moveWaitRatio = 0.8f;
    [SerializeField] int easyWaveSizeIncreaseChance = 30;
    [SerializeField] int mediumWaveSizeIncreaseChance = 60;
    [SerializeField] int hardWaveSizeIncreaseChance = 80;
    [SerializeField] float easyHealthIncreasePerWave = 3f;
    [SerializeField] float mediumHealthIncreasePerWave = 6f;
    [SerializeField] float hardHealthIncreasePerWave = 10f;

    Pathfinder pathfinder;
    Waypoint startWaypoint;

    Text waveIndicator;

    GameController gameController;
    
    int noOfEnemies = 5;
    float enemyHealth = 100;
    float damagePerSot = 10;
    float waveHealthIncrement;
    float chanceWavesizeIncrease;
    int wave = 1;

    GameController.difficulty currentDifficulty;

    void Start()
    {
        GetRelatedObjects();
        SetDifficultyVariables();
        StartCoroutine(SpawnEnemies());
    }

    private void GetRelatedObjects()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        startWaypoint = pathfinder.GetStartWaypoint();
        waveIndicator = GameObject.Find("Wave Indicator").GetComponent<Text>();
        gameController = FindObjectOfType<GameController>();
    }

    private void SetDifficultyVariables()
    {
        currentDifficulty = gameController.currentDifficulty;
        switch (currentDifficulty)
        {
            case GameController.difficulty.Easy:
                waveHealthIncrement = (100f + easyHealthIncreasePerWave) / 100f;
                chanceWavesizeIncrease = easyWaveSizeIncreaseChance;
                break;
            case GameController.difficulty.Medium:
                waveHealthIncrement = (100f + mediumHealthIncreasePerWave) / 100f;
                chanceWavesizeIncrease = mediumWaveSizeIncreaseChance;
                break;
            case GameController.difficulty.Hard:
                waveHealthIncrement = (100f + hardHealthIncreasePerWave) / 100f;
                chanceWavesizeIncrease = hardWaveSizeIncreaseChance;
                break;
        }
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            waveIndicator.text = "Wave " + wave.ToString() + " incoming";
            waveIndicator.enabled = true;
            yield return new WaitForSeconds(timeBetweenWaves);
            waveIndicator.enabled = false;
            for (int i = 0; i < noOfEnemies; i++)
            {
                EnemyMovement enemy = Instantiate(enemy1, startWaypoint.transform.position, Quaternion.identity, transform);
                enemy.SetEnemyVariables(timeBetweenSpawns, moveWaitRatio);
                enemy.GetComponent<EnemyCombat>().SetEnemyVariables(enemyHealth, damagePerSot);
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
            EnemyMovement[] allEnemies = FindObjectsOfType<EnemyMovement>();
            while (allEnemies.Length>0)
            {
                yield return 0;
                allEnemies = FindObjectsOfType<EnemyMovement>();
            }
            MoveToNextWave();
        }

    }

    private void MoveToNextWave()
    {
        timeBetweenSpawns *= 0.9f;
        enemyHealth *= waveHealthIncrement;

        System.Random rand = new System.Random();
        int chance = rand.Next(101);
        print(chance.ToString());
        if (chance < chanceWavesizeIncrease)
        {
            print("Wave Increased");
            noOfEnemies += 1;
        }
        wave += 1;
    }
}