using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyMovement enemy1 = null;
    [Range(0.1f,120f)] [SerializeField] float timeBetweenSpawns = 1f;
    [Range(0.1f, 120f)] [SerializeField] float timeBetweenWaves = 3f;
    [SerializeField] int noOfEnemies = 5;
    [Range(0, 1)] float moveWaitRatio = 0.8f;
    [SerializeField] float damagePerSot = 10;

    Pathfinder pathfinder;
    Waypoint startWaypoint;

    Text waveIndicator;

    float enemyHealth = 100;
    int wave = 1;
    
    void Start()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        startWaypoint = pathfinder.GetStartWaypoint();
        waveIndicator = GameObject.Find("Wave Indicator").GetComponent<Text>();
        StartCoroutine(SpawnEnemies());
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
        enemyHealth *= 1.1f;
        wave += 1;
        noOfEnemies += 1;
    }
}