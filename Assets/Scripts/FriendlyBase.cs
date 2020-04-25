using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendlyBase : MonoBehaviour
{
    [SerializeField] float baseStartingHealth = 1000f;
    [SerializeField] GameObject deathFXtemplate = null;

    Slider healthSlider;
    float baseCurrentHealth;

    PlayerStats playerStats;

    private void Start()
    {
        baseCurrentHealth = baseStartingHealth;
        healthSlider = GetComponentInChildren<Slider>();
        playerStats = FindObjectOfType<PlayerStats>();
    }
    public void DamageFriendlyBase(float damage)
    {
        baseCurrentHealth -= damage;
        print(baseCurrentHealth);
        UpdateHealthBar();
        if (baseCurrentHealth<=0)
        {
            EndLevelOnLoss();
        }
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = baseCurrentHealth / baseStartingHealth;
    }

    public float GetCurrentHealth()
    {
        return baseCurrentHealth;
    }

    private void EndLevelOnLoss()
    {
        StartCoroutine(LevelLost());
    }

    IEnumerator LevelLost()
    {
        int objectNumber = 0;
        FindObjectOfType<EnemySpawner>().StopAllCoroutines(); //Stop spawning new enemies

        Tower[] allTowers = FindObjectsOfType<Tower>();
        Array.Reverse(allTowers);
        foreach (Tower tower in allTowers)
        {
            //tower.KillTower(objectNumber);
            StartCoroutine(tower.DeathSequence(objectNumber));
            objectNumber += 1;
        }

        EnemyCombat[] allEnemies = FindObjectsOfType<EnemyCombat>();
        Array.Reverse(allEnemies);
        foreach (EnemyCombat enemy in allEnemies)
        {
            StartCoroutine(enemy.DeathSequence(objectNumber));
            objectNumber += 1;
        }
        while (allTowers.Length!=0 || allEnemies.Length!=0)
        {
            allTowers = FindObjectsOfType<Tower>();
            allEnemies = FindObjectsOfType<EnemyCombat>();
            yield return 0;
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(playerStats.SetGameOver());
        Destroy(gameObject);
    }


    private void OnDestroy()
    {
        GameObject deathFX = Instantiate(deathFXtemplate, transform.position + Vector3.up*15f, Quaternion.identity);
        var FXduration = deathFX.GetComponent<ParticleSystem>().main.startLifetime.constantMax;
        Destroy(deathFX, FXduration);
    }
}
